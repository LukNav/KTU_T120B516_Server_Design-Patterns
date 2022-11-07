using Microsoft.AspNetCore.Mvc;

namespace Server.GameLogic
{
    public interface IClientSessionFacade
    {
        ActionResult<string> AddClient(string name, string ip);
        ActionResult RemoveClient(string name);
        Task<IActionResult> SetClientReady(string name);
    }
}