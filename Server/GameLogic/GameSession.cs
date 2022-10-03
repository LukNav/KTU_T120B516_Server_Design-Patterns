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

         public void SetPlayerAsReady(Player client)
         {
             if (GameInfo.Player1.Name == client.Name)
                 GameInfo.Player1.IsReadyToPlay = true;
             else if (GameInfo.Player2.Name == client.Name)
                 GameInfo.Player2.IsReadyToPlay = true;
             else
                 throw new Exception($"Player {client.Name} doesn't participate in the game");

             if (GameInfo.Player1.IsReadyToPlay && GameInfo.Player2.IsReadyToPlay && !GameHasStarted)
                 StartGame();
         }

         private void StartGame()
         {
             GameHasStarted = true;
             GameInfo.StartTime = DateTime.Now;
         }

        /// <summary>
        /// Try to add a player to a session
        /// </summary>
        /// <param name="client"></param>
        public void AddPlayer(Player client)
         {
             if (GameInfo.Player1 != null)
                 GameInfo.Player1 = client;
             else if (GameInfo.Player2 != null)
                 GameInfo.Player2 = client;
             else
                 throw new Exception($"Player {client.Name} can't be added. Game already has two players");
         }
    }
}
