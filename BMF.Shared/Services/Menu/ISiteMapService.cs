using System.Collections.Generic;
using BMF.Shared.Entities.Security;

namespace BMF.Shared.Services.Menu
{
    public interface ISiteMapService
    {
        public List<SiteMapNode> SiteMapCollection();
    }
}
