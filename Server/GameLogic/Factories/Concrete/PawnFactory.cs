using System.Drawing;
using System.Numerics;
using Server.GameLogic.Factories.Abstract;
using Server.Models;

namespace Server.GameLogic.Factories.Concrete
{
    public class PawnFactory : IPawnFactory
    {
        public int Health { get; set; }
        public int Cost { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public string ImageName { get; set; }
        public PawnClass Tier { get; set; }

        public PawnFactory(int health, int cost, int speed, int damage, string imageName, PawnClass tier)
        {
            Health=health;
            Cost=cost;
            Speed=speed;
            Damage=damage;
            ImageName=imageName;
            Tier=tier;
        }

        public Pawn Create()
        {
            return new Pawn(position: new Position(0, 0), imageName: ImageName,
                health: Health, cost: Cost, speed: Speed, damage: Damage, tier: Tier);
        }
    }

}
