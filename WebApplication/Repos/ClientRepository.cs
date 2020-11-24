using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repos
{
    public class ClientRepository : IRepoBase<Models.GymClient>
    {
        readonly GymContext context = new GymContext();
        public void Add(GymClient item)
        {
            context.GymClients.Add(item);
            Save();
        }

        public void Delete(GymClient item)
        {
            context.GymClients.Remove(item);
            Save();
        }

        public System.Linq.IQueryable<GymClient> Read()
        {
            return context.GymClients.AsQueryable();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(string gymID, GymClient newItem)
        {
            throw new NotImplementedException();
        }
    }
}
