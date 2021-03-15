using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMF_NewApp.ViewModels
{
    public class MainMenuModalPageViewModel : ViewModelBase
    {
        public MainMenuModalPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {

        }

    }
}
