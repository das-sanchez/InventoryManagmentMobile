using InventoryManagmentMobile.ViewModels;

namespace InventoryManagmentMobile.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }

}