using System.Drawing;
using System.Security.Cryptography;
using Server.Models;

namespace Server.GameLogic
{
    public class PlayerFactory
    {
        private readonly List<KnownColor> _defaultColors = new List<KnownColor>
            { KnownColor.Aqua, KnownColor.Chartreuse, KnownColor.IndianRed, KnownColor.LightGreen, KnownColor.DimGray, KnownColor.SaddleBrown };

        private List<KnownColor> _availablePlayerColors { get; set; }
        public PlayerFactory()
        {
            _availablePlayerColors = new List<KnownColor>(_defaultColors);
        }

        public PlayerFactory(List<KnownColor> playerColorsList)
        {
            if (playerColorsList.Count > 1)
                _availablePlayerColors = playerColorsList;
            else
                _availablePlayerColors = new List<KnownColor>(_defaultColors);
        }

        public Player GetPlayer(string name, string ip)
        {
            if(_availablePlayerColors.Count == 0)
                _availablePlayerColors = new List<KnownColor>(_defaultColors);

            int randomColorIndex = RandomNumberGenerator.GetInt32(0, _availablePlayerColors.Count);
            KnownColor selectedRandomColor = _availablePlayerColors[randomColorIndex];
            _availablePlayerColors.RemoveAt(randomColorIndex);

            return new Player { IpAddress = $"https://localhost:{ip}", Name = name, IsReadyToPlay = false, PlayerColor = selectedRandomColor };
        }
    }
}
