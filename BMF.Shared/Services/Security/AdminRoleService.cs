using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using BMF.Shared.Entities.Security;
using BMF.Shared.Services.Data;

namespace BMF.Shared.Services.Security
{
    public partial class AdminRoleService : IAdminRoleService
    {
        private readonly IRepository<AdminRole> _repository;

        #region Ctor

        public AdminRoleService(IRepository<AdminRole> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Methods

        public List<AdminRole> GetAll(List<long> SelectedItems = null, bool showDeleted = false)
        {
            var Items = _repository.QTable.ToList().Select(s =>
              {
                  s.Selected_List = SelectedItems;
                  return s;
              }).ToList();
            if (!showDeleted)
                Items = Items.Where(w => !w.DeletedOnUtc.HasValue).ToList();
            return Items;
        }

        public List<SystemOperation> Get_AssignedOperations(AdminRole adminRole)
        {
            return _repository.Entry(adminRole)
           .Collection(c => c.AssignedOperations)
           .Query()
           .Include(i => i.Operation)
           .Where(w => w.IsActive && !w.DeletedOnUtc.HasValue && w.Operation.IsActive)
           .Select(s => s.Operation)
           .ToList();
        }

        #endregion
    }
}
