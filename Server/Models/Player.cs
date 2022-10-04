using System.Drawing;

namespace Server.Models
{
    public class Player : IClient
    {
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public KnownColor PlayerColor { get; set; }
        public bool IsReadyToPlay { get; set; }
    }
}
