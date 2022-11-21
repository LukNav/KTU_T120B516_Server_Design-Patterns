using Server.GameLogic.Factories.Abstract;
using Server.Helpers;
using Server.Models;
using System.Threading;

namespace Server.GameLogic
{
    public class GameSession : IObserverSubject
    {
        private const int Level = 2;
        private static readonly GameSession _instance = new GameSession();

        private Game GameInfo;

        private bool GameHasStarted = false;

        AutoResetEvent autoEvent = new AutoResetEvent(false);

        GameState PlayerOneState = new GameState();
        GameState PlayerTwoState = new GameState();

        private Timer StateTimer;

        //Isivaizduoju sitaip updatinti galima butu zaidimo busena, bet HTTP yra dalykas kurio visiskai nesuprantu. - Maksas
        private GameGrid GameGrid;

        public List<Player> Observers { get; set; }

        private GameSession()
        {
            GameInfo = new Game();
            GameGrid = new GameGrid();
            Observers = new List<Player>() { null, null};
        }

        public static GameSession GetInstance()
        {
            return _instance;
        }

        public void SetPlayerAsReady(string name)
        {
            Player player = GameInfo.GetPlayer(name);
            if (player == null)
                throw new Exception($"Player {name} doesn't participate in the game");

            player.IsReadyToPlay = true;

            if (GameInfo.Player1 != null && GameInfo.Player1.IsReadyToPlay
                && GameInfo.Player2 != null && GameInfo.Player2.IsReadyToPlay
                && !GameHasStarted)
            {
                StartGame(FactoryPresets.GetLevel(Level));
            }
        }

        /// <summary>
        /// Using this to quick-start the game when debugging
        /// </summary>
        public void StartGameDebug()
        {
            GameHasStarted = true;
            GameInfo.StartTime = DateTime.Now;
            GameInfo.GameLevel = FactoryPresets.GetLevel(1);
            HttpRequests.PostRequest(GameInfo.Player1.IpAddress + "/StartGame/", GameInfo);
        }

        private void StartGame(GameLevel gameLevel)
        {
            GameHasStarted = true;
            GameInfo.StartTime = DateTime.Now;
            GameInfo.GameLevel = gameLevel;//Use abstract factory to create game level preset for both players
            NotifyAllObservers();//Send to each player updated Game info

            //Without adapter
            PlayerStateRequests player1StateRequests = new PlayerStateRequests(GameInfo.Player1);
            HttpResponseMessage httpResponseMessage = player1StateRequests.GetState();
            PlayerOneState = httpResponseMessage.Deserialize<GameState>();

            //With adapter
            PlayerStateRequestsAdapter player2StateRequests = new PlayerStateRequestsAdapter(GameInfo.Player2);
            PlayerTwoState = player2StateRequests.GetState();

            HttpRequests.PostRequest(GameInfo.Player1.IpAddress + "/BeginPlayersTurn", PlayerTwoState);
        }

        public Game GetGameDto()
        {
            return GameInfo;
        }

        /// <summary>
        /// Try to add a player to a session
        /// </summary>
        /// <param name="player"></param>\
        /// <returns>Returns null if player was added and error message if player wasn't added</returns>
        public string RegisterObserver(Player player)
        {
            if (GameInfo.Player1 == null)
            {
                GameInfo.Player1 = player;
                Observers[0] = player;
            }
            else if (GameInfo.Player2 == null)
            {
                if (GameInfo.Player1.Name != player.Name)
                {
                    GameInfo.Player2 = player;
                    Observers[1] = player;
                }
                else
                    return $"Player with name {player.Name} already exists";
            }
            else
                return "Two players are already added";

            return null;
        }

        public void UnregisterObserver(string name)
        {
            if (GameInfo.Player1 != null && GameInfo.Player1.Name == name)
            {
                GameInfo.Player1 = null;
                Observers[0] = null;
            }

            else if (GameInfo.Player2 != null && GameInfo.Player2.Name == name)
            {
                GameInfo.Player2 = null;
                Observers[1] = null;
            }
        }

        /// <summary>
        /// Notify all observers that game has started
        /// </summary>
        public void NotifyAllObservers()
        {
            string endpoint = "/StartGame/";
            foreach (Player player in Observers)
            {
                HttpRequests.PostRequest(player.IpAddress + endpoint, GameInfo);
            }
        }

        internal void EndPlayersTurn(GameState gameState, string name)
        {
            if (GameInfo.Player1.Name == name)
                PlayerOneState = gameState;
            else if(GameInfo.Player2.Name == name)
                PlayerTwoState = gameState;

            HttpRequests.PostRequest(GameInfo.GetOtherPlayer(name).IpAddress + "/BeginPlayersTurn", gameState);
        }
    }
}
