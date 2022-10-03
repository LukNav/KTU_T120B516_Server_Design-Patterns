using Server.Helpers;
using Server.Models;

namespace Server.GameLogic
{
    public class GameSession
    {
         private static readonly GameSession _instance = new GameSession();

         private Game GameInfo;

         private bool GameHasStarted = false;

         private GameSession()
         {
             GameInfo = new Game();
         }

         public static GameSession GetInstance()
         {
             return _instance;
         }

         public void SetPlayerAsReady(string name)
         {
            Player player = GameInfo.GetPlayer(name);
            if(player == null)
                throw new Exception($"Player {name} doesn't participate in the game");

            player.IsReadyToPlay = true;

            if (GameInfo.Player1 != null && GameInfo.Player1.IsReadyToPlay
                &&  GameInfo.Player2 != null && GameInfo.Player2.IsReadyToPlay 
                && !GameHasStarted)
            {
                StartGame();
            }
         }

         private void StartGame()
         {
             GameHasStarted = true;
             GameInfo.StartTime = DateTime.Now;
             GameStartedNotifyPlayers();//Send to each player updated Game info
         }

        private void GameStartedNotifyPlayers()
        {
            string endpoint = "/StartGame/";
            HttpRequests.PostRequest(GameInfo.Player1.IpAddress+endpoint, GameInfo);
            HttpRequests.PostRequest(GameInfo.Player2.IpAddress+endpoint, GameInfo);
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
         public string TryCreateAndAddPlayer(Player player)
         {
             if (GameInfo.Player1 == null)
                 GameInfo.Player1 = player;
             else if (GameInfo.Player2 == null)
             {
                 if (GameInfo.Player1.Name != player.Name)
                     GameInfo.Player2 = player;
                 else
                     return $"Player with name {player.Name} already exists";
             }
             else
                 return "Two players are already added";

             return null;
         }
    }
}
