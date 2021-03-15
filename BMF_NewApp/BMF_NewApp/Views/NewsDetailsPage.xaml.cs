using System;
using Xamarin.Forms;

namespace BMF_NewApp.Views
{
    public partial class NewsDetailsPage : ContentPage
    {
        public NewsDetailsPage()
        {
            InitializeComponent();
        }

        private void hamburgerButton_Clicked(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }
    }
}
