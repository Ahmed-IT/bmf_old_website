using System.Collections.Generic;
using BMF.Shared.Entities.Security;

namespace BMF.Shared.Services.Security
{
    public interface IAdminUserService
    {
        public List<AdminUser> GetAll(List<long> SelectedItems = null, bool showDeleted = false);

        public AdminUser Get_ByID(long ID);

        public AdminUser Get_ByEmail(string email);

        public AdminUser Get_ByUsername(string username);

        public List<AdminRole> Get_AssignedRoles(AdminUser adminUser);

        public SystemUserLoginResult ValidateAuthentication(string username, string password);
    }
}
