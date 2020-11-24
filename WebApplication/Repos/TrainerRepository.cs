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
            throw new NotImplementedException();
        }

        public Trainer GetItem(string gymID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Trainer> Read()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(string gymID, Trainer newItem)
        {
            throw new NotImplementedException();
        }
    }
}
