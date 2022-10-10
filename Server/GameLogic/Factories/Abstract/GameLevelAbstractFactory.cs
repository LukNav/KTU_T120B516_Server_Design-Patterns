using Server.GameLogic.Factories.Concrete;
using Server.Models;

namespace Server.GameLogic.Factories.Abstract
{
    public class GameLevelAbstractFactory
    {
        public IPawnFactory PawnFactory1 { get; }
        public IPawnFactory PawnFactory2 { get; }
        public IPawnFactory PawnFactory3 { get; }
        public ITowerFactory TowerFactory { get; }

        public GameLevelAbstractFactory(IPawnFactory pawnFactory1, IPawnFactory pawnFactory2, IPawnFactory pawnFactory3, ITowerFactory towerFactory)
        {
            PawnFactory1=pawnFactory1;
            PawnFactory2=pawnFactory2;
            PawnFactory3=pawnFactory3;
            TowerFactory=towerFactory;
        }

        public Tower CreateTower()
        {
            return TowerFactory.Create();
        }

        public Pawn CreatePawn1()
        {
            return PawnFactory1.Create();
        }
        public Pawn CreatePawn2()
        {
            return PawnFactory2.Create();
        }
        public Pawn CreatePawn3()
        {
            return PawnFactory3.Create();
        }
    }
}
