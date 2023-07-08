﻿using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.ViewModels
{
    public class HomeViewModel
    {
        public ObservableCollection<MenuOption> list { get; set; }
        public Command<MenuOption> ItemTapped { get; }
        public HomeViewModel()
        {
            list = new ObservableCollection<MenuOption>();
            list.Add(new MenuOption { Id = "RE", Titulo = "Recibir", Icon = "recibo2x.svg" });
            list.Add(new MenuOption { Id = "EN", Titulo = "Enviar", Icon = "caja2.svg" });
            list.Add(new MenuOption { Id = "DE", Titulo = "Devolver", Icon = "devolver2x.svg" });

            ItemTapped = new Command<MenuOption>(OnItemSelected);
        }

        private async void OnItemSelected(MenuOption obj)
        {
            if (obj == null)
                return;
            switch (obj.Id)
            {
                case "RE":

                    await Shell.Current.GoToAsync(nameof(ReceptionMenuPage));


                    break;
                case "EN":

                    await Shell.Current.GoToAsync(nameof(TransferPage));

                    break;
                case "DE":

                    await Shell.Current.GoToAsync(nameof(ReturnsPage));
                    break;

                default:
                    break;
            }
        }
    }
}
