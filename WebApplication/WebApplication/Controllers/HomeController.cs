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
        TrainerLogic trainerLogic;
        ClientLogic clientLogic;

        public HomeController(TrainerLogic trainerLogic, ClientLogic clientLogic)
        {
            this.trainerLogic = trainerLogic;
            this.clientLogic = clientLogic;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Init()
        {
            trainerLogic.FillDbWithSamples();
            return RedirectToAction(nameof(Index));
        }

        #region Create Methods

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
            return RedirectToAction(nameof(GetTrainer), new { trainerId } );
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
            return View(nameof(GetTrainer), new { newClient.TrainerID});
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
            var clientToDelete = clientLogic.GetClient(clientId);
            string trainerId = clientToDelete.TrainerID;
            trainerLogic.RemoveClientFromTrainer(clientToDelete, trainerId);
            clientLogic.DeleteClient(clientId);
            return RedirectToAction(nameof(GetTrainer), new { trainerId} );
        }

        public IActionResult DeleteTrainer(string trainerId)
        {
            var clientsToDelete = trainerLogic.GetTrainer(trainerId).GymClients.ToArray();
            for (int i = 0; i < clientsToDelete.Length; i++)
            {
                trainerLogic.RemoveClientFromTrainer(clientsToDelete[i], trainerId);
                clientLogic.DeleteClient(clientsToDelete[i].GymID);
            }

            trainerLogic.DeleteTrainer(trainerId);
            return RedirectToAction(nameof(ListTrainer));
        }

        #endregion
        
    }
}