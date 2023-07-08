using InventoryManagmentMobile.Repositories;
using InventoryManagmentMobile.Views;

namespace InventoryManagmentMobile;

public partial class App : Application
{
    private readonly OleRepository Repo;
    public App(OleRepository _repo)
    {

        InitializeComponent();
        Repo = _repo;
        MainPage = new LoginPage(new ViewModels.LoginViewModel(Repo));
    }
}
