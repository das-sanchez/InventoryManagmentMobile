using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.Views;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<MenuOption> list { get; set; }
        public Command<MenuOption> ItemTapped { get; }
        private bool _showContent;
        public bool ShowContent { get { return _showContent; } set { SetProperty(ref _showContent, value); } }
        public HomeViewModel()
        {
            list = new ObservableCollection<MenuOption>();
            list.Add(new MenuOption { Id = "RE", Titulo = "Recibir", Icon = "recibo2x.svg" });
            list.Add(new MenuOption { Id = "D", Titulo = "Enviar", Icon = "caja2.svg" });
            list.Add(new MenuOption { Id = "DE", Titulo = "Devolver", Icon = "devolver2x.svg" });

            ItemTapped = new Command<MenuOption>(OnItemSelected);
            ShowContent = true;
            IsBusy = false;
        }

        private async void OnItemSelected(MenuOption obj)
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


            if (obj == null)
                return;
            var navigationParameter = new Dictionary<string, object> { { "Type", obj.Id } };
            switch (obj.Id)
            {
                case "RE":

                    await Shell.Current.GoToAsync(nameof(ReceptionMenuPage));


                    break;
                case "D":
                    await Shell.Current.GoToAsync(nameof(ReceptionPage), navigationParameter);

                    break;
                case "DE":

                    await Shell.Current.GoToAsync(nameof(ReturnsPage));
                    break;

                default:
                    break;
            }
            IsBusy = false;
            ShowContent = true;
        }
    }
}
