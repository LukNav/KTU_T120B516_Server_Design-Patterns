using System.Drawing;
using System.Numerics;
using System.Security.Cryptography;
using Server.GameLogic.Factories.Abstract;
using Server.Models;

namespace Server.GameLogic.Factories.Concrete
{
    public class TowerFactory : ITowerFactory
    {
        private int _baseHealth { get; }
        private string _imageName { get; set; }

        public TowerFactory(int baseHealth, string imageName)
        {
            _baseHealth=baseHealth;
            _imageName=imageName;
        }


        public Tower Create()
        {
            return new Tower(new Position(0, 0), _imageName, _baseHealth);
        }
    }
}
