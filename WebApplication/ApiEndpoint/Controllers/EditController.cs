using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
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
        bool fillDBControl;

        public EditController(ClientLogic clientLogic, TrainerLogic trainerLogic, ExtraInfoLogic infoLogic)
        {
            this.clientLogic = clientLogic;
            this.trainerLogic = trainerLogic;
            this.infoLogic = infoLogic;
            fillDBControl = false;
        }

        [HttpGet]
        public void FillDb()
        {
            if(!fillDBControl)
            {
                trainerLogic.FillDbWithSamples();
                clientLogic.FillDbWithSamples();
                fillDBControl = true;
            }
        }

        [HttpPost]
        public void AddClientToTrainer([FromBody] ClientAndTrainer item)
        {
            trainerLogic.AddClientToTrainer(clientLogic.GetClient(item.ClientId), item.TrainerId);
        }

        [HttpDelete]
        public void RemoveClientFromTrainer([FromBody] ClientAndTrainer item)
        {
            trainerLogic.RemoveClientFromTrainer(clientLogic.GetClient(item.ClientId), item.TrainerId);
        }

        [HttpPost]
        public void AddInfoToClient([FromBody] ClientAndInfo item)
        {
            clientLogic.AddInfoToClient(infoLogic.GetInfo(item.InfoId), item.ClientId);
        }

        [HttpDelete]
        public void RemoveInfoFromClient([FromBody] ClientAndInfo item)
        {
            clientLogic.RemoveInfoFromClient(infoLogic.GetInfo(item.InfoId), item.ClientId);
        }
    }
}
