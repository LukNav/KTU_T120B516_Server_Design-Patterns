using Server.GameLogic.Factories.Abstract;
using Server.GameLogic.Factories.Concrete;
using Server.Models;

namespace Server.Helpers
{
    public static class FactoryPresets
    {
        /// <summary>
        /// Get created game level by abstract factory. Only 3 levels exist, so if property level is < 1 or > 3, first level is returned
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static GameLevel GetLevel(int level)
        {
            switch (level)
            {
                case 2:
                    return CreateLevel2Factory();
                case 3:
                    return CreateLevel3Factory();
                default:
                    return new GameLevel(
                        towerType : new Tower(new Position(0, 0), "Tower_1.png", 100),
                        pawn1 : new Pawn(new Position(0, 0), "Soldier_1_1.png", 40, 5, 1, 20, 0, PawnClass.Tier1),
                        pawn2 : new Pawn(new Position(0, 0), "Raider_1_1.png", 30, 15, 2, 35, 5, PawnClass.Tier2),
                        pawn3 : new Pawn(new Position(0, 0), "Knight_1_1.png", 50, 25, 3, 45, 9, PawnClass.Tier3));
            }
                
        }

        public static GameLevel CreateLevel2Factory()
        {
            return new GameLevelAbstractFactory
            (
                towerFactory: new TowerFactory(200, "Tower_2.png"),
                pawnFactory1: new PawnFactory(40,  "Soldier_1_2.png", PawnClass.Tier1),
                pawnFactory2: new PawnFactory(30,  "Raider_1_2.png", PawnClass.Tier2),
                pawnFactory3: new PawnFactory(50, "Knight_1_2.png", PawnClass.Tier3)
            ).CreateGameLevel();
        }

        public static GameLevel CreateLevel3Factory()
        {
            return new GameLevelAbstractFactory
            (
                towerFactory: new TowerFactory(100, "Tower_3.png"),
                pawnFactory1: new PawnFactory(60, "Soldier_1_3.png", PawnClass.Tier1),
                pawnFactory2: new PawnFactory(50, "Raider_1_3.png", PawnClass.Tier2),
                pawnFactory3: new PawnFactory(50, "Knight_1_3.png", PawnClass.Tier3)
            ).CreateGameLevel();
        }
    }
}
