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
            GetClient(clientId).WorkoutDetail.GymID = clientId;

            clientRepo.Save();
        }

        public void AddInfoToClient(ExtraInfo extraInfo, string clientId)
        {
            extraInfo.GymID = clientId;
            GetClient(clientId).ExtraInfos.Add(extraInfo);

            clientRepo.Save();
        }

        public void RemoveDetailFromClient(WorkoutDetail workoutDetail, string clientId )
        {
            GetClient(clientId).WorkoutDetail.GymID = null;
            clientRepo.Save();
        }

        public void RemoveInfoFromClient(ExtraInfo extraInfo, string clientId)
        {
            GetClient(clientId).ExtraInfos.Remove(extraInfo);
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

        public void FillDbWithSamples()
        {
            WorkoutDetail d1 = new WorkoutDetail()
            {
                WorkoutId = "detail00",
                ContestDiets = ContestDiets.lowCarb,
                WorkoutType = WorkoutTypes.calisthenics
            };
            WorkoutDetail d2 = new WorkoutDetail()
            {
                WorkoutId = "detail01",
                ContestDiets = ContestDiets.intermittentFasting,
                WorkoutType = WorkoutTypes.powerlifting
            };
            WorkoutDetail d3 = new WorkoutDetail()
            {
                WorkoutId = "detail02",
                ContestDiets = ContestDiets.carbCycling,
                WorkoutType = WorkoutTypes.calisthenics
            };

            ExtraInfo i1 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Nem szeret lábazni" };
            ExtraInfo i2 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "váll problémái vannak" };
            ExtraInfo i3 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Nem szeret élni" };
            ExtraInfo i4 = new ExtraInfo() { InfoId = Guid.NewGuid().ToString(), Information = "Depressziós" };

            AddDetailToClient(d1,"test01");
            AddDetailToClient(d2,"test02");
            AddDetailToClient(d3,"test03");

            AddInfoToClient(i1, "test01");
            AddInfoToClient(i2, "test02");
            AddInfoToClient(i3, "test03");
            AddInfoToClient(i4, "test04");

        }

        #endregion

    }
}
