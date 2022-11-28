namespace Server.Models
{
    public class GameLevel
    {
        public int Level { get; set; }
        public Tower TowerType { get; set; }
        public Pawn Pawn1 { get; set; }
        public Pawn Pawn2 { get; set; }
        public Pawn Pawn3 { get; set; }

        public GameLevel(Tower towerType, Pawn pawn1, Pawn pawn2, Pawn pawn3)
        {
            Level = 1;
            TowerType=towerType;
            Pawn1=pawn1;
            Pawn2=pawn2;
            Pawn3=pawn3;
        }
    }
}
