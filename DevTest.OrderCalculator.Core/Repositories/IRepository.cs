using DevTest.OrderCalculator.Core.Models;
using System.Collections.Generic;

namespace DevTest.OrderCalculator.Core.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        T Add(T entity);
        T Update(T entity);
        void SaveChanges();
    }
}
