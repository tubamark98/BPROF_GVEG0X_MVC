using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repos
{
    public class TrainerRepository : IRepoBase<Models.Trainer>
    {
        readonly GymContext context = new GymContext();

        public void Add(Trainer item)
        {
            context.Trainers.Add(item);
            Save();
        }

        public void Delete(Trainer item)
        {
            context.Trainers.Remove(item);
            Save();
        }

        public void Delete(string id)
        {
            Delete(GetItem(id));
        }

        public Trainer GetItem(string trainerID)
        {
            return context.Trainers.FirstOrDefault(t => t.TrainerID == trainerID);
        }

        public IQueryable<Trainer> Read()
        {
            return context.Trainers.AsQueryable();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(string trainedID, Trainer newItem)
        {
            var oldItem = GetItem(trainedID);
            oldItem.TrainerName = newItem.TrainerName;
            oldItem.GymClients.Clear();

            foreach (var item in newItem.GymClients)
                oldItem.GymClients.Add(item);
             
            Save();
        }
    }
}
