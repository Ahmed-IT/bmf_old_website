using BMF.Shared.Entities.Location;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMF.Shared.Services.Location
{
    public class LocationCountryService : ILocationCountryService
    {
        private readonly IRepository<Country> _repository;

        #region Ctor

        public LocationCountryService(IRepository<Country> repository)
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
                        Text = $"{s.Name}"
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
