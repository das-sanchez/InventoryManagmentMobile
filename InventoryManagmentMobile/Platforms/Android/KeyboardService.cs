using Android.App;
using Android.Content;
using Android.OS;
using Android.Views.InputMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application = Android.App.Application;

namespace InventoryManagmentMobile.Platforms.Android
{
    public class KeyboardService : IKeyboardService
    {
        public void HideKeyboard()
        {
            var context = Application.Context;
            var inputManager = (InputMethodManager)context.GetSystemService(Context.InputMethodService);
            var currentFocusView = ((Activity)context).CurrentFocus;

            if (currentFocusView != null)
            {
                IBinder token = currentFocusView.WindowToken;
                inputManager?.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
            }
        }
    }
}
