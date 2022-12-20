using Server.Models;

namespace Server.GameLogic.StatePattern
{
    public class PlayerState : State
    {
        public PlayerState(Pawn pawn) { }
        public override void SwitchState()
        {
            throw new NotImplementedException();
        }
    }
}
