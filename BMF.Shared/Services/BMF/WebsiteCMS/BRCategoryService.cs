using BMF.Shared.Entities.Website;
using BMF.Shared.Services.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMF.Shared.Services.BMF.WebsiteCMS
{
    public class BRCategoryService : IBRCategoryService
    {
        private readonly IRepository<M_BRCategory> _repository;

        #region Ctor

        public BRCategoryService(IRepository<M_BRCategory> repository)
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
