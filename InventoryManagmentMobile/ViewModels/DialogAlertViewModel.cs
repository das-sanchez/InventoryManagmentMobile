using InventoryManagmentMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.ViewModels
{

    public class DialogAlertViewModel : BaseViewModel
    {
        Dialog dialog;
        public Dialog Dialog
        {
            get { return dialog; }
            set
            {
                SetProperty(ref dialog, value);

            }
        }
        public Command GoBackCommand { get; }
        public DialogAlertViewModel(Dialog _dialog)

        {
            Dialog = _dialog;
            GoBackCommand = new Command(() => GoBack());
        }

        private async void GoBack()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}
