using System.Collections.Generic;
using BMF.Shared.Entities.Security;

namespace BMF.Shared.Services.Security
{
    public interface IAdminRoleService
    {
        public List<AdminRole> GetAll(List<long> SelectedItems = null, bool showDeleted = false);

        public List<SystemOperation> Get_AssignedOperations(AdminRole adminRole);
    }
}
