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
            throw new NotImplementedException();
        }

        public void Delete(Trainer item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Trainer> Read()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(string gymID, Trainer newItem)
        {
            throw new NotImplementedException();
        }
    }
}
