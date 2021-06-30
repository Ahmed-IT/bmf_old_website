using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BMF.Shared.Services.BMF.WebsiteCMS
{
    public interface IBRSubCategoryService
    {
        public List<SelectListItem> Get_SelectList();
    }
}
