using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMF.Shared.Entities.Security;
using BMF.Shared.Services.Data;

namespace BMF.Shared.Services.Security
{
    public partial class AdminUserService : IAdminUserService
    {
        private readonly IRepository<AdminUser> _repository;

        #region Ctor

        public AdminUserService(IRepository<AdminUser> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Methods

        public List<AdminUser> GetAll(List<long> SelectedItems = null, bool showDeleted = false)
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

        public AdminUser Get_ByID(long ID)
        {
            var query = _repository.QTable.Where(w => w.ID == ID && !w.DeletedOnUtc.HasValue).SingleOrDefault();
            return query;
        }

        public AdminUser Get_ByEmail(string email)
        {
            var query = _repository.QTable.Where(w => w.Email.ToLower().Trim() == email.ToLower().Trim() && !w.DeletedOnUtc.HasValue).SingleOrDefault();
            return query;
        }

        public AdminUser Get_ByUsername(string username)
        {
            var query = _repository.QTable.Where(w => w.Username.ToLower().Trim() == username && !w.DeletedOnUtc.HasValue).SingleOrDefault();
            return query;
        }

        public List<AdminRole> Get_AssignedRoles(AdminUser adminUser)
        {
            return _repository.Entry(adminUser)
                .Collection(c => c.AssignedRoles)
                .Query()
                .Include(i => i.Role)
                .Where(w => w.IsActive && !w.DeletedOnUtc.HasValue && w.Role.IsActive && !w.Role.DeletedOnUtc.HasValue)
                .Select(s => s.Role)
                .ToList();
        }

        public SystemUserLoginResult ValidateAuthentication(string username, string password)
        {
            AdminUser validationRecord = null;
            SystemUserLoginResult systemLoginResult = new SystemUserLoginResult()
            {
                systemUserType = SystemUserType.Administrator
            };

            // Try by username
            systemLoginResult.systemUserLoginType = SystemUserLoginType.username;
            validationRecord = _repository.QTable.Where(w => w.Username.ToLower().Trim() == username.ToLower().Trim()).SingleOrDefault();

            if (validationRecord == null)
            {
                // Failed by username
                systemLoginResult.systemUserLoginResultStatus = SystemUserLoginResultStatus.AccountNotExist;
                // Try by email
                systemLoginResult.systemUserLoginType = SystemUserLoginType.email;
                validationRecord = _repository.QTable.Where(w => w.Email.ToLower().Trim() == username.ToLower().Trim()).SingleOrDefault();
            }

            if (validationRecord != null)
            {
                if (validationRecord.DeletedOnUtc.HasValue)
                    systemLoginResult.systemUserLoginResultStatus = SystemUserLoginResultStatus.Deleted;
                else if (!validationRecord.IsActive)
                    systemLoginResult.systemUserLoginResultStatus = SystemUserLoginResultStatus.NotActive;
                else
                {
                    byte[] _Password = Encoding.ASCII.GetBytes(password + validationRecord.ID);
                    _Password = new System.Security.Cryptography.SHA256Managed().ComputeHash(_Password);
                    if (!string.Equals(Convert.ToBase64String(_Password), validationRecord.Password))
                    {
                        systemLoginResult.systemUserLoginResultStatus = SystemUserLoginResultStatus.WrongPassword;
                    }
                    else
                    {
                        systemLoginResult.systemUserLoginResultStatus = SystemUserLoginResultStatus.Successful;
                    }
                }
            }
            return systemLoginResult;
        }

        #endregion
    }
}
