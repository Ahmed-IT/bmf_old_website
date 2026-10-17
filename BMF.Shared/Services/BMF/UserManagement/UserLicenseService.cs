using BMF.Shared.Entities.UserManagement;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMF.Shared.Services.UserManagement
{
    public class UserLicenseService : IUserLicenseService
    {
        private readonly IRepository<M_UserLicense> _repository;

        #region Ctor

        public UserLicenseService(IRepository<M_UserLicense> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Methods

        public List<SelectListItem> Get_SelectList()
        {
            List<SelectListItem> Source = new List<SelectListItem>();
            try
            {
                Source = _repository.QTable
                    .Where(w => w.IsActive && !w.DeletedOnUtc.HasValue)
                    .Include(i => i.LicenseType)
                    .Include(i => i.UserProfile)
                    .ToList()
                    .Select(s => new SelectListItem()
                    {
                        Value = s.ID.ToString(),
                        Disabled = s.IsActive,
                        Text = $"(Request ID: {s.ID}) L.No.: {s.License_Number} - L.Type: {s.LicenseType.Title} - {s.UserProfile.FullName} (CPR: {s.UserProfile.CPR})"
                    }).OrderBy(o => o.Text).ToList();
            }
            catch (Exception LogError)
            {
                Source = null;
            }
            return Source;
        }

        #endregion
    }
}
