using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.GameLogic;

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
            ActionResult<string> result = clientSessionFacade.AddClient(name, "kpafdkn");
            ActionResult<string> expected = Created("", $"Player '{name}' was created");
            Assert.That(result.Value, Is.EqualTo(expected.Value));
        }
    }
}
