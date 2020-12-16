using Models;
using Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class WorkoutDetailLogic
    {
        IRepoBase<WorkoutDetail> detailRepo;

        public WorkoutDetailLogic(IRepoBase<WorkoutDetail> detailRepo)
        {
            this.detailRepo = detailRepo;
        }

        #region CRUD methods
        public void AddDetail(WorkoutDetail workoutDetail)
        {
            this.detailRepo.Add(workoutDetail);
        }

        public void DeleteDetail(string detailId)
        {
            this.detailRepo.Delete(detailId);
        }

        public IQueryable<WorkoutDetail> GetDetails()
        {
            return detailRepo.Read();
        }

        public WorkoutDetail GetDetail(string detailId)
        {
            return detailRepo.GetItem(detailId);
        }

        public void UpdateDetail(string detailId, WorkoutDetail workoutDetail)
        {
            detailRepo.Update(detailId, workoutDetail);
        }
        #endregion

        #region Non-Crud methods




        #endregion
    }
}
