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
        private ClientSessionFacade _clientSessionFacade = new ClientSessionFacade();

        [HttpGet("health")]
        public ActionResult<string> HealthCheck()
        {
            return Ok("Healthy");
        }

        [HttpGet("Player/Create/{name}/{ip}")]
        public ActionResult<string> CreateClient(string name, string ip)
        {
            return _clientSessionFacade.AddClient(name, ip);
        }

        [HttpDelete("Player/Unregister/{name}")]
        public ActionResult UnregisterClient(string name)
        {
            return _clientSessionFacade.RemoveClient(name);
        }

        [HttpGet("Player/SetAsReady/{name}")]
        public async Task<IActionResult> SetPlayerAsReady(string name)
        {
            return _clientSessionFacade.SetClientReady(name);
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

        [HttpPost("EndTurn/{name}")]
        public async Task<IActionResult> EndPlayersTurn(string name, [FromBody] GameState gameState)
        {
            Task.Run(() => _gameSession.EndPlayersTurn(gameState, name));
            return Ok();
        }

        [HttpPost("GiveEnemyData/{name}")]
        public async Task<IActionResult> GiveEnemyData(string name, [FromBody] GameState gameState)
        {
            Task.Run(() => _gameSession.GiveEnemyData(gameState, name));
            return Ok();
        }

        [HttpGet("NextLevel")]
        public async Task<IActionResult> NextLevel()
        {
            Task.Run(() => _gameSession.NextLevel());
            return Ok();
        }
    }
}
