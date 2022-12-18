using Server.GameLogic.StrategyPattern;
using System.Drawing;
using System.Numerics;
using System.Text.Json.Serialization;
using WindowsFormsApplication.Controllers.VisitorPattern;

namespace Server.Models
{
    public class Pawn : Element
    {
        public Pawn(Position position, string imageName, int health, int cost, int speed, int damage, int armor, PawnClass tier)
        {
            Position = position;
            ImageName = imageName;
            Health = health;
            Cost = cost;
            Speed = speed;
            Damage = damage;
            Armor = armor;
            SkippedTick = false;
            Tier = tier;
            IsDead = false;
            switch (tier)
            {
                case PawnClass.Tier1:
                    moveAlgorithm = new ForwardMovement();
                    break;
                case PawnClass.Tier2:
                    moveAlgorithm = new DiagonalMovement();
                    break;
                case PawnClass.Tier3:
                    moveAlgorithm = new DelayedMovement();
                    break;
                default:
                    break;
            }
        }

        public Position Position { get; set; }
        public string ImageName { get; set; }
        public int Health { get; set; }
        public int Cost { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public bool SkippedTick { get; set; }
        public bool IsDead { get; set; }
        public PawnClass Tier { get; set; }
        [JsonIgnore]
        public IMoveAlgorithm moveAlgorithm { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
    public enum PawnClass
    {
        Tier1,
        Tier2,
        Tier3
    }
}
