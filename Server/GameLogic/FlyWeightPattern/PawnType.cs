using Server.Models;

namespace Server.GameLogic.FlyWeightPattern
{
    public class PawnType
    {
        public string ImageName { get; set; }
        public int Cost { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }

        public PawnType(string imageName, int cost, int speed, int damage, int armor)
        {
            ImageName = imageName;
            Cost = cost;
            Speed = speed;
            Damage = damage;
            Armor = armor;
        }

        public PawnType()
        {
        }
    }
}
