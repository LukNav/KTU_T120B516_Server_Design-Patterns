namespace Server.Models
{
    public class GameLevel
    {
        public string Name { get; set; }
        public int GameLengthInMinutes { get; set; }
        public List<Tower> ClientsTowers { get; set; }
    }
}
