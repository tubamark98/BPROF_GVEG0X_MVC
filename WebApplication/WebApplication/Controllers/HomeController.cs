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
        public IActionResult Init()
        {
            trainerLogic.FillDbWithSamples();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Index()
        {
            return View(trainerLogic.GetTrainers());
        }

        public IActionResult GetTrainer(string trainerId)
        {
            return View(trainerLogic.GetTrainer(trainerId));
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
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult CreateClient()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateClient(string trainerId, GymClient gymClient)
        {
            gymClient.GymID = Guid.NewGuid().ToString(); 
            gymClient.TrainerID = trainerId;
            trainerLogic.AddClientToTrainer(gymClient, trainerId);
            return RedirectToAction(nameof(Index));
        }
    }
}
