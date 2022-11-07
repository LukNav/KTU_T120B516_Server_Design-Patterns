using Server.Models;

namespace Server.Helpers
{
    public class PlayerStateRequestsAdapter : IPlayerStateRequestsAdapter
    {
        PlayerStateRequests _playerStateRequests;

        public PlayerStateRequestsAdapter(Player player)
        {
            _playerStateRequests = new PlayerStateRequests(player);
        }

        public GameState GetState()
        {
            return _playerStateRequests.GetState().Deserialize<GameState>();
        }
    }
}