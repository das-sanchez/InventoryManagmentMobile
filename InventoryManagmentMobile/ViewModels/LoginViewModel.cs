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
            set { SetProperty(ref _storeSelecte, value); Preferences.Set("storeNo", value.Id); Preferences.Set("storeName", (value.CompanyId != "5520" ? "HipperOle " + value.Name : "Mercadal " + value.Name)); }
        }
        private StoreResult _store;
        public StoreResult StoreResult
        {
            get { return _store; }
            set { SetProperty(ref _store, value); }
        }
        private LoginResult _loginresult;
        public LoginResult LogResult
        {
            get { return _loginresult; }
            set { SetProperty(ref _loginresult, value); }
        }
        private Login _login;
        public Login LogIn
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }
        private readonly OleRepository Repo;
        public LoginViewModel(OleRepository _repo)
        {
            Repo = _repo;
            StoreResult = new StoreResult();
            LogResult = new LoginResult();
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
                if (s.CompanyId == "5520")
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
            try
            {
                if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
                {
                    await Application.Current.MainPage.DisplayAlert("Login Error", "Usuario o Password esta en Blanco", "Aceptar");
                    return;
                }


                if (StoreSelected == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Login Error", "Debe Seleccionar un Store", "Aceptar");
                    return;
                }

                LogIn = new Login();
                LogIn.StoreId = StoreSelected.Id;
                LogIn.UserName = User;
                LogIn.Password = Password;
                LogResult = new LoginResult();
                LogResult = await Repo.Login(LogIn);

                if (!LogResult.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Login Error", LogResult.Message, "Aceptar");
                    return;
                }
                Preferences.Set("token", LogResult.Data.Token);
                Application.Current.MainPage = new AppShell();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}

