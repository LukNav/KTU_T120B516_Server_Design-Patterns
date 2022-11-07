using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.GameLogic;
using Server.GameLogic.Factories.Concrete;
using Server.Models;
using System.Timers;

namespace Server.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private GameSession _gameSession = GameSession.GetInstance();
        private PlayerFactory _playerFactory = new PlayerFactory();

        [HttpGet("Player/Create/{name}/{ip}")]
        public ActionResult<string> CreateClient(string name, string ip)
        {
            Player player = _playerFactory.Create(name, ip);

            string errorMessage = _gameSession.RegisterObserver(player);
            if (errorMessage != null)//If errorMessage is not null, return bad request with error message
                return BadRequest(errorMessage);

            return Created("", $"Player '{name}' was created");
        }

        [HttpDelete("Player/Unregister/{name}")]
        public ActionResult UnregisterClient(string name)
        {
            _gameSession.UnregisterObserver(name);
            return NoContent();
        }

        [HttpGet("Player/SetAsReady/{name}")]
        public async Task<IActionResult> SetPlayerAsReady(string name)
        {
            Task.Run(() => _gameSession.SetPlayerAsReady(name));//Run task async, because the task below is dependant on this connection closing soon
            return Ok();
        }

        [HttpGet("/Debug/StartGameSolo/{port}")]
        public async Task<IActionResult> DebugStartSolo(string port)//Using this only to quickstart the game when debugging
        {
            CreateClient("0", port);
            CreateClient("1", port);
            Task.Run(() => _gameSession.StartGameDebug());

            return Ok();
        }

        [HttpGet("Game")]
        public ActionResult<Game> GetGameInfo()
        {
            return Ok(_gameSession.GetGameDto());
        }

        [HttpGet("NewGameState")]
        public ActionResult<string> NewGameState()
        {
            return Ok(_gameSession.NewGameState("lmao"));
        }
    }
}
