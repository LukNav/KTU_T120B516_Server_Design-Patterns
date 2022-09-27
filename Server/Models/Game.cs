namespace Server.Models
{
    /// <summary>
    /// Client data transfer object to send game info
    /// </summary>
    public class Game
    {
        public GameLevel? GameLevel { get; set; }
        public Client? RedClient { get; set; }
        public Client? GreenClient { get; set; }
        public DateTime StartTime { get; set; }
    }
}
