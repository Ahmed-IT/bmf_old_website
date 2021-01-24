using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using BMF.Shared.Services.Security;

namespace BMF.Shared.Core.WebFramework
{
    public sealed class AuthorizeOperationAttribute : TypeFilterAttribute
    {
        #region Fields

        private readonly bool _ignoreFilter;
        private readonly string _systemOperation;

        #endregion

        #region Properties

        public bool IgnoreFilter => _ignoreFilter;
        public string SystemOperation => _systemOperation;

        #endregion

        #region Ctor

        public AuthorizeOperationAttribute(bool ignore = false, string systemOperation = null) : base(typeof(AuthorizeOperationFilter))
        {
            _ignoreFilter = ignore;
            _systemOperation = systemOperation;
            Arguments = new object[] { ignore, systemOperation };
        }

        #endregion

        #region Nested filter

        private class AuthorizeOperationFilter : IAuthorizationFilter
        {
            #region Fields

            private readonly bool _ignoreFilter;
            private readonly string _systemOperation;
            private readonly IPermissionService _permissionService;

            #endregion

            #region Ctor

            public AuthorizeOperationFilter(bool ignoreFilter, string systemOperation, IPermissionService permissionService)
            {
                _ignoreFilter = ignoreFilter;
                _systemOperation = systemOperation;
                _permissionService = permissionService;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Called early in the filter pipeline to confirm request is authorized
            /// </summary>
            /// <param name="filterContext">Authorization filter context</param>
            public void OnAuthorization(AuthorizationFilterContext filterContext)
            {
                if (filterContext == null)
                    throw new ArgumentNullException(nameof(filterContext));

                //check whether this filter has been overridden for the action
                var actionFilter = filterContext.ActionDescriptor.FilterDescriptors
                    .Where(filterDescriptor => filterDescriptor.Scope == FilterScope.Action)
                    .Select(filterDescriptor => filterDescriptor.Filter).OfType<AuthorizeOperationAttribute>().FirstOrDefault();

                //ignore filter (the action is available even if a customer hasn't access to the admin area)
                if (actionFilter?.IgnoreFilter ?? _ignoreFilter)
                    return;

                if (_systemOperation == null)
                    throw new ArgumentNullException(nameof(_systemOperation));

                //there is AuthorizeOperationFilter, so check access
                if (filterContext.Filters.Any(filter => filter is AuthorizeOperationFilter))
                {
                    //authorize permission of access to the admin area
                    if (!_permissionService.Authorize(_systemOperation))
                        filterContext.Result = new ChallengeResult();
                }
            }

            #endregion
        }

        #endregion
    }
}
