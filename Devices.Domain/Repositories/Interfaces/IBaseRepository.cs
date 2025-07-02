using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Devices.Domain.Repositories.Interfaces
{
    internal interface IBaseRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> Add(TEntity obj);
        Task<List<TEntity>> AddMany(List<TEntity> objs);
        Task<TEntity> GetById(string id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Update(string id, TEntity obj);
        Task<TEntity> AddOrUpdate(string id, TEntity obj);
        Task<bool> Remove(string id);
        IQueryable<TEntity> Read();
        IMongoCollection<TEntity> ReadCollection();
    }
}
