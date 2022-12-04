using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.GameLogic;
using Server.Models;

namespace Tests 
{
    internal class ClientSessionFacadeTests : ControllerBase
    {
        ClientSessionFacade clientSessionFacade;

        [SetUp]
        public void Setup()
        {
            clientSessionFacade = new ClientSessionFacade();    
        }

        [Test]
        public void AddClient_ValidInput_ReturnsCreatedValue ()
        {
            const string name = "testName";
            CreatedResult result = (CreatedResult) clientSessionFacade.AddClient(name, "kpafdkn").Result;
            CreatedResult expected = Created("", $"Player '{name}' was created");

            Assert.That(result.Value, Is.EqualTo(expected.Value));
        }

        [Test]
        public void AddClient_ThreeClients_ReturnsErrorMess()
        {
            CreatedResult result1 = (CreatedResult) clientSessionFacade.AddClient("testName1", "dffdkrn").Result;
            CreatedResult result2 = (CreatedResult) clientSessionFacade.AddClient("testName2", "htkfdkn").Result;
            BadRequestObjectResult result3 = (BadRequestObjectResult) clientSessionFacade.AddClient("testName3", "afasvfr").Result;

            Assert.That(result3.Value, Is.EqualTo("Two players are already added"));
        }
    }
}
