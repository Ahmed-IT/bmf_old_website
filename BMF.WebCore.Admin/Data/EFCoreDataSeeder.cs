using BMF.Shared.Entities.Location;
using BMF.Shared.Entities.Security;
using BMF.Shared.Services.Data;
using BMF.Shared.Services.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BMF.WebCore.Admin.Data
{
    public class EFCoreDataSeeder : IDataSeeder
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISystemOperationService _systemOperationService;

        public EFCoreDataSeeder(
            IServiceProvider serviceProvider,
            ISystemOperationService systemOperationService
            )
        {
            _serviceProvider = serviceProvider;
            _systemOperationService = systemOperationService;
        }

        public async Task SeedDatabase()
        {
            try
            {
                using var context = new BMFDbContext(_serviceProvider.GetRequiredService<DbContextOptions<BMFDbContext>>());

                #region System Operations
                try
                {
                    if (context != null && _systemOperationService != null)
                    {
                        if (!context.Set<SystemOperation>().Any())
                        {
                            context.Set<SystemOperation>().AddRange(_systemOperationService.GetAll_Local(nested: true));
                            await context.SaveChangesAsync();
                        }
                        else
                        {
                            var FullList = _systemOperationService.GetAll_Local(nested: false);
                            var FullList_SystemNamesOnly = FullList.Select(s => s.SystemName).ToList();
                            var CurrentList_SystemNamesOnly = context.Set<SystemOperation>().Select(s => s.SystemName).ToList();

                            if (context.Set<SystemOperation>().Count() != FullList.Count || !FullList_SystemNamesOnly.All(CurrentList_SystemNamesOnly.Contains))
                            {
                                // Get All Currrent Operations
                                var _currentOperations = context.Set<SystemOperation>().ToList();
                                // Get Removed Operations
                                var _removedOperations = _currentOperations.Where(w => !FullList_SystemNamesOnly.Contains(w.SystemName)).ToList();
                                // Get New Operations
                                var _newOperations = FullList.Where(w => !CurrentList_SystemNamesOnly.Contains(w.SystemName)).ToList();

                                // Process New Parents First Without Children
                                // In case old dependents rely on them 
                                // Children will be processed later anyways
                                foreach (var SingleNewOperation in _newOperations.Where(w => w.Parent == null))
                                {
                                    context.Set<SystemOperation>().Add(new SystemOperation() { IsActive = true, SystemName = SingleNewOperation.SystemName });
                                }
                                await context.SaveChangesAsync();

                                while (_newOperations.Count > 0)
                                {
                                    CurrentList_SystemNamesOnly = context.Set<SystemOperation>().Select(s => s.SystemName).ToList();
                                    _newOperations = FullList.Where(w => !CurrentList_SystemNamesOnly.Contains(w.SystemName)).ToList();
                                    // Process New SubNodes (SubOperation)
                                    foreach (var SingleNewOperation in _newOperations.Where(w => w.Parent != null))
                                    {
                                        // Sub-Sub Parent hasn't been added yet
                                        if (context.Set<SystemOperation>().Where(w => w.SystemName == SingleNewOperation.Parent.SystemName).SingleOrDefault() == null)
                                        {
                                            continue;
                                        }
                                        SingleNewOperation.ID_Parent = context.Set<SystemOperation>().Where(w => w.SystemName == SingleNewOperation.Parent.SystemName).SingleOrDefault().ID;
                                        context.Set<SystemOperation>().Add(new SystemOperation() { IsActive = true, SystemName = SingleNewOperation.SystemName, ID_Parent = SingleNewOperation.ID_Parent });
                                    }
                                    await context.SaveChangesAsync();
                                }

                                foreach (var SingleRemovedOperations in _removedOperations)
                                {
                                    // Get Dependent Operations
                                    var _dependentOperations = context.Set<SystemOperation>().Where(w => w.ID_Parent == SingleRemovedOperations.ID).ToList();
                                    foreach (var SingleDependentOperation in _dependentOperations)
                                    {
                                        // Dependent has also been deleted - Ignore updating it
                                        if (FullList.Where(w => w.SystemName == SingleDependentOperation.SystemName).SingleOrDefault() == null)
                                        {
                                            continue;
                                        }
                                        var newParent_SystemName = FullList.Where(w => w.SystemName == SingleDependentOperation.SystemName).SingleOrDefault().Parent.SystemName;
                                        SingleDependentOperation.ID_Parent = context.Set<SystemOperation>().Where(w => w.SystemName == newParent_SystemName).SingleOrDefault().ID;
                                        context.Entry(SingleDependentOperation).State = EntityState.Modified;
                                    }
                                    context.Entry(SingleRemovedOperations).State = EntityState.Deleted;
                                }
                                await context.SaveChangesAsync();

                                var iamRole = context.Set<AdminRole>().Where(w => w.IsIAMRelated).SingleOrDefault();
                                context.Set<AdminRoleOperation>().AddRange(
                                    context.Set<SystemOperation>().Where(w => w.AssignedAdminRoles.Count(c => c.ID_Role == iamRole.ID) == 0).ToList().Select(s =>
                                    {
                                        return new AdminRoleOperation() { IsActive = true, ID_Operation = s.ID, ID_Role = iamRole.ID };
                                    }).ToList()
                                    );
                                await context.SaveChangesAsync();
                            }
                        }
                    }
                }
                catch (Exception LogError)
                {
                }
                #endregion

                #region Admin Roles
                try
                {
                    if (context != null)
                    {
                        if (!context.Set<AdminRole>().Any())
                        {
                            context.Set<AdminRole>().AddRange(
                                new AdminRole()
                                {
                                    Name = "IAM",
                                    Description = "Identity & Access Management",
                                    IsActive = true,
                                    IsIAMRelated = true,
                                    AssignedOperations = context.Set<SystemOperation>().ToList().Select(s =>
                                    {
                                        return new AdminRoleOperation() { IsActive = true, ID_Operation = s.ID };
                                    }).ToList()
                                },
                                new AdminRole()
                                {
                                    Name = "Super Administrator",
                                    Description = "Super Administrator of the System (ideal should have access to everything in the system).",
                                    IsActive = true,
                                }
                                );
                            await context.SaveChangesAsync();
                        }
                    }
                }
                catch (Exception LogError)
                {
                }
                #endregion

                #region Admin Users
                try
                {
                    if (context != null)
                    {
                        if (!context.Set<AdminUser>().Any())
                        {
                            var adminUser_IAM = new AdminUser()
                            {
                                FirstName = "IAM",
                                LastName = "Built-in",
                                Email = "iam",
                                Username = "iam",
                                Password = "i",
                                IsActive = true,
                                AssignedRoles = new List<AdminUserRole>() {
                                    new AdminUserRole()
                                    {
                                        IsActive = true,
                                        ID_Role = context.Set<AdminRole>().Where(w=> w.Name == "IAM").SingleOrDefault().ID
                                    }
                                }
                            };

                            var adminUser_SuperAdmin = new AdminUser()
                            {
                                FirstName = "Super Admin",
                                LastName = "Built-in",
                                Email = "o",
                                Username = "o",
                                Password = "o",
                                IsActive = true,
                                AssignedRoles = new List<AdminUserRole>() {
                                    new AdminUserRole()
                                    {
                                        IsActive = true,
                                        ID_Role = context.Set<AdminRole>().Where(w=> w.Name == "Super Administrator").SingleOrDefault().ID
                                    }
                                }
                            };
                            context.Set<AdminUser>().AddRange(adminUser_IAM, adminUser_SuperAdmin);
                            await context.SaveChangesAsync();

                            adminUser_IAM.Password = Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes($"{adminUser_IAM.Password}{adminUser_IAM.ID}")));
                            adminUser_SuperAdmin.Password = Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes($"{adminUser_SuperAdmin.Password}{adminUser_SuperAdmin.ID}")));
                            context.Entry(adminUser_IAM).State = EntityState.Modified;
                            context.Entry(adminUser_SuperAdmin).State = EntityState.Modified;
                            await context.SaveChangesAsync();
                        }
                    }
                }
                catch (Exception LogError)
                {
                }
                #endregion

                #region Admin Role Operations
                try
                {
                    if (context != null)
                    {
                        if (!context.Set<AdminRoleOperation>().Any())
                        {
                            var iamRole = context.Set<AdminRole>().Where(w => w.IsIAMRelated).SingleOrDefault();
                            if (iamRole != null)
                            {
                                context.Set<AdminRoleOperation>().AddRange(
                                    context.Set<SystemOperation>().ToList().Select(s =>
                                    {
                                        return new AdminRoleOperation() { IsActive = true, ID_Operation = s.ID, ID_Role = iamRole.ID };
                                    }).ToList()
                                    );
                                await context.SaveChangesAsync();
                            }
                        }
                    }
                }
                catch (Exception LogError)
                {
                }
                #endregion

                #region Country
                try
                {
                    if (context != null)
                    {
                        if (!context.Set<Country>().Any())
                        {
                            context.Set<Country>().AddRange(
                                new Country() { Name = "Afghanistan", AlphaCode_2 = "AF", AlphaCode_3 = "AFG", NumericCode = 4, Demonym = "Afghan", Region = "Asia", SubRegion = "Southern Asia", CallingCode = "93", IsActive = true },
                                new Country() { Name = "Åland Islands", AlphaCode_2 = "AX", AlphaCode_3 = "ALA", NumericCode = 248, Demonym = "Ålandish", Region = "Europe", SubRegion = "Northern Europe", CallingCode = "358", IsActive = true },
                                new Country() { Name = "Albania", AlphaCode_2 = "AL", AlphaCode_3 = "ALB", NumericCode = 8, Demonym = "Albanian", Region = "Europe", SubRegion = "Southern Europe", CallingCode = "355", IsActive = true },
                                new Country() { Name = "Algeria", AlphaCode_2 = "DZ", AlphaCode_3 = "DZA", NumericCode = 12, Demonym = "Algerian", Region = "Africa", SubRegion = "Northern Africa", CallingCode = "213", IsActive = true },
                                new Country() { Name = "American Samoa", AlphaCode_2 = "AS", AlphaCode_3 = "ASM", NumericCode = 16, Demonym = "American Samoan", Region = "Oceania", SubRegion = "Polynesia", CallingCode = "1684", IsActive = true },
                                new Country() { Name = "Andorra", AlphaCode_2 = "AD", AlphaCode_3 = "AND", NumericCode = 20, Demonym = "Andorran", Region = "Europe", SubRegion = "Southern Europe", CallingCode = "376", IsActive = true },
                                new Country() { Name = "Angola", AlphaCode_2 = "AO", AlphaCode_3 = "AGO", NumericCode = 24, Demonym = "Angolan", Region = "Africa", SubRegion = "Middle Africa", CallingCode = "244", IsActive = true },
                                new Country() { Name = "Anguilla", AlphaCode_2 = "AI", AlphaCode_3 = "AIA", NumericCode = 660, Demonym = "Anguillian", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1264", IsActive = true },
                                new Country() { Name = "Antigua and Barbuda", AlphaCode_2 = "AG", AlphaCode_3 = "ATG", NumericCode = 28, Demonym = "Antiguan, Barbudan", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1268", IsActive = true },
                                new Country() { Name = "Argentina", AlphaCode_2 = "AR", AlphaCode_3 = "ARG", NumericCode = 32, Demonym = "Argentinean", Region = "Americas", SubRegion = "South America", CallingCode = "54", IsActive = true },
                                new Country() { Name = "Armenia", AlphaCode_2 = "AM", AlphaCode_3 = "ARM", NumericCode = 51, Demonym = "Armenian", Region = "Asia", SubRegion = "Western Asia", CallingCode = "374", IsActive = true },
                                new Country() { Name = "Aruba", AlphaCode_2 = "AW", AlphaCode_3 = "ABW", NumericCode = 533, Demonym = "Aruban", Region = "Americas", SubRegion = "Caribbean", CallingCode = "297", IsActive = true },
                                new Country() { Name = "Australia", AlphaCode_2 = "AU", AlphaCode_3 = "AUS", NumericCode = 36, Demonym = "Australian", Region = "Oceania", SubRegion = "Australia and New Zealand", CallingCode = "61", IsActive = true },
                                new Country() { Name = "Austria", AlphaCode_2 = "AT", AlphaCode_3 = "AUT", NumericCode = 40, Demonym = "Austrian", Region = "Europe", SubRegion = "Western Europe", CallingCode = "43", IsActive = true },
                                new Country() { Name = "Azerbaijan", AlphaCode_2 = "AZ", AlphaCode_3 = "AZE", NumericCode = 31, Demonym = "Azerbaijani", Region = "Asia", SubRegion = "Western Asia", CallingCode = "994", IsActive = true },
                                new Country() { Name = "Bahamas", AlphaCode_2 = "BS", AlphaCode_3 = "BHS", NumericCode = 44, Demonym = "Bahamian", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1242", IsActive = true },
                                new Country() { Name = "Bahrain", AlphaCode_2 = "BH", AlphaCode_3 = "BHR", NumericCode = 48, Demonym = "Bahraini", Region = "Asia", SubRegion = "Western Asia", CallingCode = "973", IsActive = true },
                                new Country() { Name = "Bangladesh", AlphaCode_2 = "BD", AlphaCode_3 = "BGD", NumericCode = 50, Demonym = "Bangladeshi", Region = "Asia", SubRegion = "Southern Asia", CallingCode = "880", IsActive = true },
                                new Country() { Name = "Barbados", AlphaCode_2 = "BB", AlphaCode_3 = "BRB", NumericCode = 52, Demonym = "Barbadian", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1246", IsActive = true },
                                new Country() { Name = "Belarus", AlphaCode_2 = "BY", AlphaCode_3 = "BLR", NumericCode = 112, Demonym = "Belarusian", Region = "Europe", SubRegion = "Eastern Europe", CallingCode = "375", IsActive = true },
                                new Country() { Name = "Belgium", AlphaCode_2 = "BE", AlphaCode_3 = "BEL", NumericCode = 56, Demonym = "Belgian", Region = "Europe", SubRegion = "Western Europe", CallingCode = "32", IsActive = true },
                                new Country() { Name = "Belize", AlphaCode_2 = "BZ", AlphaCode_3 = "BLZ", NumericCode = 84, Demonym = "Belizean", Region = "Americas", SubRegion = "Central America", CallingCode = "501", IsActive = true },
                                new Country() { Name = "Benin", AlphaCode_2 = "BJ", AlphaCode_3 = "BEN", NumericCode = 204, Demonym = "Beninese", Region = "Africa", SubRegion = "Western Africa", CallingCode = "229", IsActive = true },
                                new Country() { Name = "Bermuda", AlphaCode_2 = "BM", AlphaCode_3 = "BMU", NumericCode = 60, Demonym = "Bermudian", Region = "Americas", SubRegion = "Northern America", CallingCode = "1441", IsActive = true },
                                new Country() { Name = "Bhutan", AlphaCode_2 = "BT", AlphaCode_3 = "BTN", NumericCode = 64, Demonym = "Bhutanese", Region = "Asia", SubRegion = "Southern Asia", CallingCode = "975", IsActive = true },
                                new Country() { Name = "Bolivia (Plurinational State of)", AlphaCode_2 = "BO", AlphaCode_3 = "BOL", NumericCode = 68, Demonym = "Bolivian", Region = "Americas", SubRegion = "South America", CallingCode = "591", IsActive = true },
                                new Country() { Name = "Bonaire, Sint Eustatius and Saba", AlphaCode_2 = "BQ", AlphaCode_3 = "BES", NumericCode = 535, Demonym = "Dutch", Region = "Americas", SubRegion = "Caribbean", CallingCode = "5997", IsActive = true },
                                new Country() { Name = "Bosnia and Herzegovina", AlphaCode_2 = "BA", AlphaCode_3 = "BIH", NumericCode = 70, Demonym = "Bosnian, Herzegovinian", Region = "Europe", SubRegion = "Southern Europe", CallingCode = "387", IsActive = true },
                                new Country() { Name = "Botswana", AlphaCode_2 = "BW", AlphaCode_3 = "BWA", NumericCode = 72, Demonym = "Motswana", Region = "Africa", SubRegion = "Southern Africa", CallingCode = "267", IsActive = true },
                                new Country() { Name = "Brazil", AlphaCode_2 = "BR", AlphaCode_3 = "BRA", NumericCode = 76, Demonym = "Brazilian", Region = "Americas", SubRegion = "South America", CallingCode = "55", IsActive = true },
                                new Country() { Name = "British Indian Ocean Territory", AlphaCode_2 = "IO", AlphaCode_3 = "IOT", NumericCode = 86, Demonym = "Indian", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "246", IsActive = true },
                                new Country() { Name = "Virgin Islands (British)", AlphaCode_2 = "VG", AlphaCode_3 = "VGB", NumericCode = 92, Demonym = "Virgin Islander", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1284", IsActive = true },
                                new Country() { Name = "Virgin Islands (U.S.)", AlphaCode_2 = "VI", AlphaCode_3 = "VIR", NumericCode = 850, Demonym = "Virgin Islander", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1 340", IsActive = true },
                                new Country() { Name = "Brunei Darussalam", AlphaCode_2 = "BN", AlphaCode_3 = "BRN", NumericCode = 96, Demonym = "Bruneian", Region = "Asia", SubRegion = "South-Eastern Asia", CallingCode = "673", IsActive = true },
                                new Country() { Name = "Bulgaria", AlphaCode_2 = "BG", AlphaCode_3 = "BGR", NumericCode = 100, Demonym = "Bulgarian", Region = "Europe", SubRegion = "Eastern Europe", CallingCode = "359", IsActive = true },
                                new Country() { Name = "Burkina Faso", AlphaCode_2 = "BF", AlphaCode_3 = "BFA", NumericCode = 854, Demonym = "Burkinabe", Region = "Africa", SubRegion = "Western Africa", CallingCode = "226", IsActive = true },
                                new Country() { Name = "Burundi", AlphaCode_2 = "BI", AlphaCode_3 = "BDI", NumericCode = 108, Demonym = "Burundian", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "257", IsActive = true },
                                new Country() { Name = "Cambodia", AlphaCode_2 = "KH", AlphaCode_3 = "KHM", NumericCode = 116, Demonym = "Cambodian", Region = "Asia", SubRegion = "South-Eastern Asia", CallingCode = "855", IsActive = true },
                                new Country() { Name = "Cameroon", AlphaCode_2 = "CM", AlphaCode_3 = "CMR", NumericCode = 120, Demonym = "Cameroonian", Region = "Africa", SubRegion = "Middle Africa", CallingCode = "237", IsActive = true },
                                new Country() { Name = "Canada", AlphaCode_2 = "CA", AlphaCode_3 = "CAN", NumericCode = 124, Demonym = "Canadian", Region = "Americas", SubRegion = "Northern America", CallingCode = "1", IsActive = true },
                                new Country() { Name = "Cabo Verde", AlphaCode_2 = "CV", AlphaCode_3 = "CPV", NumericCode = 132, Demonym = "Cape Verdian", Region = "Africa", SubRegion = "Western Africa", CallingCode = "238", IsActive = true },
                                new Country() { Name = "Cayman Islands", AlphaCode_2 = "KY", AlphaCode_3 = "CYM", NumericCode = 136, Demonym = "Caymanian", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1345", IsActive = true },
                                new Country() { Name = "Central African Republic", AlphaCode_2 = "CF", AlphaCode_3 = "CAF", NumericCode = 140, Demonym = "Central African", Region = "Africa", SubRegion = "Middle Africa", CallingCode = "236", IsActive = true },
                                new Country() { Name = "Chad", AlphaCode_2 = "TD", AlphaCode_3 = "TCD", NumericCode = 148, Demonym = "Chadian", Region = "Africa", SubRegion = "Middle Africa", CallingCode = "235", IsActive = true },
                                new Country() { Name = "Chile", AlphaCode_2 = "CL", AlphaCode_3 = "CHL", NumericCode = 152, Demonym = "Chilean", Region = "Americas", SubRegion = "South America", CallingCode = "56", IsActive = true },
                                new Country() { Name = "China", AlphaCode_2 = "CN", AlphaCode_3 = "CHN", NumericCode = 156, Demonym = "Chinese", Region = "Asia", SubRegion = "Eastern Asia", CallingCode = "86", IsActive = true },
                                new Country() { Name = "Christmas Island", AlphaCode_2 = "CX", AlphaCode_3 = "CXR", NumericCode = 162, Demonym = "Christmas Island", Region = "Oceania", SubRegion = "Australia and New Zealand", CallingCode = "61", IsActive = true },
                                new Country() { Name = "Cocos (Keeling) Islands", AlphaCode_2 = "CC", AlphaCode_3 = "CCK", NumericCode = 166, Demonym = "Cocos Islander", Region = "Oceania", SubRegion = "Australia and New Zealand", CallingCode = "61", IsActive = true },
                                new Country() { Name = "Colombia", AlphaCode_2 = "CO", AlphaCode_3 = "COL", NumericCode = 170, Demonym = "Colombian", Region = "Americas", SubRegion = "South America", CallingCode = "57", IsActive = true },
                                new Country() { Name = "Comoros", AlphaCode_2 = "KM", AlphaCode_3 = "COM", NumericCode = 174, Demonym = "Comoran", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "269", IsActive = true },
                                new Country() { Name = "Congo", AlphaCode_2 = "CG", AlphaCode_3 = "COG", NumericCode = 178, Demonym = "Congolese", Region = "Africa", SubRegion = "Middle Africa", CallingCode = "242", IsActive = true },
                                new Country() { Name = "Congo (Democratic Republic of the)", AlphaCode_2 = "CD", AlphaCode_3 = "COD", NumericCode = 180, Demonym = "Congolese", Region = "Africa", SubRegion = "Middle Africa", CallingCode = "243", IsActive = true },
                                new Country() { Name = "Cook Islands", AlphaCode_2 = "CK", AlphaCode_3 = "COK", NumericCode = 184, Demonym = "Cook Islander", Region = "Oceania", SubRegion = "Polynesia", CallingCode = "682", IsActive = true },
                                new Country() { Name = "Costa Rica", AlphaCode_2 = "CR", AlphaCode_3 = "CRI", NumericCode = 188, Demonym = "Costa Rican", Region = "Americas", SubRegion = "Central America", CallingCode = "506", IsActive = true },
                                new Country() { Name = "Croatia", AlphaCode_2 = "HR", AlphaCode_3 = "HRV", NumericCode = 191, Demonym = "Croatian", Region = "Europe", SubRegion = "Southern Europe", CallingCode = "385", IsActive = true },
                                new Country() { Name = "Cuba", AlphaCode_2 = "CU", AlphaCode_3 = "CUB", NumericCode = 192, Demonym = "Cuban", Region = "Americas", SubRegion = "Caribbean", CallingCode = "53", IsActive = true },
                                new Country() { Name = "Curaçao", AlphaCode_2 = "CW", AlphaCode_3 = "CUW", NumericCode = 531, Demonym = "Dutch", Region = "Americas", SubRegion = "Caribbean", CallingCode = "599", IsActive = true },
                                new Country() { Name = "Cyprus", AlphaCode_2 = "CY", AlphaCode_3 = "CYP", NumericCode = 196, Demonym = "Cypriot", Region = "Europe", SubRegion = "Southern Europe", CallingCode = "357", IsActive = true },
                                new Country() { Name = "Czech Republic", AlphaCode_2 = "CZ", AlphaCode_3 = "CZE", NumericCode = 203, Demonym = "Czech", Region = "Europe", SubRegion = "Eastern Europe", CallingCode = "420", IsActive = true },
                                new Country() { Name = "Denmark", AlphaCode_2 = "DK", AlphaCode_3 = "DNK", NumericCode = 208, Demonym = "Danish", Region = "Europe", SubRegion = "Northern Europe", CallingCode = "45", IsActive = true },
                                new Country() { Name = "Djibouti", AlphaCode_2 = "DJ", AlphaCode_3 = "DJI", NumericCode = 262, Demonym = "Djibouti", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "253", IsActive = true },
                                new Country() { Name = "Dominica", AlphaCode_2 = "DM", AlphaCode_3 = "DMA", NumericCode = 212, Demonym = "Dominican", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1767", IsActive = true },
                                new Country() { Name = "Dominican Republic", AlphaCode_2 = "DO", AlphaCode_3 = "DOM", NumericCode = 214, Demonym = "Dominican", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1809", IsActive = true },
                                new Country() { Name = "Ecuador", AlphaCode_2 = "EC", AlphaCode_3 = "ECU", NumericCode = 218, Demonym = "Ecuadorean", Region = "Americas", SubRegion = "South America", CallingCode = "593", IsActive = true },
                                new Country() { Name = "Egypt", AlphaCode_2 = "EG", AlphaCode_3 = "EGY", NumericCode = 818, Demonym = "Egyptian", Region = "Africa", SubRegion = "Northern Africa", CallingCode = "20", IsActive = true },
                                new Country() { Name = "El Salvador", AlphaCode_2 = "SV", AlphaCode_3 = "SLV", NumericCode = 222, Demonym = "Salvadoran", Region = "Americas", SubRegion = "Central America", CallingCode = "503", IsActive = true },
                                new Country() { Name = "Equatorial Guinea", AlphaCode_2 = "GQ", AlphaCode_3 = "GNQ", NumericCode = 226, Demonym = "Equatorial Guinean", Region = "Africa", SubRegion = "Middle Africa", CallingCode = "240", IsActive = true },
                                new Country() { Name = "Eritrea", AlphaCode_2 = "ER", AlphaCode_3 = "ERI", NumericCode = 232, Demonym = "Eritrean", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "291", IsActive = true },
                                new Country() { Name = "Estonia", AlphaCode_2 = "EE", AlphaCode_3 = "EST", NumericCode = 233, Demonym = "Estonian", Region = "Europe", SubRegion = "Northern Europe", CallingCode = "372", IsActive = true },
                                new Country() { Name = "Ethiopia", AlphaCode_2 = "ET", AlphaCode_3 = "ETH", NumericCode = 231, Demonym = "Ethiopian", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "251", IsActive = true },
                                new Country() { Name = "Falkland Islands (Malvinas)", AlphaCode_2 = "FK", AlphaCode_3 = "FLK", NumericCode = 238, Demonym = "Falkland Islander", Region = "Americas", SubRegion = "South America", CallingCode = "500", IsActive = true },
                                new Country() { Name = "Faroe Islands", AlphaCode_2 = "FO", AlphaCode_3 = "FRO", NumericCode = 234, Demonym = "Faroese", Region = "Europe", SubRegion = "Northern Europe", CallingCode = "298", IsActive = true },
                                new Country() { Name = "Fiji", AlphaCode_2 = "FJ", AlphaCode_3 = "FJI", NumericCode = 242, Demonym = "Fijian", Region = "Oceania", SubRegion = "Melanesia", CallingCode = "679", IsActive = true },
                                new Country() { Name = "Finland", AlphaCode_2 = "FI", AlphaCode_3 = "FIN", NumericCode = 246, Demonym = "Finnish", Region = "Europe", SubRegion = "Northern Europe", CallingCode = "358", IsActive = true },
                                new Country() { Name = "France", AlphaCode_2 = "FR", AlphaCode_3 = "FRA", NumericCode = 250, Demonym = "French", Region = "Europe", SubRegion = "Western Europe", CallingCode = "33", IsActive = true },
                                new Country() { Name = "French Polynesia", AlphaCode_2 = "PF", AlphaCode_3 = "PYF", NumericCode = 258, Demonym = "French Polynesian", Region = "Oceania", SubRegion = "Polynesia", CallingCode = "689", IsActive = true },
                                new Country() { Name = "Gabon", AlphaCode_2 = "GA", AlphaCode_3 = "GAB", NumericCode = 266, Demonym = "Gabonese", Region = "Africa", SubRegion = "Middle Africa", CallingCode = "241", IsActive = true },
                                new Country() { Name = "Gambia", AlphaCode_2 = "GM", AlphaCode_3 = "GMB", NumericCode = 270, Demonym = "Gambian", Region = "Africa", SubRegion = "Western Africa", CallingCode = "220", IsActive = true },
                                new Country() { Name = "Georgia", AlphaCode_2 = "GE", AlphaCode_3 = "GEO", NumericCode = 268, Demonym = "Georgian", Region = "Asia", SubRegion = "Western Asia", CallingCode = "995", IsActive = true },
                                new Country() { Name = "Germany", AlphaCode_2 = "DE", AlphaCode_3 = "DEU", NumericCode = 276, Demonym = "German", Region = "Europe", SubRegion = "Western Europe", CallingCode = "49", IsActive = true },
                                new Country() { Name = "Ghana", AlphaCode_2 = "GH", AlphaCode_3 = "GHA", NumericCode = 288, Demonym = "Ghanaian", Region = "Africa", SubRegion = "Western Africa", CallingCode = "233", IsActive = true },
                                new Country() { Name = "Gibraltar", AlphaCode_2 = "GI", AlphaCode_3 = "GIB", NumericCode = 292, Demonym = "Gibraltar", Region = "Europe", SubRegion = "Southern Europe", CallingCode = "350", IsActive = true },
                                new Country() { Name = "Greece", AlphaCode_2 = "GR", AlphaCode_3 = "GRC", NumericCode = 300, Demonym = "Greek", Region = "Europe", SubRegion = "Southern Europe", CallingCode = "30", IsActive = true },
                                new Country() { Name = "Greenland", AlphaCode_2 = "GL", AlphaCode_3 = "GRL", NumericCode = 304, Demonym = "Greenlandic", Region = "Americas", SubRegion = "Northern America", CallingCode = "299", IsActive = true },
                                new Country() { Name = "Grenada", AlphaCode_2 = "GD", AlphaCode_3 = "GRD", NumericCode = 308, Demonym = "Grenadian", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1473", IsActive = true },
                                new Country() { Name = "Guadeloupe", AlphaCode_2 = "GP", AlphaCode_3 = "GLP", NumericCode = 312, Demonym = "Guadeloupian", Region = "Americas", SubRegion = "Caribbean", CallingCode = "590", IsActive = true },
                                new Country() { Name = "Guam", AlphaCode_2 = "GU", AlphaCode_3 = "GUM", NumericCode = 316, Demonym = "Guamanian", Region = "Oceania", SubRegion = "Micronesia", CallingCode = "1671", IsActive = true },
                                new Country() { Name = "Guatemala", AlphaCode_2 = "GT", AlphaCode_3 = "GTM", NumericCode = 320, Demonym = "Guatemalan", Region = "Americas", SubRegion = "Central America", CallingCode = "502", IsActive = true },
                                new Country() { Name = "Guernsey", AlphaCode_2 = "GG", AlphaCode_3 = "GGY", NumericCode = 831, Demonym = "Channel Islander", Region = "Europe", SubRegion = "Northern Europe", CallingCode = "44", IsActive = true },
                                new Country() { Name = "Guinea", AlphaCode_2 = "GN", AlphaCode_3 = "GIN", NumericCode = 324, Demonym = "Guinean", Region = "Africa", SubRegion = "Western Africa", CallingCode = "224", IsActive = true },
                                new Country() { Name = "Guinea-Bissau", AlphaCode_2 = "GW", AlphaCode_3 = "GNB", NumericCode = 624, Demonym = "Guinea-Bissauan", Region = "Africa", SubRegion = "Western Africa", CallingCode = "245", IsActive = true },
                                new Country() { Name = "Guyana", AlphaCode_2 = "GY", AlphaCode_3 = "GUY", NumericCode = 328, Demonym = "Guyanese", Region = "Americas", SubRegion = "South America", CallingCode = "592", IsActive = true },
                                new Country() { Name = "Haiti", AlphaCode_2 = "HT", AlphaCode_3 = "HTI", NumericCode = 332, Demonym = "Haitian", Region = "Americas", SubRegion = "Caribbean", CallingCode = "509", IsActive = true },
                                new Country() { Name = "Honduras", AlphaCode_2 = "HN", AlphaCode_3 = "HND", NumericCode = 340, Demonym = "Honduran", Region = "Americas", SubRegion = "Central America", CallingCode = "504", IsActive = true },
                                new Country() { Name = "Hong Kong", AlphaCode_2 = "HK", AlphaCode_3 = "HKG", NumericCode = 344, Demonym = "Chinese", Region = "Asia", SubRegion = "Eastern Asia", CallingCode = "852", IsActive = true },
                                new Country() { Name = "Hungary", AlphaCode_2 = "HU", AlphaCode_3 = "HUN", NumericCode = 348, Demonym = "Hungarian", Region = "Europe", SubRegion = "Eastern Europe", CallingCode = "36", IsActive = true },
                                new Country() { Name = "Iceland", AlphaCode_2 = "IS", AlphaCode_3 = "ISL", NumericCode = 352, Demonym = "Icelander", Region = "Europe", SubRegion = "Northern Europe", CallingCode = "354", IsActive = true },
                                new Country() { Name = "India", AlphaCode_2 = "IN", AlphaCode_3 = "IND", NumericCode = 356, Demonym = "Indian", Region = "Asia", SubRegion = "Southern Asia", CallingCode = "91", IsActive = true },
                                new Country() { Name = "Indonesia", AlphaCode_2 = "ID", AlphaCode_3 = "IDN", NumericCode = 360, Demonym = "Indonesian", Region = "Asia", SubRegion = "South-Eastern Asia", CallingCode = "62", IsActive = true },
                                new Country() { Name = "Côte d'Ivoire", AlphaCode_2 = "CI", AlphaCode_3 = "CIV", NumericCode = 384, Demonym = "Ivorian", Region = "Africa", SubRegion = "Western Africa", CallingCode = "225", IsActive = true },
                                new Country() { Name = "Iran (Islamic Republic of)", AlphaCode_2 = "IR", AlphaCode_3 = "IRN", NumericCode = 364, Demonym = "Iranian", Region = "Asia", SubRegion = "Southern Asia", CallingCode = "98", IsActive = true },
                                new Country() { Name = "Iraq", AlphaCode_2 = "IQ", AlphaCode_3 = "IRQ", NumericCode = 368, Demonym = "Iraqi", Region = "Asia", SubRegion = "Western Asia", CallingCode = "964", IsActive = true },
                                new Country() { Name = "Ireland", AlphaCode_2 = "IE", AlphaCode_3 = "IRL", NumericCode = 372, Demonym = "Irish", Region = "Europe", SubRegion = "Northern Europe", CallingCode = "353", IsActive = true },
                                new Country() { Name = "Isle of Man", AlphaCode_2 = "IM", AlphaCode_3 = "IMN", NumericCode = 833, Demonym = "Manx", Region = "Europe", SubRegion = "Northern Europe", CallingCode = "44", IsActive = true },
                                new Country() { Name = "Israel", AlphaCode_2 = "IL", AlphaCode_3 = "ISR", NumericCode = 376, Demonym = "Israeli", Region = "Asia", SubRegion = "Western Asia", CallingCode = "972", IsActive = true },
                                new Country() { Name = "Italy", AlphaCode_2 = "IT", AlphaCode_3 = "ITA", NumericCode = 380, Demonym = "Italian", Region = "Europe", SubRegion = "Southern Europe", CallingCode = "39", IsActive = true },
                                new Country() { Name = "Jamaica", AlphaCode_2 = "JM", AlphaCode_3 = "JAM", NumericCode = 388, Demonym = "Jamaican", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1876", IsActive = true },
                                new Country() { Name = "Japan", AlphaCode_2 = "JP", AlphaCode_3 = "JPN", NumericCode = 392, Demonym = "Japanese", Region = "Asia", SubRegion = "Eastern Asia", CallingCode = "81", IsActive = true },
                                new Country() { Name = "Jersey", AlphaCode_2 = "JE", AlphaCode_3 = "JEY", NumericCode = 832, Demonym = "Channel Islander", Region = "Europe", SubRegion = "Northern Europe", CallingCode = "44", IsActive = true },
                                new Country() { Name = "Jordan", AlphaCode_2 = "JO", AlphaCode_3 = "JOR", NumericCode = 400, Demonym = "Jordanian", Region = "Asia", SubRegion = "Western Asia", CallingCode = "962", IsActive = true },
                                new Country() { Name = "Kazakhstan", AlphaCode_2 = "KZ", AlphaCode_3 = "KAZ", NumericCode = 398, Demonym = "Kazakhstani", Region = "Asia", SubRegion = "Central Asia", CallingCode = "76", IsActive = true },
                                new Country() { Name = "Kenya", AlphaCode_2 = "KE", AlphaCode_3 = "KEN", NumericCode = 404, Demonym = "Kenyan", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "254", IsActive = true },
                                new Country() { Name = "Kiribati", AlphaCode_2 = "KI", AlphaCode_3 = "KIR", NumericCode = 296, Demonym = "I-Kiribati", Region = "Oceania", SubRegion = "Micronesia", CallingCode = "686", IsActive = true },
                                new Country() { Name = "Kuwait", AlphaCode_2 = "KW", AlphaCode_3 = "KWT", NumericCode = 414, Demonym = "Kuwaiti", Region = "Asia", SubRegion = "Western Asia", CallingCode = "965", IsActive = true },
                                new Country() { Name = "Kyrgyzstan", AlphaCode_2 = "KG", AlphaCode_3 = "KGZ", NumericCode = 417, Demonym = "Kirghiz", Region = "Asia", SubRegion = "Central Asia", CallingCode = "996", IsActive = true },
                                new Country() { Name = "Lao People's Democratic Republic", AlphaCode_2 = "LA", AlphaCode_3 = "LAO", NumericCode = 418, Demonym = "Laotian", Region = "Asia", SubRegion = "South-Eastern Asia", CallingCode = "856", IsActive = true },
                                new Country() { Name = "Latvia", AlphaCode_2 = "LV", AlphaCode_3 = "LVA", NumericCode = 428, Demonym = "Latvian", Region = "Europe", SubRegion = "Northern Europe", CallingCode = "371", IsActive = true },
                                new Country() { Name = "Lebanon", AlphaCode_2 = "LB", AlphaCode_3 = "LBN", NumericCode = 422, Demonym = "Lebanese", Region = "Asia", SubRegion = "Western Asia", CallingCode = "961", IsActive = true },
                                new Country() { Name = "Lesotho", AlphaCode_2 = "LS", AlphaCode_3 = "LSO", NumericCode = 426, Demonym = "Mosotho", Region = "Africa", SubRegion = "Southern Africa", CallingCode = "266", IsActive = true },
                                new Country() { Name = "Liberia", AlphaCode_2 = "LR", AlphaCode_3 = "LBR", NumericCode = 430, Demonym = "Liberian", Region = "Africa", SubRegion = "Western Africa", CallingCode = "231", IsActive = true },
                                new Country() { Name = "Libya", AlphaCode_2 = "LY", AlphaCode_3 = "LBY", NumericCode = 434, Demonym = "Libyan", Region = "Africa", SubRegion = "Northern Africa", CallingCode = "218", IsActive = true },
                                new Country() { Name = "Liechtenstein", AlphaCode_2 = "LI", AlphaCode_3 = "LIE", NumericCode = 438, Demonym = "Liechtensteiner", Region = "Europe", SubRegion = "Western Europe", CallingCode = "423", IsActive = true },
                                new Country() { Name = "Lithuania", AlphaCode_2 = "LT", AlphaCode_3 = "LTU", NumericCode = 440, Demonym = "Lithuanian", Region = "Europe", SubRegion = "Northern Europe", CallingCode = "370", IsActive = true },
                                new Country() { Name = "Luxembourg", AlphaCode_2 = "LU", AlphaCode_3 = "LUX", NumericCode = 442, Demonym = "Luxembourger", Region = "Europe", SubRegion = "Western Europe", CallingCode = "352", IsActive = true },
                                new Country() { Name = "Macao", AlphaCode_2 = "MO", AlphaCode_3 = "MAC", NumericCode = 446, Demonym = "Chinese", Region = "Asia", SubRegion = "Eastern Asia", CallingCode = "853", IsActive = true },
                                new Country() { Name = "Macedonia (the former Yugoslav Republic of)", AlphaCode_2 = "MK", AlphaCode_3 = "MKD", NumericCode = 807, Demonym = "Macedonian", Region = "Europe", SubRegion = "Southern Europe", CallingCode = "389", IsActive = true },
                                new Country() { Name = "Madagascar", AlphaCode_2 = "MG", AlphaCode_3 = "MDG", NumericCode = 450, Demonym = "Malagasy", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "261", IsActive = true },
                                new Country() { Name = "Malawi", AlphaCode_2 = "MW", AlphaCode_3 = "MWI", NumericCode = 454, Demonym = "Malawian", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "265", IsActive = true },
                                new Country() { Name = "Malaysia", AlphaCode_2 = "MY", AlphaCode_3 = "MYS", NumericCode = 458, Demonym = "Malaysian", Region = "Asia", SubRegion = "South-Eastern Asia", CallingCode = "60", IsActive = true },
                                new Country() { Name = "Maldives", AlphaCode_2 = "MV", AlphaCode_3 = "MDV", NumericCode = 462, Demonym = "Maldivan", Region = "Asia", SubRegion = "Southern Asia", CallingCode = "960", IsActive = true },
                                new Country() { Name = "Mali", AlphaCode_2 = "ML", AlphaCode_3 = "MLI", NumericCode = 466, Demonym = "Malian", Region = "Africa", SubRegion = "Western Africa", CallingCode = "223", IsActive = true },
                                new Country() { Name = "Malta", AlphaCode_2 = "MT", AlphaCode_3 = "MLT", NumericCode = 470, Demonym = "Maltese", Region = "Europe", SubRegion = "Southern Europe", CallingCode = "356", IsActive = true },
                                new Country() { Name = "Marshall Islands", AlphaCode_2 = "MH", AlphaCode_3 = "MHL", NumericCode = 584, Demonym = "Marshallese", Region = "Oceania", SubRegion = "Micronesia", CallingCode = "692", IsActive = true },
                                new Country() { Name = "Martinique", AlphaCode_2 = "MQ", AlphaCode_3 = "MTQ", NumericCode = 474, Demonym = "French", Region = "Americas", SubRegion = "Caribbean", CallingCode = "596", IsActive = true },
                                new Country() { Name = "Mauritania", AlphaCode_2 = "MR", AlphaCode_3 = "MRT", NumericCode = 478, Demonym = "Mauritanian", Region = "Africa", SubRegion = "Western Africa", CallingCode = "222", IsActive = true },
                                new Country() { Name = "Mauritius", AlphaCode_2 = "MU", AlphaCode_3 = "MUS", NumericCode = 480, Demonym = "Mauritian", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "230", IsActive = true },
                                new Country() { Name = "Mayotte", AlphaCode_2 = "YT", AlphaCode_3 = "MYT", NumericCode = 175, Demonym = "French", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "262", IsActive = true },
                                new Country() { Name = "Mexico", AlphaCode_2 = "MX", AlphaCode_3 = "MEX", NumericCode = 484, Demonym = "Mexican", Region = "Americas", SubRegion = "Central America", CallingCode = "52", IsActive = true },
                                new Country() { Name = "Micronesia (Federated States of)", AlphaCode_2 = "FM", AlphaCode_3 = "FSM", NumericCode = 583, Demonym = "Micronesian", Region = "Oceania", SubRegion = "Micronesia", CallingCode = "691", IsActive = true },
                                new Country() { Name = "Moldova (Republic of)", AlphaCode_2 = "MD", AlphaCode_3 = "MDA", NumericCode = 498, Demonym = "Moldovan", Region = "Europe", SubRegion = "Eastern Europe", CallingCode = "373", IsActive = true },
                                new Country() { Name = "Monaco", AlphaCode_2 = "MC", AlphaCode_3 = "MCO", NumericCode = 492, Demonym = "Monegasque", Region = "Europe", SubRegion = "Western Europe", CallingCode = "377", IsActive = true },
                                new Country() { Name = "Mongolia", AlphaCode_2 = "MN", AlphaCode_3 = "MNG", NumericCode = 496, Demonym = "Mongolian", Region = "Asia", SubRegion = "Eastern Asia", CallingCode = "976", IsActive = true },
                                new Country() { Name = "Montenegro", AlphaCode_2 = "ME", AlphaCode_3 = "MNE", NumericCode = 499, Demonym = "Montenegrin", Region = "Europe", SubRegion = "Southern Europe", CallingCode = "382", IsActive = true },
                                new Country() { Name = "Montserrat", AlphaCode_2 = "MS", AlphaCode_3 = "MSR", NumericCode = 500, Demonym = "Montserratian", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1664", IsActive = true },
                                new Country() { Name = "Morocco", AlphaCode_2 = "MA", AlphaCode_3 = "MAR", NumericCode = 504, Demonym = "Moroccan", Region = "Africa", SubRegion = "Northern Africa", CallingCode = "212", IsActive = true },
                                new Country() { Name = "Mozambique", AlphaCode_2 = "MZ", AlphaCode_3 = "MOZ", NumericCode = 508, Demonym = "Mozambican", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "258", IsActive = true },
                                new Country() { Name = "Myanmar", AlphaCode_2 = "MM", AlphaCode_3 = "MMR", NumericCode = 104, Demonym = "Burmese", Region = "Asia", SubRegion = "South-Eastern Asia", CallingCode = "95", IsActive = true },
                                new Country() { Name = "Namibia", AlphaCode_2 = "NA", AlphaCode_3 = "NAM", NumericCode = 516, Demonym = "Namibian", Region = "Africa", SubRegion = "Southern Africa", CallingCode = "264", IsActive = true },
                                new Country() { Name = "Nauru", AlphaCode_2 = "NR", AlphaCode_3 = "NRU", NumericCode = 520, Demonym = "Nauruan", Region = "Oceania", SubRegion = "Micronesia", CallingCode = "674", IsActive = true },
                                new Country() { Name = "Nepal", AlphaCode_2 = "NP", AlphaCode_3 = "NPL", NumericCode = 524, Demonym = "Nepalese", Region = "Asia", SubRegion = "Southern Asia", CallingCode = "977", IsActive = true },
                                new Country() { Name = "Netherlands", AlphaCode_2 = "NL", AlphaCode_3 = "NLD", NumericCode = 528, Demonym = "Dutch", Region = "Europe", SubRegion = "Western Europe", CallingCode = "31", IsActive = true },
                                new Country() { Name = "New Caledonia", AlphaCode_2 = "NC", AlphaCode_3 = "NCL", NumericCode = 540, Demonym = "New Caledonian", Region = "Oceania", SubRegion = "Melanesia", CallingCode = "687", IsActive = true },
                                new Country() { Name = "New Zealand", AlphaCode_2 = "NZ", AlphaCode_3 = "NZL", NumericCode = 554, Demonym = "New Zealander", Region = "Oceania", SubRegion = "Australia and New Zealand", CallingCode = "64", IsActive = true },
                                new Country() { Name = "Nicaragua", AlphaCode_2 = "NI", AlphaCode_3 = "NIC", NumericCode = 558, Demonym = "Nicaraguan", Region = "Americas", SubRegion = "Central America", CallingCode = "505", IsActive = true },
                                new Country() { Name = "Niger", AlphaCode_2 = "NE", AlphaCode_3 = "NER", NumericCode = 562, Demonym = "Nigerien", Region = "Africa", SubRegion = "Western Africa", CallingCode = "227", IsActive = true },
                                new Country() { Name = "Nigeria", AlphaCode_2 = "NG", AlphaCode_3 = "NGA", NumericCode = 566, Demonym = "Nigerian", Region = "Africa", SubRegion = "Western Africa", CallingCode = "234", IsActive = true },
                                new Country() { Name = "Niue", AlphaCode_2 = "NU", AlphaCode_3 = "NIU", NumericCode = 570, Demonym = "Niuean", Region = "Oceania", SubRegion = "Polynesia", CallingCode = "683", IsActive = true },
                                new Country() { Name = "Norfolk Island", AlphaCode_2 = "NF", AlphaCode_3 = "NFK", NumericCode = 574, Demonym = "Norfolk Islander", Region = "Oceania", SubRegion = "Australia and New Zealand", CallingCode = "672", IsActive = true },
                                new Country() { Name = "Korea (Democratic People's Republic of)", AlphaCode_2 = "KP", AlphaCode_3 = "PRK", NumericCode = 408, Demonym = "North Korean", Region = "Asia", SubRegion = "Eastern Asia", CallingCode = "850", IsActive = true },
                                new Country() { Name = "Northern Mariana Islands", AlphaCode_2 = "MP", AlphaCode_3 = "MNP", NumericCode = 580, Demonym = "American", Region = "Oceania", SubRegion = "Micronesia", CallingCode = "1670", IsActive = true },
                                new Country() { Name = "Norway", AlphaCode_2 = "NO", AlphaCode_3 = "NOR", NumericCode = 578, Demonym = "Norwegian", Region = "Europe", SubRegion = "Northern Europe", CallingCode = "47", IsActive = true },
                                new Country() { Name = "Oman", AlphaCode_2 = "OM", AlphaCode_3 = "OMN", NumericCode = 512, Demonym = "Omani", Region = "Asia", SubRegion = "Western Asia", CallingCode = "968", IsActive = true },
                                new Country() { Name = "Pakistan", AlphaCode_2 = "PK", AlphaCode_3 = "PAK", NumericCode = 586, Demonym = "Pakistani", Region = "Asia", SubRegion = "Southern Asia", CallingCode = "92", IsActive = true },
                                new Country() { Name = "Palau", AlphaCode_2 = "PW", AlphaCode_3 = "PLW", NumericCode = 585, Demonym = "Palauan", Region = "Oceania", SubRegion = "Micronesia", CallingCode = "680", IsActive = true },
                                new Country() { Name = "Palestine, State of", AlphaCode_2 = "PS", AlphaCode_3 = "PSE", NumericCode = 275, Demonym = "Palestinian", Region = "Asia", SubRegion = "Western Asia", CallingCode = "970", IsActive = true },
                                new Country() { Name = "Panama", AlphaCode_2 = "PA", AlphaCode_3 = "PAN", NumericCode = 591, Demonym = "Panamanian", Region = "Americas", SubRegion = "Central America", CallingCode = "507", IsActive = true },
                                new Country() { Name = "Papua New Guinea", AlphaCode_2 = "PG", AlphaCode_3 = "PNG", NumericCode = 598, Demonym = "Papua New Guinean", Region = "Oceania", SubRegion = "Melanesia", CallingCode = "675", IsActive = true },
                                new Country() { Name = "Paraguay", AlphaCode_2 = "PY", AlphaCode_3 = "PRY", NumericCode = 600, Demonym = "Paraguayan", Region = "Americas", SubRegion = "South America", CallingCode = "595", IsActive = true },
                                new Country() { Name = "Peru", AlphaCode_2 = "PE", AlphaCode_3 = "PER", NumericCode = 604, Demonym = "Peruvian", Region = "Americas", SubRegion = "South America", CallingCode = "51", IsActive = true },
                                new Country() { Name = "Philippines", AlphaCode_2 = "PH", AlphaCode_3 = "PHL", NumericCode = 608, Demonym = "Filipino", Region = "Asia", SubRegion = "South-Eastern Asia", CallingCode = "63", IsActive = true },
                                new Country() { Name = "Pitcairn", AlphaCode_2 = "PN", AlphaCode_3 = "PCN", NumericCode = 612, Demonym = "Pitcairn Islander", Region = "Oceania", SubRegion = "Polynesia", CallingCode = "64", IsActive = true },
                                new Country() { Name = "Poland", AlphaCode_2 = "PL", AlphaCode_3 = "POL", NumericCode = 616, Demonym = "Polish", Region = "Europe", SubRegion = "Eastern Europe", CallingCode = "48", IsActive = true },
                                new Country() { Name = "Portugal", AlphaCode_2 = "PT", AlphaCode_3 = "PRT", NumericCode = 620, Demonym = "Portuguese", Region = "Europe", SubRegion = "Southern Europe", CallingCode = "351", IsActive = true },
                                new Country() { Name = "Puerto Rico", AlphaCode_2 = "PR", AlphaCode_3 = "PRI", NumericCode = 630, Demonym = "Puerto Rican", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1787", IsActive = true },
                                new Country() { Name = "Qatar", AlphaCode_2 = "QA", AlphaCode_3 = "QAT", NumericCode = 634, Demonym = "Qatari", Region = "Asia", SubRegion = "Western Asia", CallingCode = "974", IsActive = true },
                                new Country() { Name = "Réunion", AlphaCode_2 = "RE", AlphaCode_3 = "REU", NumericCode = 638, Demonym = "French", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "262", IsActive = true },
                                new Country() { Name = "Romania", AlphaCode_2 = "RO", AlphaCode_3 = "ROU", NumericCode = 642, Demonym = "Romanian", Region = "Europe", SubRegion = "Eastern Europe", CallingCode = "40", IsActive = true },
                                new Country() { Name = "Russian Federation", AlphaCode_2 = "RU", AlphaCode_3 = "RUS", NumericCode = 643, Demonym = "Russian", Region = "Europe", SubRegion = "Eastern Europe", CallingCode = "7", IsActive = true },
                                new Country() { Name = "Rwanda", AlphaCode_2 = "RW", AlphaCode_3 = "RWA", NumericCode = 646, Demonym = "Rwandan", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "250", IsActive = true },
                                new Country() { Name = "Saint Barthélemy", AlphaCode_2 = "BL", AlphaCode_3 = "BLM", NumericCode = 652, Demonym = "Saint Barthélemy Islander", Region = "Americas", SubRegion = "Caribbean", CallingCode = "590", IsActive = true },
                                new Country() { Name = "Saint Helena, Ascension and Tristan da Cunha", AlphaCode_2 = "SH", AlphaCode_3 = "SHN", NumericCode = 654, Demonym = "Saint Helenian", Region = "Africa", SubRegion = "Western Africa", CallingCode = "290", IsActive = true },
                                new Country() { Name = "Saint Kitts and Nevis", AlphaCode_2 = "KN", AlphaCode_3 = "KNA", NumericCode = 659, Demonym = "Kittian and Nevisian", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1869", IsActive = true },
                                new Country() { Name = "Saint Lucia", AlphaCode_2 = "LC", AlphaCode_3 = "LCA", NumericCode = 662, Demonym = "Saint Lucian", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1758", IsActive = true },
                                new Country() { Name = "Saint Martin (French part)", AlphaCode_2 = "MF", AlphaCode_3 = "MAF", NumericCode = 663, Demonym = "Saint Martin Islander", Region = "Americas", SubRegion = "Caribbean", CallingCode = "590", IsActive = true },
                                new Country() { Name = "Saint Pierre and Miquelon", AlphaCode_2 = "PM", AlphaCode_3 = "SPM", NumericCode = 666, Demonym = "French", Region = "Americas", SubRegion = "Northern America", CallingCode = "508", IsActive = true },
                                new Country() { Name = "Saint Vincent and the Grenadines", AlphaCode_2 = "VC", AlphaCode_3 = "VCT", NumericCode = 670, Demonym = "Saint Vincentian", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1784", IsActive = true },
                                new Country() { Name = "Samoa", AlphaCode_2 = "WS", AlphaCode_3 = "WSM", NumericCode = 882, Demonym = "Samoan", Region = "Oceania", SubRegion = "Polynesia", CallingCode = "685", IsActive = true },
                                new Country() { Name = "San Marino", AlphaCode_2 = "SM", AlphaCode_3 = "SMR", NumericCode = 674, Demonym = "Sammarinese", Region = "Europe", SubRegion = "Southern Europe", CallingCode = "378", IsActive = true },
                                new Country() { Name = "Sao Tome and Principe", AlphaCode_2 = "ST", AlphaCode_3 = "STP", NumericCode = 678, Demonym = "Sao Tomean", Region = "Africa", SubRegion = "Middle Africa", CallingCode = "239", IsActive = true },
                                new Country() { Name = "Saudi Arabia", AlphaCode_2 = "SA", AlphaCode_3 = "SAU", NumericCode = 682, Demonym = "Saudi Arabian", Region = "Asia", SubRegion = "Western Asia", CallingCode = "966", IsActive = true },
                                new Country() { Name = "Senegal", AlphaCode_2 = "SN", AlphaCode_3 = "SEN", NumericCode = 686, Demonym = "Senegalese", Region = "Africa", SubRegion = "Western Africa", CallingCode = "221", IsActive = true },
                                new Country() { Name = "Serbia", AlphaCode_2 = "RS", AlphaCode_3 = "SRB", NumericCode = 688, Demonym = "Serbian", Region = "Europe", SubRegion = "Southern Europe", CallingCode = "381", IsActive = true },
                                new Country() { Name = "Seychelles", AlphaCode_2 = "SC", AlphaCode_3 = "SYC", NumericCode = 690, Demonym = "Seychellois", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "248", IsActive = true },
                                new Country() { Name = "Sierra Leone", AlphaCode_2 = "SL", AlphaCode_3 = "SLE", NumericCode = 694, Demonym = "Sierra Leonean", Region = "Africa", SubRegion = "Western Africa", CallingCode = "232", IsActive = true },
                                new Country() { Name = "Singapore", AlphaCode_2 = "SG", AlphaCode_3 = "SGP", NumericCode = 702, Demonym = "Singaporean", Region = "Asia", SubRegion = "South-Eastern Asia", CallingCode = "65", IsActive = true },
                                new Country() { Name = "Sint Maarten (Dutch part)", AlphaCode_2 = "SX", AlphaCode_3 = "SXM", NumericCode = 534, Demonym = "Dutch", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1721", IsActive = true },
                                new Country() { Name = "Slovakia", AlphaCode_2 = "SK", AlphaCode_3 = "SVK", NumericCode = 703, Demonym = "Slovak", Region = "Europe", SubRegion = "Eastern Europe", CallingCode = "421", IsActive = true },
                                new Country() { Name = "Slovenia", AlphaCode_2 = "SI", AlphaCode_3 = "SVN", NumericCode = 705, Demonym = "Slovene", Region = "Europe", SubRegion = "Southern Europe", CallingCode = "386", IsActive = true },
                                new Country() { Name = "Solomon Islands", AlphaCode_2 = "SB", AlphaCode_3 = "SLB", NumericCode = 90, Demonym = "Solomon Islander", Region = "Oceania", SubRegion = "Melanesia", CallingCode = "677", IsActive = true },
                                new Country() { Name = "Somalia", AlphaCode_2 = "SO", AlphaCode_3 = "SOM", NumericCode = 706, Demonym = "Somali", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "252", IsActive = true },
                                new Country() { Name = "South Africa", AlphaCode_2 = "ZA", AlphaCode_3 = "ZAF", NumericCode = 710, Demonym = "South African", Region = "Africa", SubRegion = "Southern Africa", CallingCode = "27", IsActive = true },
                                new Country() { Name = "South Georgia and the South Sandwich Islands", AlphaCode_2 = "GS", AlphaCode_3 = "SGS", NumericCode = 239, Demonym = "South Georgia and the South Sandwich Islander", Region = "Americas", SubRegion = "South America", CallingCode = "500", IsActive = true },
                                new Country() { Name = "Korea (Republic of)", AlphaCode_2 = "KR", AlphaCode_3 = "KOR", NumericCode = 410, Demonym = "South Korean", Region = "Asia", SubRegion = "Eastern Asia", CallingCode = "82", IsActive = true },
                                new Country() { Name = "South Sudan", AlphaCode_2 = "SS", AlphaCode_3 = "SSD", NumericCode = 728, Demonym = "South Sudanese", Region = "Africa", SubRegion = "Middle Africa", CallingCode = "211", IsActive = true },
                                new Country() { Name = "Spain", AlphaCode_2 = "ES", AlphaCode_3 = "ESP", NumericCode = 724, Demonym = "Spanish", Region = "Europe", SubRegion = "Southern Europe", CallingCode = "34", IsActive = true },
                                new Country() { Name = "Sri Lanka", AlphaCode_2 = "LK", AlphaCode_3 = "LKA", NumericCode = 144, Demonym = "Sri Lankan", Region = "Asia", SubRegion = "Southern Asia", CallingCode = "94", IsActive = true },
                                new Country() { Name = "Sudan", AlphaCode_2 = "SD", AlphaCode_3 = "SDN", NumericCode = 729, Demonym = "Sudanese", Region = "Africa", SubRegion = "Northern Africa", CallingCode = "249", IsActive = true },
                                new Country() { Name = "Suriname", AlphaCode_2 = "SR", AlphaCode_3 = "SUR", NumericCode = 740, Demonym = "Surinamer", Region = "Americas", SubRegion = "South America", CallingCode = "597", IsActive = true },
                                new Country() { Name = "Svalbard and Jan Mayen", AlphaCode_2 = "SJ", AlphaCode_3 = "SJM", NumericCode = 744, Demonym = "Norwegian", Region = "Europe", SubRegion = "Northern Europe", CallingCode = "4779", IsActive = true },
                                new Country() { Name = "Swaziland", AlphaCode_2 = "SZ", AlphaCode_3 = "SWZ", NumericCode = 748, Demonym = "Swazi", Region = "Africa", SubRegion = "Southern Africa", CallingCode = "268", IsActive = true },
                                new Country() { Name = "Sweden", AlphaCode_2 = "SE", AlphaCode_3 = "SWE", NumericCode = 752, Demonym = "Swedish", Region = "Europe", SubRegion = "Northern Europe", CallingCode = "46", IsActive = true },
                                new Country() { Name = "Switzerland", AlphaCode_2 = "CH", AlphaCode_3 = "CHE", NumericCode = 756, Demonym = "Swiss", Region = "Europe", SubRegion = "Western Europe", CallingCode = "41", IsActive = true },
                                new Country() { Name = "Syrian Arab Republic", AlphaCode_2 = "SY", AlphaCode_3 = "SYR", NumericCode = 760, Demonym = "Syrian", Region = "Asia", SubRegion = "Western Asia", CallingCode = "963", IsActive = true },
                                new Country() { Name = "Taiwan", AlphaCode_2 = "TW", AlphaCode_3 = "TWN", NumericCode = 158, Demonym = "Taiwanese", Region = "Asia", SubRegion = "Eastern Asia", CallingCode = "886", IsActive = true },
                                new Country() { Name = "Tajikistan", AlphaCode_2 = "TJ", AlphaCode_3 = "TJK", NumericCode = 762, Demonym = "Tadzhik", Region = "Asia", SubRegion = "Central Asia", CallingCode = "992", IsActive = true },
                                new Country() { Name = "Tanzania, United Republic of", AlphaCode_2 = "TZ", AlphaCode_3 = "TZA", NumericCode = 834, Demonym = "Tanzanian", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "255", IsActive = true },
                                new Country() { Name = "Thailand", AlphaCode_2 = "TH", AlphaCode_3 = "THA", NumericCode = 764, Demonym = "Thai", Region = "Asia", SubRegion = "South-Eastern Asia", CallingCode = "66", IsActive = true },
                                new Country() { Name = "Timor-Leste", AlphaCode_2 = "TL", AlphaCode_3 = "TLS", NumericCode = 626, Demonym = "East Timorese", Region = "Asia", SubRegion = "South-Eastern Asia", CallingCode = "670", IsActive = true },
                                new Country() { Name = "Togo", AlphaCode_2 = "TG", AlphaCode_3 = "TGO", NumericCode = 768, Demonym = "Togolese", Region = "Africa", SubRegion = "Western Africa", CallingCode = "228", IsActive = true },
                                new Country() { Name = "Tokelau", AlphaCode_2 = "TK", AlphaCode_3 = "TKL", NumericCode = 772, Demonym = "Tokelauan", Region = "Oceania", SubRegion = "Polynesia", CallingCode = "690", IsActive = true },
                                new Country() { Name = "Tonga", AlphaCode_2 = "TO", AlphaCode_3 = "TON", NumericCode = 776, Demonym = "Tongan", Region = "Oceania", SubRegion = "Polynesia", CallingCode = "676", IsActive = true },
                                new Country() { Name = "Trinidad and Tobago", AlphaCode_2 = "TT", AlphaCode_3 = "TTO", NumericCode = 780, Demonym = "Trinidadian", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1868", IsActive = true },
                                new Country() { Name = "Tunisia", AlphaCode_2 = "TN", AlphaCode_3 = "TUN", NumericCode = 788, Demonym = "Tunisian", Region = "Africa", SubRegion = "Northern Africa", CallingCode = "216", IsActive = true },
                                new Country() { Name = "Turkey", AlphaCode_2 = "TR", AlphaCode_3 = "TUR", NumericCode = 792, Demonym = "Turkish", Region = "Asia", SubRegion = "Western Asia", CallingCode = "90", IsActive = true },
                                new Country() { Name = "Turkmenistan", AlphaCode_2 = "TM", AlphaCode_3 = "TKM", NumericCode = 795, Demonym = "Turkmen", Region = "Asia", SubRegion = "Central Asia", CallingCode = "993", IsActive = true },
                                new Country() { Name = "Turks and Caicos Islands", AlphaCode_2 = "TC", AlphaCode_3 = "TCA", NumericCode = 796, Demonym = "Turks and Caicos Islander", Region = "Americas", SubRegion = "Caribbean", CallingCode = "1649", IsActive = true },
                                new Country() { Name = "Tuvalu", AlphaCode_2 = "TV", AlphaCode_3 = "TUV", NumericCode = 798, Demonym = "Tuvaluan", Region = "Oceania", SubRegion = "Polynesia", CallingCode = "688", IsActive = true },
                                new Country() { Name = "Uganda", AlphaCode_2 = "UG", AlphaCode_3 = "UGA", NumericCode = 800, Demonym = "Ugandan", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "256", IsActive = true },
                                new Country() { Name = "Ukraine", AlphaCode_2 = "UA", AlphaCode_3 = "UKR", NumericCode = 804, Demonym = "Ukrainian", Region = "Europe", SubRegion = "Eastern Europe", CallingCode = "380", IsActive = true },
                                new Country() { Name = "United Arab Emirates", AlphaCode_2 = "AE", AlphaCode_3 = "ARE", NumericCode = 784, Demonym = "Emirati", Region = "Asia", SubRegion = "Western Asia", CallingCode = "971", IsActive = true },
                                new Country() { Name = "United Kingdom of Great Britain and Northern Ireland", AlphaCode_2 = "GB", AlphaCode_3 = "GBR", NumericCode = 826, Demonym = "British", Region = "Europe", SubRegion = "Northern Europe", CallingCode = "44", IsActive = true },
                                new Country() { Name = "United States of America", AlphaCode_2 = "US", AlphaCode_3 = "USA", NumericCode = 840, Demonym = "American", Region = "Americas", SubRegion = "Northern America", CallingCode = "1", IsActive = true },
                                new Country() { Name = "Uruguay", AlphaCode_2 = "UY", AlphaCode_3 = "URY", NumericCode = 858, Demonym = "Uruguayan", Region = "Americas", SubRegion = "South America", CallingCode = "598", IsActive = true },
                                new Country() { Name = "Uzbekistan", AlphaCode_2 = "UZ", AlphaCode_3 = "UZB", NumericCode = 860, Demonym = "Uzbekistani", Region = "Asia", SubRegion = "Central Asia", CallingCode = "998", IsActive = true },
                                new Country() { Name = "Vanuatu", AlphaCode_2 = "VU", AlphaCode_3 = "VUT", NumericCode = 548, Demonym = "Ni-Vanuatu", Region = "Oceania", SubRegion = "Melanesia", CallingCode = "678", IsActive = true },
                                new Country() { Name = "Venezuela (Bolivarian Republic of)", AlphaCode_2 = "VE", AlphaCode_3 = "VEN", NumericCode = 862, Demonym = "Venezuelan", Region = "Americas", SubRegion = "South America", CallingCode = "58", IsActive = true },
                                new Country() { Name = "Viet Nam", AlphaCode_2 = "VN", AlphaCode_3 = "VNM", NumericCode = 704, Demonym = "Vietnamese", Region = "Asia", SubRegion = "South-Eastern Asia", CallingCode = "84", IsActive = true },
                                new Country() { Name = "Wallis and Futuna", AlphaCode_2 = "WF", AlphaCode_3 = "WLF", NumericCode = 876, Demonym = "Wallis and Futuna Islander", Region = "Oceania", SubRegion = "Polynesia", CallingCode = "681", IsActive = true },
                                new Country() { Name = "Western Sahara", AlphaCode_2 = "EH", AlphaCode_3 = "ESH", NumericCode = 732, Demonym = "Sahrawi", Region = "Africa", SubRegion = "Northern Africa", CallingCode = "212", IsActive = true },
                                new Country() { Name = "Yemen", AlphaCode_2 = "YE", AlphaCode_3 = "YEM", NumericCode = 887, Demonym = "Yemeni", Region = "Asia", SubRegion = "Western Asia", CallingCode = "967", IsActive = true },
                                new Country() { Name = "Zambia", AlphaCode_2 = "ZM", AlphaCode_3 = "ZMB", NumericCode = 894, Demonym = "Zambian", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "260", IsActive = true },
                                new Country() { Name = "Zimbabwe", AlphaCode_2 = "ZW", AlphaCode_3 = "ZWE", NumericCode = 716, Demonym = "Zimbabwean", Region = "Africa", SubRegion = "Eastern Africa", CallingCode = "263", IsActive = true }
                                );

                            await context.SaveChangesAsync();
                        }
                    }
                }
                catch (Exception LogError)
                {
                }
                #endregion
            }
            catch (Exception LogError)
            {
            }
        }

    }
}
