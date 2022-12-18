using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication.Controllers.VisitorPattern
{
    public class CorpseVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            Pawn corpse = element as Pawn;
            corpse.ImageName = "Corpse.png";
        }
    }
}
