using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;
using BMF.Shared.Entities;

namespace BMF.Shared.Services.Data
{
    /// <summary>
    /// Entity Framework Core Repository
    /// This class is to be used to communicate with an Entity Framework Database Conext rather than use the DbContext directly.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EFRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly BMFDbContext _context;

        public EFRepository(BMFDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a queryable table
        /// </summary>
        public DbSet<TEntity> QTable => _context.Set<TEntity>();

        /// <summary>
        /// Gets an instance of the record to perform more operations on the entity itself (such as load related collections)
        /// </summary>
        public EntityEntry<TEntity> Entry(TEntity entity) => _context.Entry(entity);

        /// <summary>
        /// Call save changes on the database layer
        /// </summary>
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        //public ValueTask<TEntity> Get_ByFindAsync(params object[] keyValues) => QTable.FindAsync(keyValues);
    }
}
