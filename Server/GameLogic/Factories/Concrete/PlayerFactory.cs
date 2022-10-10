using System.Drawing;
using System.Security.Cryptography;
using Server.Models;

namespace Server.GameLogic.Factories.Concrete
{
    public class PlayerFactory
    {
        private readonly List<KnownColor> _availablePlayerColors = new List<KnownColor>
            { KnownColor.Red, KnownColor.Green, KnownColor.Blue, KnownColor.Yellow};

        public PlayerFactory()
        {
        }

        public Player Create(string name, string ip)
        {
            int randomColorIndex = RandomNumberGenerator.GetInt32(0, _availablePlayerColors.Count);
            KnownColor selectedRandomColor = _availablePlayerColors[randomColorIndex];
            _availablePlayerColors.RemoveAt(randomColorIndex);

            return new Player { IpAddress = $"https://localhost:{ip}", Name = name, IsReadyToPlay = false, PlayerColor = selectedRandomColor };
        }
    }
}
