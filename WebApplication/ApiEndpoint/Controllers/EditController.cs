using Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEndpoint.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class EditController : ControllerBase
    {
        ClientLogic clientLogic;
        TrainerLogic trainerLogic;
        ExtraInfoLogic infoLogic;

        public EditController(ClientLogic clientLogic, TrainerLogic trainerLogic, ExtraInfoLogic infoLogic)
        {
            this.clientLogic = clientLogic;
            this.trainerLogic = trainerLogic;
            this.infoLogic = infoLogic;
        }

        [HttpGet]
        public void FillDb()
        {
            trainerLogic.FillDbWithSamples();
            clientLogic.FillDbWithSamples();
        }
    }
}
