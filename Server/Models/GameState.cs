namespace Server.Models
{
    public class GameState
    {
        public int PlayerTowerHealth { get; set; }
        public int OpponentTowerHealth { get;set; }
        public List<Pawn> Pawns { get; set; }
        public GameGrid SelectedGameGrid { get; set; }

    }
}
