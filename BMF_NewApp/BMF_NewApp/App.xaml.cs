using BMF_NewApp.Data;
using BMF_NewApp.Models;
using BMF_NewApp.Services;
using BMF_NewApp.ViewModels;
using BMF_NewApp.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using static BMF_NewApp.Models.BMFAppTables;

[assembly: ExportFont("Montserrat-Black.ttf", Alias = "MontserratBlack")]
[assembly: ExportFont("Montserrat-Bold.ttf", Alias = "MontserratBold")]
[assembly: ExportFont("Montserrat-ExtraLight.ttf", Alias = "MontserratExtraLight")]
[assembly: ExportFont("Montserrat-Light.ttf", Alias = "MontserratLight")]
[assembly: ExportFont("Montserrat-Medium.ttf", Alias = "MontserratMedium")]
[assembly: ExportFont("Montserrat-Regular.ttf", Alias = "MontserratRegular")]
[assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "MontserratSemiBold")]
[assembly: ExportFont("Montserrat-Thin.ttf", Alias = "MontserratThin")]

namespace BMF_NewApp
{
    public partial class App
    {
        public static int ID { get; set; }
        public static ClsProfile profile { get; set; }
        public static string CPR { get; set; }
        public static string UserFullName { get; set; }
        public static string UserPhotoUrl { get; set; }
        public static bool IsAdmin { get; set; }
        public static bool IsLoggedIn { get; set; }
        public static bool ShowLogin { get; set; }
        public static bool ShowLogout { get; set; }

        static BMFAppDatabase database;


        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Constants.SyncFusionLicense);

            try
            {
                //var AddUserInfo = (from user in Database.GetItemsAsync<TblUsersAccount>().Result
                //                   where user.LoggedIn == true
                //                   select user).FirstOrDefault();

                //if (AddUserInfo == null)
                //{
                //    await NavigationService.NavigateAsync("BaseNavigationPage/LoginPage");
                //}
                //else
                //{
                //    Current.Properties["CPR"] = AddUserInfo.CPR;
                    await NavigationService.NavigateAsync("BaseNavigationPage/MainPage");
                //}
            }
            catch (System.Exception ex)
            {
                var properties = new Dictionary<string, string>
                {
                    { "App.xaml.cs", "OnInitialized" }
                };

                Crashes.TrackError(ex, properties);
            }

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //  RestService as a Singleton for dependency injection
            IRestService restService = new RestService();
            containerRegistry.RegisterInstance<IRestService>(restService);

            //  LocalService as a Singleton for dependency injection
            ILocalService localService = new LocalService();
            containerRegistry.RegisterInstance<ILocalService>(localService);

            // This updates INavigationService and registers PopupNavigation.Instance
            containerRegistry.RegisterPopupNavigationService();

            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<Xamarin.Forms.NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<BaseNavigationPage, BaseNavigationPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<CalendarPage, CalendarPageViewModel>();
            containerRegistry.RegisterForNavigation<MyLicensePage, MyLicensePageViewModel>();
            containerRegistry.RegisterForNavigation<CalendarRoundsPage, CalendarRoundsPageViewModel>();
            containerRegistry.RegisterForNavigation<MyIDsPage, MyIDsPageViewModel>();
            containerRegistry.RegisterForNavigation<ForgotPasswordPage, ForgotPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<NewAccountPage, NewAccountPageViewModel>();
            containerRegistry.RegisterForNavigation<NewsPage, NewsPageViewModel>();
            containerRegistry.RegisterForNavigation<NewsDetailsPage, NewsDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<ViewLicenseModalPage, ViewLicenseModalPageViewModel>();
            containerRegistry.RegisterForNavigation<BulletinsPage, BulletinsPageViewModel>();
            containerRegistry.RegisterForNavigation<RulesRegsPage, RulesRegsPageViewModel>();
            containerRegistry.RegisterForNavigation<BulletinsListPage, BulletinsListPageViewModel>();
            containerRegistry.RegisterForNavigation<RulesRegsListPage, RulesRegsListPageViewModel>();
            containerRegistry.RegisterForNavigation<ViewPdfPage, ViewPdfPageViewModel>();
            containerRegistry.RegisterForNavigation<RenewLicensePage, RenewLicensePageViewModel>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();
        }

        protected override void OnStart()
        {
            try
            {
                Xamarin.Forms.Application.Current.On<iOS>().SetEnableAccessibilityScalingForNamedFontSizes(false);


                AppCenter.Start("ios=52474204-5180-4298-9ae3-356ca41d35e6;" + "android=2b308c1a-7a40-4d46-be69-5b710a6b7562;", typeof(Analytics), typeof(Crashes));
            }
            catch (System.Exception ex)
            {
                var properties = new Dictionary<string, string>
                {
                    { "App.xaml.cs", "OnStart" }
                };

                Crashes.TrackError(ex, properties);
            }
        }

        public static BMFAppDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new BMFAppDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("BMFAppDB.db3"));
                }

                return database;
            }
        }
    }
}
