using BMF_NewApp.iOS;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNaviRenderer))]
namespace BMF_NewApp.iOS
{
    public class CustomNaviRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
        }
        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            // We start to use DarkContent style mode since iOS 13
            return UIStatusBarStyle.DarkContent;
        }
    }
}