using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.ViewModels
{

    public class ReceptionMenuViewModel : BaseViewModel
    {
        public ObservableCollection<MenuOption> list { get; set; }
        public Command<MenuOption> ItemTapped { get; }
        private bool _showContent;
        public bool ShowContent { get { return _showContent; } set { SetProperty(ref _showContent, value); } }
        public ReceptionMenuViewModel()
        {
            list = new ObservableCollection<MenuOption>();
            list.Add(new MenuOption { Id = "P", Titulo = "Orden de Compra Proveedor", Icon = "checklist2x.svg" });
            list.Add(new MenuOption { Id = "T", Titulo = "Transferencia desde otra Tienda", Icon = "transfer2x.svg" });
            list.Add(new MenuOption { Id = "CD", Titulo = "Transferencia desde el Centro de Distribucion", Icon = "warehouse2x.svg" });
            ItemTapped = new Command<MenuOption>(OnItemSelected);
            IsBusy = false;
            ShowContent = true;
        }

        private async void OnItemSelected(MenuOption option)
        {
            if (IsBusy)
            {
                return;
            }
            else
            {
                ShowContent = false;
                IsBusy = true;
            }

            var navigationParameter = new Dictionary<string, object> { { "Type", option.Id } };
            if (option == null)
                return;
            switch (option.Id)
            {
                case "P":

                    await Shell.Current.GoToAsync(nameof(ReceptionPage), navigationParameter);


                    break;
                case "T":


                    await Shell.Current.GoToAsync(nameof(ReceptionPage), navigationParameter);


                    break;
                case "CD":

                    await Shell.Current.GoToAsync(nameof(ReceptionCDPage));
                    break;

                default:
                    break;
            }
            IsBusy = false;
            ShowContent = true;
        }
    }
}
