using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BMF.Shared.Services.Events
{
    public interface IEventRoundSessionService
    {
        public List<SelectListItem> Get_SelectList();
    }
}
