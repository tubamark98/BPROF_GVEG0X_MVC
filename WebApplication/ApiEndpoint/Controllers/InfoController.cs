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
    [ApiController]
    [Authorize]
    [Route("{controller}")]
    public class InfoController : ControllerBase
    {
        ExtraInfoLogic logic;
        public InfoController(ExtraInfoLogic logic)
        {
            this.logic = logic;
        }

        [HttpDelete("{uid}")]
        public void DeleteInfo(string uid)
        {
            logic.DeleteInfo(uid);
        }

        [HttpGet("{uid}")]
        public ExtraInfo GetInfo(string uid)
        {
            return logic.GetInfo(uid);
        }

        [HttpGet]
        public IEnumerable<ExtraInfo> GetAllClient()
        {
            return logic.GetInfos();
        }

        [HttpPost]
        public void AddInfo([FromBody] ExtraInfo info)
        {
            logic.AddInfo(info);
        }

        [HttpPut("{oldid}")]
        public void UpdateInfo(string oldid, [FromBody] ExtraInfo info)
        {
            logic.UpdateInfo(oldid, info);
        }
    }
}
