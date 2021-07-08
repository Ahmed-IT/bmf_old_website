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
    public class RulesRegsListPageViewModel : ViewModelBase
    {
        private ClsRulesRegsCategory _rulesRegs;
        public ClsRulesRegsCategory RulesRegs
        {
            get { return _rulesRegs; }
            set { SetProperty(ref _rulesRegs, value); }
        }

        private ObservableRangeCollection<ClsRulesRegsSubCategory> _rulesRegsList;
        public ObservableRangeCollection<ClsRulesRegsSubCategory> RulesRegsList
        {
            get { return _rulesRegsList; }
            set { SetProperty(ref _rulesRegsList, value); }
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

        private DelegateCommand<ClsRulesRegs> _categoryTappedCommand;
        public DelegateCommand<ClsRulesRegs> CategoryTappedCommand =>
            _categoryTappedCommand ?? (_categoryTappedCommand = new DelegateCommand<ClsRulesRegs>(ExecuteCatrogyTappedCommand));


        //  Injected service
        private readonly IRestService _restService;
        private readonly ILocalService _localService;

        public RulesRegsListPageViewModel(INavigationService navigationService, IPageDialogService dialogService
            , ILocalService localService, IRestService restService)
            : base(navigationService, dialogService)
        {
            Title = "Rules & Regs";

            RulesRegs = new ClsRulesRegsCategory();
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

                if (parameters["rulesRegs"] is ClsRulesRegsCategory _RulesRegs)
                {
                    IsBusy = true;

                    RulesRegs = _RulesRegs;

                    if (RulesRegs != null)
                    {
                        Title = RulesRegs.Title + " Rules & Regs";
                    }

                    if (RulesRegs.SubCategory.Count > 0)
                    {
                        var query = RulesRegs.SubCategory.Select(a => new ClsRulesRegsSubCategory
                        {
                            RulesRegs = a.RulesRegs.Select(b => new ClsRulesRegs
                            {
                                ID = b.ID,
                                ID_SubCategory = b.ID_SubCategory,
                                PDF_Url = b.PDF_Url,
                                Title = b.Title,
                            }).ToList(),
                            ID = a.ID,
                            ID_Category = a.ID_Category,
                            Title = a.Title,
                            SectionHeight = (a.RulesRegs.Where(c => c.ID_SubCategory == a.ID).Count() * 110)
                        });

                        RulesRegsList = new ObservableRangeCollection<ClsRulesRegsSubCategory>(query.Where(a => a.RulesRegs.Count > 0));
                    }

                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Error Loading Data", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "RulesRegsListPageViewModel", "OnNavigatedTo" }
                };

                Crashes.TrackError(ex, properties);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ExecuteCatrogyTappedCommand(ClsRulesRegs obj)
        {
            try
            {
                // create and add some navigation paremters we will send an Event object
                var parameter = new NavigationParameters
            {
                { "rulesRegs", obj }
            };

                await NavigationService.NavigateAsync("ViewPdfPage", parameter);
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Navigating to Bulletins", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "RulesRegsListPageViewModel", "ExecuteCatrogyTappedCommand" }
                };

                Crashes.TrackError(ex, properties);
            }

        }

    }
}
