using Models;
using Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class ClientLogic
    {
        IRepoBase<GymClient> clientRepo;
        IRepoBase<Trainer> trainerRepo;

        public ClientLogic(IRepoBase<GymClient> clientRepo, IRepoBase<Trainer> trainerRepo)
        {
            this.clientRepo = clientRepo;
            this.trainerRepo = trainerRepo;
        }

        #region CRUD methods
        public void AddClient(GymClient gymClient)
        {
            this.clientRepo.Add(gymClient);
        }

        public void DeleteClient(string clientId)
        {
            this.clientRepo.Delete(clientId);
        }
        public IQueryable<GymClient> GetClients()
        {
            return clientRepo.Read();
        }
        public GymClient GetClient(string clientId)
        {
            return clientRepo.Read(clientId);
        }
        public void UpdateClient(string clientId, GymClient newTrainer)
        {
            clientRepo.Update(clientId, newTrainer);
        }

        #endregion

        #region Non-CRUD methods



        #endregion

    }
}
