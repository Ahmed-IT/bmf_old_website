using BMF.Shared.Entities.Events;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMF.Shared.Services.Events
{
    public class EventRoundSessionService : IEventRoundSessionService
    {
        private readonly IRepository<M_EventRoundSession> _repository;

        #region Ctor

        public EventRoundSessionService(IRepository<M_EventRoundSession> repository)
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
                    .Include(i => i.EventRound)
                    .Include(i => i.EventRound.EventCategory)
                    .ToList()
                    .Select(s => new SelectListItem()
                    {
                        Value = s.ID.ToString(),
                        Disabled = s.IsActive,
                        Text = $"Category: {s.EventRound.EventCategory.Title} - Round Number: {s.EventRound.Round_Number} - Session: {s.Title}"
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
