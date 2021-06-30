using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BMF.Shared.Services.UserManagement
{
    public interface IUserProfileService
    {
        public List<SelectListItem> Get_SelectList();
    }
}
