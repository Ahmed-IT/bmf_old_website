using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BMF.Shared.Services.Events
{
    public interface IEventRoundService
    {
        public List<SelectListItem> Get_SelectList();
    }
}
