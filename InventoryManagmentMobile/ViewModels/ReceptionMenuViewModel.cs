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

    public class ReceptionMenuViewModel
    {
        public ObservableCollection<MenuOption> list { get; set; }
        public Command<MenuOption> ItemTapped { get; }
        public ReceptionMenuViewModel()
        {
            list = new ObservableCollection<MenuOption>();
            list.Add(new MenuOption { Id = "RE", Titulo = "Orden de Compra Proveedor", Icon = "checklist2x.svg" });
            list.Add(new MenuOption { Id = "RS", Titulo = "Transferencia desde otra Tienda", Icon = "transfer2x.svg" });
            list.Add(new MenuOption { Id = "CD", Titulo = "Transferencia desde el Centro de Distribucion", Icon = "warehouse2x.svg" });
            ItemTapped = new Command<MenuOption>(OnItemSelected);
        }

        private async void OnItemSelected(MenuOption option)
        {
            if (option == null)
                return;
            switch (option.Id)
            {
                case "RE":

                    await Shell.Current.GoToAsync(nameof(ReceptionPage));


                    break;
                case "CD":

                    await Shell.Current.GoToAsync(nameof(ReceptionCDPage));

                    break;
                case "RS":

                    await Shell.Current.GoToAsync(nameof(RecepcionStorePage));
                    break;

                default:
                    break;
            }
        }
    }
}
