using BMF.Shared.Entities.UserManagement;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMF.Shared.Services.UserManagement
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IRepository<M_UserProfile> _repository;

        #region Ctor

        public UserProfileService(IRepository<M_UserProfile> repository)
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
                    .ToList()
                    .Select(s => new SelectListItem()
                    {
                        Value = s.ID.ToString(),
                        Disabled = s.IsActive,
                        Text = $"{s.FullName} (CPR: {s.CPR})"
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
