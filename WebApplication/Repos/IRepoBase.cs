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
        IQueryable<T> Read();
        void Update(string gymID, T newItem);
        void Save();
    }
}
