using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repos
{
    public class InfoRepository : IRepoBase<Models.ExtraInfo>
    {
        GymContext context = new GymContext();
        public void Add(ExtraInfo item)
        {
            context.ExtraInfos.Add(item);
            Save();
        }

        public void Delete(ExtraInfo item)
        {
            context.ExtraInfos.Remove(item);
            Save();
        }

        public void Delete(string id)
        {
            Delete(GetItem(id));
        }

        public ExtraInfo GetItem(string gymID)
        {
            return context.ExtraInfos.FirstOrDefault(t => t.InfoId == gymID);
        }

        public IQueryable<ExtraInfo> Read()
        {
            return context.ExtraInfos.AsQueryable();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(string gymID, ExtraInfo newItem)
        {
            var oldItem = GetItem(gymID);
            oldItem.Information = newItem.Information;
            Save();
        }
    }
}
