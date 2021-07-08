using System;
using Xamarin.Forms;

namespace BMF_NewApp.Views
{
    public partial class BulletinsListPage : ContentPage
    {
        public BulletinsListPage()
        {
            InitializeComponent();
        }

        private void hamburgerButton_Clicked(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }
    }
}
