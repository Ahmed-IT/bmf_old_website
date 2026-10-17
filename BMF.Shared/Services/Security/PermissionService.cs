using System;
using System.Linq;
using BMF.Shared.Core;
using BMF.Shared.Entities.Security;
using BMF.Shared.Services.Data;

namespace BMF.Shared.Services.Security
{
    public class PermissionService : IPermissionService
    {
        private readonly IWorkContext _workContext;
        private readonly IRepository<AdminUser> _adminUserRepository;

        public PermissionService(IWorkContext workContext,
            IRepository<AdminUser> adminUserRepository)
        {
            _workContext = workContext;
            _adminUserRepository = adminUserRepository;
        }

        public bool Authorize(string operationSystemName)
        {
            bool isAuthorized = false;
            if (_workContext == null)
                return false;
            switch (_workContext.CurrentSystemUserType)
            {
                case SystemUserType.Administrator:
                    isAuthorized = _adminUserRepository.QTable.Where(w => w.ID == _workContext.CurrentAdminUser.ID)
                        .SelectMany(s => s.AssignedRoles.Where(w => w.IsActive && !w.DeletedOnUtc.HasValue).Select(s => s.Role))
                        .Where(w => w.IsActive && !w.DeletedOnUtc.HasValue)
                        .Any(s => s.AssignedOperations.Where(w => w.IsActive && !w.DeletedOnUtc.HasValue).Select(s => s.Operation.SystemName).Contains(operationSystemName));
                    return isAuthorized;
                case SystemUserType.None:
                default:
                    break;
            }
            return isAuthorized;
        }
    }
}
