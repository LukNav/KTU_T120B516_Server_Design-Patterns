namespace Server.Models
{
    public class Client
    {
        public string Name { get; set; }
        /// <summary>
        /// Which side the player has chosen in the game
        /// </summary>
        public ClientFactionEnum Faction  { get; set; }
    }
}
