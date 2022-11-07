using Microsoft.AspNetCore.Mvc;

namespace Server.GameLogic
{
    public interface IClientSessionFacade
    {
        ActionResult<string> AddClient(string name, string ip);
        ActionResult RemoveClient(string name);
        IActionResult SetClientReady(string name);
    }
}