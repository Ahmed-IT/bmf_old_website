using BMF_NewApp.Models;
using BMF_NewApp.Services;
using Microsoft.AppCenter.Crashes;
using MvvmHelpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMF_NewApp.ViewModels
{
    public class BulletinsListPageViewModel : ViewModelBase
    {
        private ClsBulletinCategory _bulletin;
        public ClsBulletinCategory Bulletin
        {
            get { return _bulletin; }
            set { SetProperty(ref _bulletin, value); }
        }

        private ObservableRangeCollection<ClsBulletinSubCategory> _bulletinsList;
        public ObservableRangeCollection<ClsBulletinSubCategory> BulletinsList
        {
            get { return _bulletinsList; }
            set { SetProperty(ref _bulletinsList, value); }
        }

        private ClsProfile _profile;
        public ClsProfile Profile
        {
            get { return _profile; }
            set { SetProperty(ref _profile, value); }
        }

        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set { SetProperty(ref _isLoggedIn, value); }
        }

        private bool _showLogin;
        public bool ShowLogin
        {
            get { return _showLogin; }
            set { SetProperty(ref _showLogin, value); }
        }

        private bool _showLogout;
        public bool ShowLogout
        {
            get { return _showLogout; }
            set { SetProperty(ref _showLogout, value); }
        }

        private DelegateCommand<ClsBulletins> _categoryTappedCommand;
        public DelegateCommand<ClsBulletins> CategoryTappedCommand =>
            _categoryTappedCommand ?? (_categoryTappedCommand = new DelegateCommand<ClsBulletins>(ExecuteCatrogyTappedCommand));

        //  Injected service
        private readonly IRestService _restService;
        private readonly ILocalService _localService;

        public BulletinsListPageViewModel(INavigationService navigationService, IPageDialogService dialogService
            , ILocalService localService, IRestService restService)
            : base(navigationService, dialogService)
        {
            Title = "Bulletins";

            Bulletin = new ClsBulletinCategory();
            Profile = new ClsProfile();

            _localService = localService;
            _restService = restService;
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                if (App.IsLoggedIn == true)
                {
                    IsLoggedIn = true;
                    ShowLogout = true;
                    ShowLogin = false;
                }
                else
                {
                    IsLoggedIn = false;
                    ShowLogout = false;
                    ShowLogin = true;
                }

                if (App.profile != null)
                {
                    Profile = App.profile;
                }

                if (parameters["bulletins"] is ClsBulletinCategory _Bulletin)
                {
                    IsBusy = true;

                    Bulletin = _Bulletin;

                    if (Bulletin != null)
                    {
                        Title = Bulletin.Title + " Bulletins";
                    }

                    if (Bulletin.SubCategory.Count > 0)
                    {
                        var query = Bulletin.SubCategory.Select(a => new ClsBulletinSubCategory
                        {
                            Bulletins = a.Bulletins.Select(b => new ClsBulletins
                            {
                                ID = b.ID,
                                ID_SubCategory = b.ID_SubCategory,
                                PDF_Url = b.PDF_Url,
                                Title = b.Title,
                            }).ToList(),
                            ID = a.ID,
                            ID_Category = a.ID_Category,
                            Title = a.Title,
                            SectionHeight = (a.Bulletins.Where(c => c.ID_SubCategory == a.ID).Count() * 110)
                        });

                        BulletinsList = new ObservableRangeCollection<ClsBulletinSubCategory>(query.Where(a => a.Bulletins.Count > 0));
                    }

                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Error Loading Data", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "CalendarRoundsPageViewModel", "OnNavigatedTo" }
                };

                Crashes.TrackError(ex, properties);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ExecuteCatrogyTappedCommand(ClsBulletins obj)
        {
            try
            {
                // create and add some navigation paremters we will send an Event object
                var parameter = new NavigationParameters
            {
                { "bulletins", obj }
            };

                await NavigationService.NavigateAsync("ViewPdfPage", parameter);
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Navigating to Bulletins", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "BulletinsPageViewModel", "ExecuteCatrogyTappedCommand" }
                };

                Crashes.TrackError(ex, properties);
            }

        }

    }

}
