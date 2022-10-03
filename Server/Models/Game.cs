namespace Server.Models
{
    /// <summary>
    /// Client data transfer object to send game info
    /// </summary>
    public class Game
    {
        public GameLevel GameLevel { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public DateTime StartTime { get; set; }

        public Game()
        {
        }

        public Player GetPlayer(string name)
        {
            if (Player1.Name == name)
                return Player1;
            else if (Player2.Name == name)
                return Player2;
            return null;
        }
    }
}
