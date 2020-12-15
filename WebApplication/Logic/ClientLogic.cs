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
        IRepoBase<WorkoutDetail> detailRepo;
        IRepoBase<ExtraInfo> infoRepo;
        public ClientLogic(IRepoBase<GymClient> clientRepo, IRepoBase<Trainer> trainerRepo,
            IRepoBase<WorkoutDetail> detailRepo, IRepoBase<ExtraInfo> infoRepo)
        {
            this.clientRepo = clientRepo;
            this.trainerRepo = trainerRepo;
            this.detailRepo = detailRepo;
            this.infoRepo = infoRepo;
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
            return clientRepo.GetItem(clientId);
        }

        public void UpdateClient(string clientId, GymClient newClient)
        {
            clientRepo.Update(clientId, newClient);
        }

        #endregion

        #region Non-CRUD methods



        #endregion

    }
}
