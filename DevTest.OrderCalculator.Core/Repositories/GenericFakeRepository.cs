using DevTest.OrderCalculator.Core.Helpers;
using DevTest.OrderCalculator.Core.Models;
using System.Collections.Generic;

namespace DevTest.OrderCalculator.Core.Repositories
{
    public abstract class GenericFakeRepository<T>
        : IRepository<T> where T : EntityBase
    {
        private readonly ILogger _logger;

        public GenericFakeRepository(ILogger logger)
        {
            _logger = logger;
        }

        public virtual IEnumerable<T> GetAll()
        {
            _logger.Log("Fake getting all entities");
            return new List<T>();
        }

        public virtual T Add(T entity)
        {
            _logger.Log("Fake adding entity");
            return entity;
        }

        public abstract T Get(long id);

        public virtual T Update(T entity)
        {
            _logger.Log("Fake update entity");
            return entity;
        }

        public virtual void SaveChanges()
        {
            _logger.Log("Fake saving changes");
        }
    }
}
