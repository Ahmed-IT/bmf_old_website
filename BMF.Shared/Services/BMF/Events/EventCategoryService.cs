using BMF.Shared.Entities.Events;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMF.Shared.Services.Events
{
    public class EventCategoryService : IEventCategoryService
    {
        private readonly IRepository<M_EventCategory> _repository;

        #region Ctor

        public EventCategoryService(IRepository<M_EventCategory> repository)
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
                        Text = $"{s.Title}"
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
