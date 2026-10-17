using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace BMF.Shared.Services.Location
{
    public interface ILocationCountryService
    {
        public List<SelectListItem> Get_SelectList();
    }
}
