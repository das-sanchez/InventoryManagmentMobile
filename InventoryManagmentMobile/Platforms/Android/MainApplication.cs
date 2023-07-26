using Android.App;
using Android.Runtime;
using InventoryManagmentMobile.Platforms.Android;

namespace InventoryManagmentMobile;

[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
        //KeyboardHelper.HideKeyboard();
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
