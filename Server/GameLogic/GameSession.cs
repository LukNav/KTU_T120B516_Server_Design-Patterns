using Server.GameLogic.Factories.Abstract;
using Server.Helpers;
using Server.Models;
using System.Threading;

namespace Server.GameLogic
{
    public class GameSession : IObserverSubject
    {
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
            Observers = new List<Player>(2);
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
                StartGame(FactoryPresets.CreateLevel1Factory());
            }
        }

        /// <summary>
        /// Using this to quick-start the game when debugging
        /// </summary>
        public void StartGameDebug()
        {
            GameHasStarted = true;
            GameInfo.StartTime = DateTime.Now;
            GameInfo.GameLevel = FactoryPresets.CreateLevel1Factory().CreateGameLevel();
            HttpRequests.PostRequest(GameInfo.Player1.IpAddress + "/StartGame/", GameInfo);
        }

        private void StartGame(GameLevelAbstractFactory levelFactory)
        {
            GameHasStarted = true;
            GameInfo.StartTime = DateTime.Now;
            GameInfo.GameLevel = levelFactory.CreateGameLevel();//Use abstract factory to create game level preset for both players
            NotifyAllObservers();//Send to each player updated Game info

            Console.WriteLine("Pradedam zaidima");

            //Gaunam pradinius GameState
            string getEndpoint = "/GetGameState/";
            HttpResponseMessage httpResponseMessagePlayerOne = HttpRequests.GetRequest(GameInfo.Player1.IpAddress + getEndpoint);
            PlayerOneState = httpResponseMessagePlayerOne.Deserialize<GameState>();
            HttpResponseMessage httpResponseMessagePlayerTwo = HttpRequests.GetRequest(GameInfo.Player2.IpAddress + getEndpoint);
            PlayerTwoState = httpResponseMessagePlayerTwo.Deserialize<GameState>();

            Console.WriteLine("Pradedam timed event");

            //Tipo timeris kuris cia kazka veikti gali?
            StateTimer = new Timer(TimedEvent, null, 5000, Timeout.Infinite); //Po 1000 milisekundziu padarys TimedEvent, ir tada tai kartos kas 1000 milisekundziu
        }

        private void TimedEvent(Object stateInfo)
        {

            Console.WriteLine("TimedEventVyksta");

            //Gaunam GameState
            string getEndpoint = "/GetGameState/";
            HttpResponseMessage httpResponseMessagePlayerOne = HttpRequests.GetRequest(GameInfo.Player1.IpAddress + getEndpoint);
            PlayerOneState = httpResponseMessagePlayerOne.Deserialize<GameState>();
            HttpResponseMessage httpResponseMessagePlayerTwo = HttpRequests.GetRequest(GameInfo.Player2.IpAddress + getEndpoint);
            PlayerTwoState = httpResponseMessagePlayerTwo.Deserialize<GameState>();

            //Vincentui: Va cia gali ikisti nuoroda i metoda, kuris su fizikom susitvarkys.



            //Placeholderio tikslu, abiems zaidejams tiesiog nusiuncia pirmo zaidejo busena.
            string updateEndpoint = "/UpdateGameState/";
            HttpRequests.PostRequest(GameInfo.Player1.IpAddress + updateEndpoint, PlayerOneState);
            HttpRequests.PostRequest(GameInfo.Player2.IpAddress + updateEndpoint, PlayerOneState);

            StateTimer.Change(5000, Timeout.Infinite);
        }

        public string NewGameState(string text)
        {
            return text;
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

        public void UnregisterObserver(Player player)
        {
            if (GameInfo.Player1 == null && GameInfo.Player1.Name == player.Name)
            {
                GameInfo.Player1 = null;
                Observers[0] = null;
            }

            else if (GameInfo.Player2 == null && GameInfo.Player2.Name == player.Name)
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
    }
}
