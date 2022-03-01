using System.Threading.Tasks;

namespace CensusApp.Api.Core.Domain._Base
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity obj);
        Task AddAsync(TEntity obj);
        void Update(TEntity obj);
        Task UpdateAsync(TEntity obj);
        void Delete(TEntity obj);
        TEntity Get(object id);

    }
}
