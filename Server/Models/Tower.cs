using System.Drawing;

namespace Server.Models
{
    public class Tower
    {
        public Tower(Point position, string imageName, int health)
        {
            this.position=position;
            this.imageName=imageName;
            this.health=health;
        }

        public Point position { get; set; }
        public string imageName { get; set; }
        public int health { get; set; }
    }
}
