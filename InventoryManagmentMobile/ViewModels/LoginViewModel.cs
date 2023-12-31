﻿using InventoryManagmentMobile.Database;
using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.Repositories;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

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

            set 
            {     
                SetProperty(ref _storeSelecte, value); 

                if (value != null)
                {
                    Preferences.Set("storeNo", value.Id); 
                    Preferences.Set("storeName", value.Name);
                }
            }
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
        private UserStore _userStoreSelected;
        public UserStore UserStoreSelected
        {
            get { return _userStoreSelected; }
            set { SetProperty(ref _userStoreSelected, value); }
        }
        private readonly OleDataContext _context;
        private readonly OleRepository Repo;
        public LoginViewModel(OleRepository _repo, OleDataContext context)
        {
            Repo = _repo;
            _context = context;
            StoreResult = new StoreResult();
            LogResult = new LoginResult();
            Stores = new ObservableCollection<Store>();
            LoginCommand = new Command(async () => await Login());
            //StoreLoad();
        }
        private async void StoreLoad()
        {
            try
            {
                StoreResult = await Repo.StoreByNo("");
                Stores.Clear();

                if (!StoreResult.IsSuccess)
                    throw new Exception(StoreResult.Message);
                

                StoreResult.Stores = StoreResult.Stores.OrderBy(x => x.CompanyId).ThenBy(x => x.Name).ToArray();

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
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Login Error", ex.Message, "Aceptar");
                return;
            }

        }
        public async Task GetStoreListByUser(string username)
        {
            try
            {
                //Stores = new ObservableCollection<Store>();


                StoreResult = null;
                 
                var storesByUsers = await Repo.GetStoreListByUser(username);
                
                if (storesByUsers == null)
                    throw new Exception("Se produjo un error al consultar las tiendas para el usuario, no se obtuvo ninguna información.");

                if (!storesByUsers.IsSuccess)
                    throw new Exception(storesByUsers.Message);

                StoreResult = new StoreResult()
                {
                    IsSuccess = storesByUsers.IsSuccess,
                    Message = storesByUsers.Message,
                    MessagesFromErp = storesByUsers.MessagesFromErp,
                    Stores = storesByUsers.Data.Select(x => new Store
                    {
                        Name = x.StoreName,
                        Id = x.StoreId,
                    }).ToArray()
                };

                Stores.Clear();

                //StoreResult.Stores = StoreResult.Stores.OrderBy(x => x.CompanyId).ThenBy(x => x.Name).ToArray();

                StoreResult.Stores.OrderBy(x => x.Name).ToList().ForEach((s) =>
                {
                    Stores.Add(s);
                });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }

        }
        public void GetStoreByUser()
        {
            //StoreSelected = new Store();

            if (_context.ValidExistUserStore(User))
            {
                if (!string.IsNullOrEmpty(User))
                {
                    var us = _context.GetUserStore(User);

                    if (Stores.Any(x => x.Id == us.StoreNo))
                        StoreSelected = Stores.FirstOrDefault(x => x.Id == us.StoreNo);
                }
            }


        }

        private async Task Login()
        {
            try
            {
                if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
                    throw new Exception("Usuario o Password esta en Blanco");
                   
                if (StoreSelected == null)
                    throw new Exception("Debe seleccionar una tienda");
                    
                if (_context.ValidExistUserStore(User))
                {
                    var us = _context.GetUserStore(User);

                    if (us.StoreNo != StoreSelected.Id)
                    {
                        bool answer = await Application.Current.MainPage.DisplayAlert("Login", $"La tienda {StoreSelected.Name} seleccionada es diferente a la que tiene por defecto. ¿Desea cambiarla?", "Si", "No");

                        if (answer)
                            _context.SaveUserStore(new UserStore() { Id = us.Id, StoreNo = StoreSelected.Id, UserId = User });
                    }
                }
                else
                {
                    _context.SaveUserStore(new UserStore() { Id = 0, StoreNo = StoreSelected.Id, UserId = User });
                }

                LogIn = new Login();
                LogIn.StoreId = StoreSelected.Id;
                LogIn.UserName = User;
                LogIn.Password = Password;
                LogResult = new LoginResult();
                LogResult = await Repo.Login(LogIn);

                if (!LogResult.IsSuccess)
                    throw new Exception(LogResult.Message);
                    
                Preferences.Set("token", LogResult.Data.Token);
                Application.Current.MainPage = new AppShell();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }

        }
    }
}

