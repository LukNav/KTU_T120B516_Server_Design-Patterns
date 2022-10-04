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

        [HttpGet("Player/Create/{name}/{ip}")]
        public ActionResult<string> CreateClient(string name, string ip)
        {
            PlayerFactory playerFactory = new PlayerFactory();

            Player player = playerFactory.GetPlayer(name, ip);

            string errorMessage = _gameSession.TryCreateAndAddPlayer(player);
            if (errorMessage != null)//If errorMessage is not null, return bad request with error message
                return BadRequest(errorMessage);

            return Created("", $"Player '{name}' was created");
        }

        [HttpGet("Player/SetAsReady/{name}")]
        public async Task<IActionResult> SetPlayerAsReady(string name)
        {
            Task.Run(() => _gameSession.SetPlayerAsReady(name));//Run task async, because the task below is dependant on this connection closing soon
            return Ok();
        }

        [HttpGet("Game")]
        public ActionResult<Game> GetGameInfo()
        {
            return Ok(_gameSession.GetGameDto());
        }
    }
}
