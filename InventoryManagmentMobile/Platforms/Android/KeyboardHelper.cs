using Android.Content;
using Android.Views.InputMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Platforms.Android
{
    public partial class KeyboardHelper
    {
        public void HideKeyboard()
        {
            var context = Platform.AppContext;
            var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            if (inputMethodManager != null)
            {
                var activity = Platform.CurrentActivity;
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
                activity.Window.DecorView.ClearFocus();
            }
        }
    }
}
