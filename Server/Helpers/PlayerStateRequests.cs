﻿using Microsoft.Net.Http.Headers;
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
        private string endpoint;

        Player _player { get; set; }

        public PlayerStateRequests(Player player)
        {
            _player = player;
            endpoint = "/GetGameState/";
        }

        //public EnemyStateRequests(Player player)
        //{
        //    _player = player;
        //    endpoint = "/GetEnemyGameState/";
        //} would be idea, too tired to investigate

        public HttpResponseMessage GetState()
        {
            string getEndpoint = endpoint;
            return HttpRequests.GetRequest(_player.IpAddress + getEndpoint);
        }

        public void UpdateState()
        {
            HttpRequests.PostRequest(_player.IpAddress + "/updateGameState", _player);
        }
    }
}