using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace BMF.Shared.Entities.Security
{
    public class SiteMapNode
    {
        #region Fields

        /// <summary>
        /// Gets or sets the route values.
        /// </summary>
        public RouteValueDictionary RouteValues { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the system name.
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the name of the controller.
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the icon class (Font Awesome: http://fontawesome.io/)
        /// </summary>
        public string IconClass { get; set; }

        /// <summary>
        /// Gets or sets the item is visible
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the item is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to open url in new tab (window) or not
        /// </summary>
        public bool OpenUrlInNewTab { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        /// Gets or sets the child nodes.
        /// </summary>
        public IList<SiteMapNode> ChildNodes { get; set; }

        #endregion

        #region Ctor

        public SiteMapNode()
        {
            RouteValues = new RouteValueDictionary();
            ChildNodes = new List<SiteMapNode>();
        }

        #endregion
    }
}
