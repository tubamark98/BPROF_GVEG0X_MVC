using Models;
using Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class TrainerLogic
    {
        IRepoBase<GymClient> clientRepo;
        IRepoBase<Trainer> trainerRepo;

        public TrainerLogic(IRepoBase<GymClient> clientRepo, IRepoBase<Trainer> trainerRepo)
        {
            this.clientRepo = clientRepo;
            this.trainerRepo = trainerRepo;
        }


        #region CRUD methods
        public void AddTrainer(Trainer trainer)
        {
            this.trainerRepo.Add(trainer);
        }

        public void DeleteTrainer(string trainerId)
        {
            this.trainerRepo.Delete(trainerId);
        }
        public IQueryable<Trainer> GetTrainers()
        {
            return trainerRepo.Read();
        }
        public Trainer GetTrainer(string trainerId)
        {
            return trainerRepo.Read(trainerId);
        }
        public void UpdateTrainer(string trainerId, Trainer newTrainer)
        {
            trainerRepo.Update(trainerId, newTrainer);
        }

        #endregion

        #region Non-CRUD methods

        public void AddClientToTrainer(GymClient gymClient, string trainerId)
        {
            GetTrainer(trainerId).GymClients.Add(gymClient);
            trainerRepo.Save();
        }

        public void RemoveClientFromTrainer(GymClient gymClient, string trainerId)
        {
            GetTrainer(trainerId).GymClients.Remove(gymClient);
            trainerRepo.Save();
        }

        #endregion
    }
}
