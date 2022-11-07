using Microsoft.AspNetCore.Mvc;
using Server.GameLogic.Factories.Concrete;
using Server.Models;

namespace Server.GameLogic
{
    public class ClientSessionFacade : ControllerBase, IClientSessionFacade
    {
        GameSession _gameSession;
        PlayerFactory _playerFactory;

        public ClientSessionFacade()
        {
            _gameSession = GameSession.GetInstance();
            _playerFactory = new PlayerFactory();
        }

        public ActionResult<string> AddClient(string name, string ip)
        {
            Player player = _playerFactory.Create(name, ip);

            string errorMessage = _gameSession.RegisterObserver(player);
            if (errorMessage != null)//If errorMessage is not null, return bad request with error message
                return BadRequest(errorMessage);

            return Created("", $"Player '{name}' was created");
        }

        public ActionResult RemoveClient(string name)
        {
            _gameSession.UnregisterObserver(name);
            return NoContent();
        }

        public IActionResult SetClientReady(string name)
        {
            Task.Run(() => _gameSession.SetPlayerAsReady(name));//Run task async, because the task below is dependant on this connection closing soon
            return Ok();
        }
    }
}
