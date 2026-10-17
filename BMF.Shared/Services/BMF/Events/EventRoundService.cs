using BMF.Shared.Entities.Events;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMF.Shared.Services.Events
{
    public class EventRoundService : IEventRoundService
    {
        private readonly IRepository<M_EventRound> _repository;

        #region Ctor

        public EventRoundService(IRepository<M_EventRound> repository)
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
                    .Include(i => i.EventCategory)
                    .Include(i => i.EventSeason)
                    .ToList()
                    .Select(s => new SelectListItem()
                    {
                        Value = s.ID.ToString(),
                        Disabled = s.IsActive,
                        Text = $"Season: {s.EventSeason.Title} - Category: {s.EventCategory.Title} - Round Number: {s.Round_Number}"
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
