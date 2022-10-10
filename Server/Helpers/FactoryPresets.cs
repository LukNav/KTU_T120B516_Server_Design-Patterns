using Server.GameLogic.Factories.Abstract;
using Server.GameLogic.Factories.Concrete;

namespace Server.Helpers
{
    public static class FactoryPresets
    {
        public static GameLevelAbstractFactory CreateLevel1Factory()
        {
            return new GameLevelAbstractFactory
            (
                towerFactory: new TowerFactory(100, "Tower_1.png"),
                pawnFactory1: new PawnFactory(20, 5, 1, 10, "Villager_1.png"),
                pawnFactory2: new PawnFactory(30, 15, 2, 15, "Villager_2.png"),
                pawnFactory3: new PawnFactory(100, 25, 3, 30, "Villager_3.png")

            );
        }

    }
}
