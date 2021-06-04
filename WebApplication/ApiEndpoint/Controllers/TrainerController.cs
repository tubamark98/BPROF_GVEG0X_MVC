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
    //[Authorize]
    [Route("{controller}")]
    public class TrainerController : ControllerBase
    {
        TrainerLogic logic;

        public TrainerController(TrainerLogic logic)
        {
            this.logic = logic;
        }

        [HttpDelete("{uid}")]
        public void DeleteTrainer(string uid)
        {
            logic.DeleteTrainer(uid);
        }

        [HttpGet("{uid}")]
        public Trainer GetTrainer(string uid)
        {
            return logic.GetTrainer(uid);
        }

        [HttpGet]
        public IEnumerable<Trainer> GetAllTrainers()
        {
            return logic.GetTrainers();
        }

        [HttpPost]
        public void AddTrainer([FromBody] Trainer trainer)
        {
            logic.AddTrainer(trainer);
        }

        [HttpPut("{oldid}")]
        public void UpdateTrainer(string oldid, [FromBody] Trainer trainer)
        {
            logic.UpdateTrainer(oldid, trainer);
        }

    }
}
