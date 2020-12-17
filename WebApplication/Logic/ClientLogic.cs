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
        IRepoBase<WorkoutDetail> detailRepo;
        IRepoBase<ExtraInfo> infoRepo;

        public ClientLogic(IRepoBase<GymClient> clientRepo)
        {
            this.clientRepo = clientRepo;
        }
        public ClientLogic(IRepoBase<GymClient> clientRepo, IRepoBase<WorkoutDetail> detailRepo, IRepoBase<ExtraInfo> infoRepo)
        {
            this.clientRepo = clientRepo;
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
        public void AddDetailToClient(WorkoutDetail workoutDetail, string clientId)
        {
            GetClient(clientId).WorkoutDetail = new WorkoutDetail();
            GetClient(clientId).WorkoutDetail = workoutDetail;

            clientRepo.Save();
        }

        public void AddInfoToClient(ExtraInfo extraInfo, string clientId)
        {
            GetClient(clientId).AdditionalInfo.Add(extraInfo);
            clientRepo.Save();
        }

        public void RemoveDetailFromClient(WorkoutDetail workoutDetail, string clientId )
        {
            GetClient(clientId).WorkoutDetail = null;
            clientRepo.Save();
        }

        public void RemoveInfoFromClient(ExtraInfo extraInfo, string clientId)
        {
            GetClient(clientId).AdditionalInfo.Remove(extraInfo);
            clientRepo.Save();
        }

        public float AverageAgeOfClients(string trainerId)
        {
            var query = from clients in clientRepo.Read()
                        where clients.Trainer.TrainerID == trainerId
                        select clients.Age;

            float helper = 0;
            foreach (int item in query)
            {
                helper += item;
            }
            return helper / query.Count();
        }

        public void valamiidkyet(GymClient client)
        {

            var query = from x in detailRepo.Read()
                        join y in infoRepo.Read() on x.GymClient equals y.GymClient
                        group x by x.GymClient.FullName into g
                        select g.Key;


        }

        #endregion

    }
}
