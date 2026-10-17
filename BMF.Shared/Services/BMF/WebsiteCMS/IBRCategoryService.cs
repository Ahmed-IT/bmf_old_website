using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BMF.Shared.Services.BMF.WebsiteCMS
{
    public interface IBRCategoryService
    {
        public List<SelectListItem> Get_SelectList();
    }
}
