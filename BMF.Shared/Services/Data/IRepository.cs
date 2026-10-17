using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;
using BMF.Shared.Entities;

namespace BMF.Shared.Services.Data
{
    public partial interface IRepository<TEntity> where TEntity : BaseEntity
    {
        DbSet<TEntity> QTable { get; }

        EntityEntry<TEntity> Entry(TEntity entity);

        Task<int> SaveChangesAsync();

        //ValueTask<TEntity> Get_ByFindAsync(params object[] keyValues);
    }
}
