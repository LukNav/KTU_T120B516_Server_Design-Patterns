namespace Server.Models
{
    public class GameLevel
    {
        public Tower TowerType { get; set; }
        public Pawn Pawn1 { get; set; }
        public Pawn Pawn2 { get; set; }
        public Pawn Pawn3 { get; set; }

        public GameLevel(Tower towerType, Pawn pawn1, Pawn pawn2, Pawn pawn3)
        {
            TowerType=towerType;
            Pawn1=pawn1;
            Pawn2=pawn2;
            Pawn3=pawn3;
        }
    }
}
