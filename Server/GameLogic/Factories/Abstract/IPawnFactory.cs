using Server.GameLogic.Factories.Concrete;
using Server.Models;

namespace Server.GameLogic.Factories.Abstract
{
    public interface IPawnFactory
    {
        Pawn Create();
    }
}