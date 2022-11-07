using Server.Models;

namespace Server.Helpers
{
    interface IPlayerStateRequestsAdapter
    {
        GameState GetState();
    }
}