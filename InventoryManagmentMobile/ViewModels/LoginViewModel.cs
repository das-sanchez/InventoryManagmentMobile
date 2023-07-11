using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {


        private string _user;
        public string User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }


        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        public Command LoginCommand { get; }

        public ObservableCollection<Store> Stores { get; set; }
        private Store _storeSelecte;
        public Store StoreSelected
        {
            get { return _storeSelecte; }
            set { SetProperty(ref _storeSelecte, value); Preferences.Set("storeNo", value.Id); Preferences.Set("storeName", (value.CompanyId != 5520 ? "HipperOle " + value.Name : "Mercadal " + value.Name)); }
        }
        private StoreResult _store;
        public StoreResult StoreResult
        {
            get { return _store; }
            set { SetProperty(ref _store, value); }
        }
        private readonly OleRepository Repo;
        public LoginViewModel(OleRepository _repo)
        {
            Repo = _repo;
            StoreResult = new StoreResult();
            Stores = new ObservableCollection<Store>();
            LoginCommand = new Command(async () => await Login());
            StoreLoad();
        }
        private async void StoreLoad()
        {

            StoreResult = await Repo.StoreByNo("");
            Stores.Clear();
            StoreResult.Stores.ToList().ForEach((s) =>
            {
                if (s.CompanyId == 5520)
                {
                    s.Name = "Mercadal - " + s.Name;
                }
                else
                {
                    s.Name = "Ole - " + s.Name;
                }
                Stores.Add(s);
            });
        }
        private async Task Login()
        {
            Application.Current.MainPage = new AppShell();
        }
    }
}

