using System.Drawing;
using System.Security.Cryptography;
using Server.Models;

namespace Server.GameLogic.Factories.Concrete
{
    public class PlayerFactory
    {
        private readonly List<KnownColor> _basePlayerColors = new List<KnownColor>
            { KnownColor.Red, KnownColor.Green, KnownColor.Blue, KnownColor.Yellow};

        private List<KnownColor> _availablePlayerColors;

        public PlayerFactory()

        {
            KnownColor[] tempArray = new KnownColor[_basePlayerColors.Count];
            _basePlayerColors.CopyTo(tempArray);
            _availablePlayerColors = new List<KnownColor>(tempArray);
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
