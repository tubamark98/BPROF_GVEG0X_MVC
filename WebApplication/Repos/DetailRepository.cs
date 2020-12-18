using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repos
{
    public class DetailRepository : IRepoBase<Models.WorkoutDetail>
    {
        GymContext context = new GymContext();
        public void Add(WorkoutDetail item)
        {
            context.WorkoutDetails.Add(item);
            Save();
        }

        public void Delete(WorkoutDetail item)
        {
            context.WorkoutDetails.Remove(item);
            Save();
        }

        public void Delete(string id)
        {
            Delete(GetItem(id));
        }

        public WorkoutDetail GetItem(string gymID)
        {
            return context.WorkoutDetails.FirstOrDefault(t => t.WorkoutId == gymID);
        }

        public IQueryable<WorkoutDetail> Read()
        {
            return context.WorkoutDetails.AsQueryable();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(string gymID, WorkoutDetail newItem)
        {
            var oldItem = GetItem(gymID);
            oldItem.ContestDiets = newItem.ContestDiets;
            oldItem.WorkoutType = newItem.WorkoutType;

            Save();
        }
    }
}
