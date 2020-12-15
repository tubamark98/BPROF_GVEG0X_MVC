using Models;
using Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class ExtraInfoLogic
    {
        IRepoBase<GymClient> clientRepo;
        IRepoBase<Trainer> trainerRepo;
        IRepoBase<WorkoutDetail> detailRepo;
        IRepoBase<ExtraInfo> infoRepo;
        public ExtraInfoLogic(IRepoBase<GymClient> clientRepo, IRepoBase<Trainer> trainerRepo,
            IRepoBase<WorkoutDetail> detailRepo, IRepoBase<ExtraInfo> infoRepo)
        {
            this.clientRepo = clientRepo;
            this.trainerRepo = trainerRepo;
            this.detailRepo = detailRepo;
            this.infoRepo = infoRepo;
        }

        #region CRUD methods
        public void AddInfo(ExtraInfo extraInfo)
        { 
            this.infoRepo.Add(extraInfo);
        }

        public void DeleteInfo(string infoId)
        {
            this.infoRepo.Delete(infoId);
        }

        public IQueryable<ExtraInfo> GetInfos()
        {
            return infoRepo.Read();
        }

        public ExtraInfo GetInfo(string infoId)
        {
            return infoRepo.GetItem(infoId);
        }

        public void UpdateInfo(string infoId, ExtraInfo extraInfo)
        {
            infoRepo.Update(infoId, extraInfo);
        }
        #endregion


        #region Non-Crud methods




        #endregion
    }
}
