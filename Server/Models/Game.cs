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
    }
}
