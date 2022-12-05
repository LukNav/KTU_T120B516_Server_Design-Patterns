using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
            clientSessionFacade.AddClient("testName1", "dffdkrn");
            clientSessionFacade.AddClient("testName2", "htkfdkn");
            BadRequestObjectResult result3 = (BadRequestObjectResult)clientSessionFacade.AddClient("testName3", "afasvfr").Result;

            Assert.That(result3.Value, Is.EqualTo("Two players are already added"));
            clientSessionFacade.RemoveClient("testName1");
            clientSessionFacade.RemoveClient("testName2");
        }
        [Test]
        public void AddClient_NullClient_ReturnsErrorMess()
        {
            const string name = null;
            CreatedResult result = (CreatedResult)clientSessionFacade.AddClient(name, "dffdkrn").Result;

            Assert.That(result.Value, Is.EqualTo($"Player '{name}' was created"));
            clientSessionFacade.RemoveClient(name);
        }

        [Test]
        public void RemoveClientFromOnePlayer_ValidInput_Returns204()
        {
            const string name = "testName";
            clientSessionFacade.AddClient(name, "kpafdkn");
            NoContentResult result = (NoContentResult) clientSessionFacade.RemoveClient(name);

            Assert.That(result.StatusCode, Is.EqualTo(204));
        }

        [Test]
        public void RemoveClientFirstPlayer_ValidInput_Returns204()
        {
            const string name1 = "testName1";
            const string name2 = "testName2";
            clientSessionFacade.AddClient(name1, "kpafdkn");
            clientSessionFacade.AddClient(name2, "kpafdkn");
            NoContentResult result = (NoContentResult)clientSessionFacade.RemoveClient(name1);

            Assert.That(result.StatusCode, Is.EqualTo(204));
            clientSessionFacade.RemoveClient(name2);
        }

        [Test]
        public void RemoveClientSecondPlayer_ValidInput_Returns204()
        {
            const string name1 = "testName1";
            const string name2 = "testName2";
            clientSessionFacade.AddClient(name1, "kpafdkn");
            clientSessionFacade.AddClient(name2, "kpafdkn");
            NoContentResult result = (NoContentResult)clientSessionFacade.RemoveClient(name2);

            Assert.That(result.StatusCode, Is.EqualTo(204));
            clientSessionFacade.RemoveClient(name1);
        }

        [Test]
        public void RemoveClient_NotValidInput_Returns204()
        {
            const string name = "testName";
            clientSessionFacade.AddClient(name, "kpafdkn");
            NoContentResult result = (NoContentResult)clientSessionFacade.RemoveClient(name + "34");

            Assert.That(result.StatusCode, Is.EqualTo(204));
            clientSessionFacade.RemoveClient(name);
        }

        [Test]
        public void RemoveClient_NullInput_Returns204()
        {
            const string name = null;
            clientSessionFacade.AddClient(name, "kpafdkn");
            NoContentResult result = (NoContentResult)clientSessionFacade.RemoveClient(name);

            Assert.That(result.StatusCode, Is.EqualTo(204));
            clientSessionFacade.RemoveClient(name);
        }
    }
}
