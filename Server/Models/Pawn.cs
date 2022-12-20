using Server.GameLogic.StrategyPattern;
using System.Drawing;
using System.Numerics;
using System.Text.Json.Serialization;
using Server.GameLogic.FlyWeightPattern;
using WindowsFormsApplication.Controllers.VisitorPattern;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Server.GameLogic.StatePattern;

namespace Server.Models
{
    public class Pawn : Element
    {
        public Pawn() { PawnType = new PawnType(); moveAlgorithm = new ForwardMovement(); }
        public Pawn(Position position, int health, PawnType pawnType, PawnClass tier)
        {
            Position = position;
            Health = health;
            PawnType = pawnType;
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

        public Pawn(Position position, string imageName, int health, int cost, int speed, int damage, int armor, PawnClass tier)
        {
            PawnType = new PawnType(imageName, cost, speed, damage, armor);
            Position = position;
            Health = health;
            SkippedTick = false;
            Tier = tier;
            IsDead = false;
            CurrentState = new PlayerState(this);
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

        public State CurrentState { get; set; }
        public PawnType PawnType { get; set; }
        public Position Position { get; set; }
        public string ImageName { get => PawnType.ImageName; set => PawnType.ImageName = value; }
        public int Health { get; set; }
        public int Cost { get => PawnType.Cost; set => PawnType.Cost = value; }
        public int Speed { get => PawnType.Speed; set => PawnType.Speed = value; }
        public int Damage { get => PawnType.Damage; set => PawnType.Damage = value; }
        public int Armor { get => PawnType.Armor; set => PawnType.Armor = value; }
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
