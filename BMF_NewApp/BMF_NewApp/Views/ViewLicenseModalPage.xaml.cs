using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms; // use Xamarin.Forms

namespace BMF_NewApp.Views
{
    public partial class ViewLicenseModalPage : PopupPage
    {
        public ViewLicenseModalPage()
        {
            InitializeComponent();
        }

        private async void CloseClick(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}