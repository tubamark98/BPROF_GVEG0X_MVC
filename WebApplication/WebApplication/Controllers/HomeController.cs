using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logic;
using Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        readonly ClientLogic clientLogic;
        readonly ExtraInfoLogic infoLogic;
        readonly TrainerLogic trainerLogic;

        public HomeController(TrainerLogic trainerLogic, ClientLogic clientLogic,
            ExtraInfoLogic infoLogic)
        {
            this.trainerLogic = trainerLogic;
            this.clientLogic = clientLogic;
            this.infoLogic = infoLogic;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Init()
        {
            trainerLogic.FillDbWithSamples();
            clientLogic.FillDbWithSamples();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Statistics()
        {
            Statistics statistics = new Statistics
            {
                AverageAgeValue = trainerLogic.AverageAge(),
                AmountOfTrainers = trainerLogic.AmountOfTrainers(),
                AmountOfClients = trainerLogic.AmountOfClients(),
                AmountOfExtraInfo = trainerLogic.AmountOfExtraInfo(),
                GenderPercentage = trainerLogic.GenderPercentage(),
                AmountOfAlcoholists = clientLogic.AmountOfAlcoholists(),
                LongestInfo = clientLogic.LongestInfo()
            };
            return View(nameof(Statistics), statistics);
        }

        #region Create Methods
        [HttpGet]
        public IActionResult CreateClient(string trainerId)
        {
            return View(nameof(CreateClient), trainerId);
        }

        [HttpPost]
        public IActionResult CreateClient(string trainerId, GymClient gymClient)
        {
            gymClient.GymID = Guid.NewGuid().ToString();
            gymClient.TrainerID = trainerId;
            trainerLogic.AddClientToTrainer(gymClient, trainerId);
            return RedirectToAction(nameof(GetTrainer), new { trainerId });
        }

        [HttpGet]
        public IActionResult CreateInfo(string clientId)
        {
            return View(nameof(CreateInfo), clientId);
        }

        [HttpPost]
        public IActionResult CreateInfo(string clientId, ExtraInfo extraInfo)
        {
            extraInfo.InfoId = Guid.NewGuid().ToString();
            clientLogic.AddInfoToClient(extraInfo, clientId);
            return RedirectToAction(nameof(GetTrainer), new { clientLogic.GetClient(clientId).TrainerID });
        }

        [HttpGet]
        public IActionResult CreateTrainer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTrainer( Trainer trainer)
        {
            trainer.TrainerID = Guid.NewGuid().ToString();
            trainerLogic.AddTrainer(trainer);
            return RedirectToAction(nameof(ListTrainer));
        }

        

        #endregion

        #region Read methods

        public IActionResult GetTrainer(string trainerId)
        {
            return View(trainerLogic.GetTrainer(trainerId));
        }

        public IActionResult ListTrainer()
        {
            return View(trainerLogic.GetTrainers());
        }

        #endregion

        #region Update Methods
        [HttpGet]
        public IActionResult UpdateClient(string clientId)
        {
            return View(clientLogic.GetClient(clientId));
        }

        [HttpPost]
        public IActionResult UpdateClient(GymClient newClient)
        {
            clientLogic.UpdateClient(newClient.GymID, newClient);
            return RedirectToAction(nameof(GetTrainer), new { newClient.TrainerID});
        }

        [HttpGet]
        public IActionResult UpdateInfo(string infoId)
        {
            return View(infoLogic.GetInfo(infoId));
        }

        [HttpPost]
        public IActionResult UpdateInfo(ExtraInfo newInfo)
        {
            infoLogic.UpdateInfo(newInfo.InfoId, newInfo);
            return RedirectToAction(nameof(GetTrainer), new { clientLogic.GetClient(newInfo.GymID).TrainerID });
        }

        [HttpGet]
        public IActionResult UpdateTrainer(string trainerId)
        {
            return View(trainerLogic.GetTrainer(trainerId));
        }

        [HttpPost]
        public IActionResult UpdateTrainer(Trainer newTrainer)
        {
            trainerLogic.UpdateTrainer(newTrainer.TrainerID, newTrainer);
            return RedirectToAction(nameof(GetTrainer), new { newTrainer.TrainerID });
        }

        #endregion

        #region Delete Methods

        public IActionResult DeleteClient(string clientId)
        {
            var infoToDelete = clientLogic.GetClient(clientId).ExtraInfos.ToArray();
            for (int i = 0; i < infoToDelete.Length; i++)
            {
                clientLogic.RemoveInfoFromClient(infoToDelete[i], clientId);
                infoLogic.DeleteInfo(infoToDelete[i].InfoId);
            }

            var clientToDelete = clientLogic.GetClient(clientId);
            string trainerId = clientToDelete.TrainerID;
            trainerLogic.RemoveClientFromTrainer(clientToDelete, trainerId);
            clientLogic.DeleteClient(clientId);
            return RedirectToAction(nameof(GetTrainer), new { trainerId} );
        }

        public IActionResult DeleteInfo(string infoId)
        {
            var infoToDelete = infoLogic.GetInfo(infoId);
            string clientId = infoToDelete.GymClient.GymID;
            clientLogic.RemoveInfoFromClient(infoToDelete, clientId);
            infoLogic.DeleteInfo(infoId);
            return RedirectToAction(nameof(GetTrainer), new { infoToDelete.GymClient.TrainerID });
        }

        public IActionResult DeleteTrainer(string trainerId)
        {
            var clientsToDelete = trainerLogic.GetTrainer(trainerId).GymClients.ToArray();
            for (int i = 0; i < clientsToDelete.Length; i++)
            {
                trainerLogic.RemoveClientFromTrainer(clientsToDelete[i], trainerId);
                foreach(var item in clientsToDelete[i].ExtraInfos)
                {
                    clientLogic.RemoveInfoFromClient(item,clientsToDelete[i].GymID);
                }
                clientLogic.DeleteClient(clientsToDelete[i].GymID);
            }

            trainerLogic.DeleteTrainer(trainerId);
            return RedirectToAction(nameof(ListTrainer));
        }

        #endregion
        
    }
}