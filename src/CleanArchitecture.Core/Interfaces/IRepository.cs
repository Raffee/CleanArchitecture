using System.Collections.Generic;
using CleanArchitecture.Core.SharedKernel;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IRepository<T, TId> where T : BaseEntity<TId>
    {
        T GetById(TId id);
        List<T> List();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}