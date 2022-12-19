using System.Drawing;
using System.Numerics;
using Server.GameLogic.Factories.Abstract;
using Server.GameLogic.FlyWeightPattern;
using Server.Models;

namespace Server.GameLogic.Factories.Concrete
{
    public class PawnFactory : IPawnFactory, IFlyWeight<PawnType>
    {
        public int Health { get; set; }
        public PawnClass Tier { get; set; }
        string PawnType { get; set; }

        private Dictionary<string, PawnType> _typeMap;

        public PawnFactory(int health, string pawnType, PawnClass tier)
        {
            Health=health;
            Tier=tier;
            PawnType = pawnType;
            _typeMap = new Dictionary<string, PawnType>
            {
                { "Soldier_1_1.png", new PawnType("Soldier_1_1.png",5,1,20,0)},
                { "Raider_1_1.png", new PawnType("Raider_1_1.png",15,2,35,5)},
                { "Knight_1_1.png", new PawnType("Knight_1_1.png",25, 3, 45, 9)},
                { "Soldier_1_2.png", new PawnType("Soldier_1_2.png",5, 1, 20, 0)},
                { "Raider_1_2.png", new PawnType("Raider_1_2.png",15, 2, 35, 5)},
                { "Knight_1_2.png", new PawnType("Knight_1_2.png",25, 3, 45, 9)},
                { "Soldier_1_3.png", new PawnType("Soldier_1_3.png", 5, 1, 20, 0)},
                { "Raider_1_3.png", new PawnType("Raider_1_3.png", 15, 2, 45, 5)},
                { "Knight_1_3.png", new PawnType("Knight_1_3.png", 25,3, 60, 9)}
            };

        }



        public Pawn Create()
        {
            return new Pawn(new Position(0, 0), Health, GetType(PawnType), Tier);
        }

        public PawnType GetType(string key)
        {
            return _typeMap[key];
        }
    }

}
