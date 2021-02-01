using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BMF.Shared.Services.Events
{
    public interface IEventCategoryService
    {
        public List<SelectListItem> Get_SelectList();
    }
}
