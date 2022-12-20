using Server.Models;

namespace Server.GameLogic.StatePattern
{
    public abstract class State
    {
        private Pawn pawn;
        public bool IsSelectable { get; set; }

        public Pawn Pawn
        {
            get
            {
                return pawn;
            }
            set
            {
                pawn = value;
            }
        }

        public abstract void SwitchState();
    }
}
