using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.GameLogic;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private GameSession _gameSession = GameSession.GetInstance();

        [HttpGet("Player/Create/{name}")]
        public ActionResult<string> CreateClient(string name)
        {
            Player player = new Player
            {
                IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString(), 
                Name = name
            };

            string errorMessage = _gameSession.TryCreateAndAddPlayer(player);
            if (errorMessage != null)//If errorMessage is not null, return bad request with error message
                return BadRequest(errorMessage);

            return Created("", $"Player '{name}' was created");
        }

        [HttpGet("Player/SetAsReady/{name}")]
        public ActionResult<string> SetPlayerAsReady(string name)
        {
            _gameSession.SetPlayerAsReady(name);
            return NoContent();
        }

        [HttpGet("Game")]
        public ActionResult<Game> GetGameInfo()
        {
            return Ok(_gameSession.GetGameDto());
        }
    }
}
