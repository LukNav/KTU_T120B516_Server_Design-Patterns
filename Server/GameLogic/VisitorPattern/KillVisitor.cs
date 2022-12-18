using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Models;

namespace WindowsFormsApplication.Controllers.VisitorPattern
{
    public class KillVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            Pawn corpse = element as Pawn;
            corpse.IsDead = true;
        }
    }
}
