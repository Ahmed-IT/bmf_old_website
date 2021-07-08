using FFImageLoading.Forms.Platform;
using FFImageLoading.Transformations;
using Foundation;
using Prism;
using Prism.Ioc;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfBarcode.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.SfCarousel.XForms.iOS;
using Syncfusion.SfNavigationDrawer.XForms.iOS;
using Syncfusion.SfPdfViewer.XForms.iOS;
using Syncfusion.XForms.iOS.Border;
using Syncfusion.XForms.iOS.ComboBox;
using Syncfusion.XForms.iOS.TextInputLayout;
using UIKit;


namespace BMF_NewApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Rg.Plugins.Popup.Popup.Init();  //  RG Popup initialization

            SfTextInputLayoutRenderer.Init();  // Syncfusion SfTextInputLayout
            SfListViewRenderer.Init();  //  Syncfusion List View
            CachedImageRenderer.Init();  //  FF Image
            new TintTransformation();
            new SfNavigationDrawerRenderer();
            new SfBusyIndicatorRenderer();
            new SfBarcodeRenderer();
            SfBorderRenderer.Init();
            SfComboBoxRenderer.Init();
            new SfCarouselRenderer();
            SfPdfDocumentViewRenderer.Init();

            LoadApplication(new App(new iOSInitializer()));

            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            return base.FinishedLaunching(app, options);
        }

    }


    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
