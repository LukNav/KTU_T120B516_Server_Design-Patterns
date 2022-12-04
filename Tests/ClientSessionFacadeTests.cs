using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            clientSessionFacade.RemoveClient(name);
        }

        [Test]
        public void AddClient_ValidInput_Returns201()
        {
            const string name = "testName";
            CreatedResult result = (CreatedResult)clientSessionFacade.AddClient(name, "kpafdkn").Result;
            CreatedResult expected = Created("", $"Player '{name}' was created");

            Assert.That(result.StatusCode, Is.EqualTo(expected.StatusCode));
            clientSessionFacade.RemoveClient(name);
        }

        [Test]
        public void AddClient_ThreeClients_ReturnsErrorMess()
        {
            CreatedResult result1 = (CreatedResult) clientSessionFacade.AddClient("testName1", "dffdkrn").Result;
            CreatedResult result2 = (CreatedResult) clientSessionFacade.AddClient("testName2", "htkfdkn").Result;
            BadRequestObjectResult result3 = (BadRequestObjectResult) clientSessionFacade.AddClient("testName3", "afasvfr").Result;

            Assert.That(result3.Value, Is.EqualTo("Two players are already added"));
            clientSessionFacade.RemoveClient("testName1");
            clientSessionFacade.RemoveClient("testName2");
        }

        [Test]
        public void RemoveClientFromOnePlayer_ValidInput_Returns204()
        {
            const string name = "testName";
            CreatedResult client = (CreatedResult)clientSessionFacade.AddClient(name, "kpafdkn").Result;
            NoContentResult result = (NoContentResult) clientSessionFacade.RemoveClient(name);

            Assert.That(result.StatusCode, Is.EqualTo(204));
        }

        [Test]
        public void RemoveClientFirstPlayer_ValidInput_Returns204()
        {
            const string name1 = "testName1";
            const string name2 = "testName2";
            CreatedResult client = (CreatedResult)clientSessionFacade.AddClient(name1, "kpafdkn").Result;
            CreatedResult client2 = (CreatedResult)clientSessionFacade.AddClient(name2, "kpafdkn").Result;
            NoContentResult result = (NoContentResult)clientSessionFacade.RemoveClient(name1);

            Assert.That(result.StatusCode, Is.EqualTo(204));
            clientSessionFacade.RemoveClient(name2);
        }

        [Test]
        public void RemoveClientSecondPlayer_ValidInput_Returns204()
        {
            const string name1 = "testName1";
            const string name2 = "testName2";
            CreatedResult client = (CreatedResult)clientSessionFacade.AddClient(name1, "kpafdkn").Result;
            CreatedResult client2 = (CreatedResult)clientSessionFacade.AddClient(name2, "kpafdkn").Result;
            NoContentResult result = (NoContentResult)clientSessionFacade.RemoveClient(name2);

            Assert.That(result.StatusCode, Is.EqualTo(204));
            clientSessionFacade.RemoveClient(name1);
        }

        [Test]
        public void RemoveClient_NotValidInput_Returns()
        {
            const string name = "testName";
            CreatedResult client = (CreatedResult)clientSessionFacade.AddClient(name, "kpafdkn").Result;
            NoContentResult result = (NoContentResult)clientSessionFacade.RemoveClient(name+"34");

            Assert.That(result.StatusCode, Is.EqualTo(204)); //nelogiška, bet logiška
            clientSessionFacade.RemoveClient(name);
        }



    }
}
