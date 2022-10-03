using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.GameLogic;

namespace Server.Controllers
{
    [Route("game/")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private GameSession _gameSession = GameSession.GetInstance();

        
    }
}
