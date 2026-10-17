using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BMF.Shared.Services.UserManagement
{
    public interface IMarshalRoleService
    {
        public List<SelectListItem> Get_SelectList();
    }
}
