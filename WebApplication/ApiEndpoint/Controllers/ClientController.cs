using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEndpoint.Controllers
{
    [Authorize]
    [ApiController]
    [Route("{controller}")]
    public class ClientController : ControllerBase
    {
        ClientLogic logic;

        public ClientController(ClientLogic logic)
        {
            this.logic = logic;
        }

        [HttpDelete("{uid}")]
        public void DeleteClient(string uid)
        {
            logic.DeleteClient(uid);
        }

        [HttpGet("{uid}")]
        public GymClient GetClient(string uid)
        {
            return logic.GetClient(uid);
        }

        [HttpGet]
        public IEnumerable<GymClient> GetAllClient()
        {
            return logic.GetClients();
        }

        [HttpPost]
        public void AddClient([FromBody]GymClient client)
        {
            logic.AddClient(client);
        }

        [HttpPut("{oldid}")]
        public void UpdateClient(string oldid,[FromBody] GymClient client)
        {
            logic.UpdateClient(oldid, client);
        }
    }
}
