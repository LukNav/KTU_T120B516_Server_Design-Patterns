using System.Drawing;
using System.Numerics;

namespace Server.Models
{
    public class Tower
    {
        public Tower(Position position, string imageName, int health)
        {
            this.position=position;
            this.imageName=imageName;
            this.health=health;
        }

        public Position position { get; set; }
        public string imageName { get; set; }
        public int health { get; set; }
    }
}
