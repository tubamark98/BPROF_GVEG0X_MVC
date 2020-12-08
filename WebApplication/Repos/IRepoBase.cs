using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repos
{
    public interface IRepoBase<T> where T:new()
    {
        void Add(T item);
        void Delete(T item);
        void Delete(string id);
        IQueryable<T> Read();
        void Update(string gymID, T newItem);
        T GetItem(string gymID);
        void Save();

    }
}
