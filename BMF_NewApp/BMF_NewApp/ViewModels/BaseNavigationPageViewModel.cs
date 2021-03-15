using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMF_NewApp.ViewModels
{
    public class BaseNavigationPageViewModel : ViewModelBase
    {
        public BaseNavigationPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
             : base(navigationService, dialogService)
        {
            //  Set Title
            Title = Constants.AppTitle;
        }
    }
}
