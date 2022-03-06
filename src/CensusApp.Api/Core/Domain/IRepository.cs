using System.Threading.Tasks;

namespace CensusApp.Api.Core.Domain
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        void Insert(TEntity obj);
        Task InsertAsync(TEntity obj);
        void Update(TEntity obj);
        Task UpdateAsync(TEntity obj);
        void Delete(TEntity obj);
        TEntity Get(object id);

    }
}
