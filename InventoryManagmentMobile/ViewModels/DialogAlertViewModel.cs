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
        public bool UserHasInteracted { get; private set; } = false;
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
        public DialogAlertViewModel(Dialog _dialog, bool goToBeginning)

        {
            Dialog = _dialog;

            //GoBackCommand = new Command(() => (goToBeginning ? GoToBeginning() : GoBack()));
            //GoBackCommand = new Command(() => (goToBeginning ? GoToBeginning() : GoBack()));
            GoBackCommand = new Command(() =>
            {
                if (goToBeginning)
                    GoToBeginning();
                else
                    GoBack();

                UserHasInteracted = true;
            });
        }



        private async void GoBack()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private async void GoToBeginning()
        {
            await Shell.Current.Navigation.PopToRootAsync();
        }
    }
}
