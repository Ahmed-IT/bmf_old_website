using System;
using Xamarin.Forms;

namespace BMF_NewApp.Views
{
    public partial class MyLicensePage : ContentPage
    {
        public MyLicensePage()
        {
            InitializeComponent();
        }

        private void hamburgerButton_Clicked(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }
    }
}
