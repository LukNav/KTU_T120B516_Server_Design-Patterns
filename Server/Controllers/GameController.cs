using Microsoft.AspNetCore.Mvc;
using Server.GameLogic;

namespace Server.Controllers
{
    public class GameController : Controller
    {
        private GameSession _gameSession = new GameSession();
        public IActionResult Index()
        {
            return View();
        }
    }
}
