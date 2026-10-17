using _Standards.Library.Helpers;
using _Standards.Library.Security;
using BMF.Shared.Models.AccessManagement;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BMF.Web.Admin.Database
{
    public class DataFunctions : DisposableClass
    {
        private BMFDbContext _TheDatabase = new BMFDbContext();

        #region Sidebar Menu - Default System Functions

        public string Get_URLClean(string URL)
        {
            return Get_URLWithoutQueryString(URL);
        }

        public string Get_URLWithoutQueryString(string URL)
        {
            return URL.Split('?').FirstOrDefault();
        }

        public List<long> Get_SystemFeaturePathIDs(string URL)
        {
            List<long> Path = new List<long>();
            if (string.IsNullOrEmpty(URL))
                return null;
            S_Feature _PathItem = Get_SystemFeatureLastURL(URL);
            if (_PathItem != null)
            {
                Path.Add(_PathItem.ID);
                while (_PathItem.ID_ParentFeature.HasValue)
                {
                    var _Temp = Get_SystemFeature(_PathItem.ID_ParentFeature.Value);
                    if (_Temp != null)
                    {
                        Path.Add(_Temp.ID);
                        _PathItem = _Temp;
                    }
                }
            }
            return Path;
        }

        public S_Feature Get_SystemFeaturePath(string URL)
        {
            if (string.IsNullOrEmpty(URL))
                return null;
            S_Feature Path = Get_SystemFeatureLastURL(URL);
            if (Path == null)
                return null;
            while (Path.ID_ParentFeature.HasValue)
            {
                var _Temp = Get_SystemFeature(Path.ID_ParentFeature.Value);
                if (_Temp != null)
                {
                    _Temp.Children.Add(Path);
                    Path = _Temp;
                }
            }
            return Path;
        }

        public S_Feature Get_SystemFeatureLastURL(string URL)
        {
            var _SystemFeature = (from Src in _TheDatabase.Table_SystemFeature
                                  where
                                  Src.IsActive
                                  && Src.DeletedOnUtc == null
                                  orderby Src.Level descending
                                  select Src).AsNoTracking().ToList().Where(w => w.URL.ToLower() == URL.TrimEnd('/').ToLower()).Select(s => s).FirstOrDefault();
            return _SystemFeature;
        }

        public long Get_SystemFeatureLastURLID(string URL)
        {
            URL = URL.TrimEnd('/');
            var _SystemFeature = (from Src in _TheDatabase.Table_SystemFeature
                                  where
                                  Src.IsActive
                                  && Src.URL.Equals(URL, StringComparison.OrdinalIgnoreCase)
                                  && Src.DeletedOnUtc == null
                                  orderby Src.Level descending
                                  select Src).AsNoTracking().FirstOrDefault();
            return (_SystemFeature != null ? _SystemFeature.ID : 0);
        }

        public List<S_Feature> GetAll_SystemFeatures_SidebarMenu(string Path)
        {
            List<S_Feature> TheFunctions = new List<S_Feature>();
            List<long> Cleared = AuthenticationManager.GetAll_Features();
            try
            {
                var Selected = Get_SystemFeaturePathIDs(Path);
                TheFunctions = GetAll_SystemFeaturesNested(true, Selected, Cleared);
            }
            catch (Exception LogError)
            {

            }
            return TheFunctions;
        }

        public List<S_Feature> GetAll_ChildFunctions(long ID)
        {
            List<S_Feature> ChildrenCollection = new List<S_Feature>();
            try
            {
                ChildrenCollection = (from Src in _TheDatabase.Table_SystemFeature
                                      where
                                      Src.Visibility
                                      && Src.IsActive
                                      && Src.DeletedOnUtc == null
                                      && Src.ID_ParentFeature == ID
                                      orderby Src.Order
                                      select Src).ToList().Select(s => new _SystemFeature(s)
                                      {
                                          Cleared = true,
                                          Features = GetAll_ChildFunctions(s.ID),
                                      }).DistinctBy(i => i.ID).ToList();
            }
            catch (Exception LogError)
            {
                //throw;
            }
            return ChildrenCollection;
        }

        public S_Feature Get_SystemFeature(long ID, bool IncludeChildren = false)
        {

            var _SystemFeature = (from SF in _TheDatabase.Table_SystemFeature
                                  where
                                  SF.ID == ID
                                  && SF.IsActive
                                  && SF.DeletedOnUtc == null
                                  select SF).AsNoTracking().ToList().Select(s => new _SystemFeature(s, IncludeChildren)).SingleOrDefault();
            return _SystemFeature;
        }

        #endregion

        #region System Features

        public List<S_Feature> GetAll_SystemFeatures(bool IncludeChildren = false)
        {
            try
            {
                var List = (from Src in _TheDatabase.Table_SystemFeature
                            where
                            Src.DeletedOnUtc == null
                            select Src).ToList().Select(s => new _SystemFeature(s, IncludeChildren)).ToList();
                return List;
            }
            catch (Exception LogError)
            {
                return null;
            }
        }

        public List<S_Feature> GetAll_SystemFeaturesNested(bool IncludeChildren = false, List<long> Selected = null, List<long> Cleared = null)
        {
            try
            {
                var List = (from Src in _TheDatabase.Table_SystemFeature
                            where
                            Src.DeletedOnUtc == null
                            && Src.ID_ParentFeature == null
                            orderby Src.Order
                            select Src).ToList().Select(s => new _SystemFeature(s, IncludeChildren, Selected, Cleared)).ToList();
                return List;
            }
            catch (Exception LogError)
            {
                return null;
            }
        }

        #endregion

        #region System Roles

        public List<S_Role> GetAll_SystemRoles(bool IncludeFeatures = false, bool IncludeUsers = false, List<long> Selected = null)
        {
            try
            {
                var List = (from Src in _TheDatabase.Table_SystemRole
                            where
                            Src.DeletedOnUtc == null
                            select Src).ToList().Select(s => new _SystemRole(s, IncludeFeatures, IncludeUsers, Selected)).ToList();
                return List;
            }
            catch (Exception LogError)
            {
                return null;
            }
        }

        public S_Role Get_SystemRole(long ID, bool IncludeFeatures = false, bool IncludeUsers = false)
        {
            try
            {
                var List = (from Src in _TheDatabase.Table_SystemRole
                            where
                            Src.ID == ID
                            && Src.DeletedOnUtc == null
                            select Src).ToList().Select(s => new _SystemRole(s, IncludeFeatures, IncludeUsers)).SingleOrDefault();
                return List;
            }
            catch (Exception LogError)
            {
                return null;
            }
        }

        #endregion

        #region System Users

        public List<S_User> GetAll_SystemUsers(bool IncludeRoles = false, List<long> Selected = null)
        {
            try
            {
                var List = (from Src in _TheDatabase.Table_SystemUser
                            where
                            Src.DeletedOnUtc == null
                            select Src).ToList().Select(s => new _SystemUser(s, IncludeRoles, Selected)).ToList();
                return List;
            }
            catch (Exception LogError)
            {
                return null;
            }
        }

        public S_User Get_SystemUser(long ID, bool IncludeRoles = false)
        {
            try
            {
                var List = (from Src in _TheDatabase.Table_SystemUser
                            where
                            Src.ID == ID
                            && Src.DeletedOnUtc == null
                            select Src).ToList().Select(s => new _SystemUser(s, IncludeRoles)).SingleOrDefault();
                return List;
            }
            catch (Exception LogError)
            {
                return null;
            }
        }

        #endregion

    }
}