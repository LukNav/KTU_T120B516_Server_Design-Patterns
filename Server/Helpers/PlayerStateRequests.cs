using Microsoft.Net.Http.Headers;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server.Helpers
{
    public class PlayerStateRequests
    {
        Player _player { get; set; }

        public PlayerStateRequests(Player player)
        {
            _player = player;
        }

        public GameState GetState()
        {
            string getEndpoint = "/GetGameState/";
            HttpResponseMessage httpResponseMessagePlayerOne = HttpRequests.GetRequest(_player.IpAddress + getEndpoint);
            return httpResponseMessagePlayerOne.Deserialize<GameState>();
        }
    }
}