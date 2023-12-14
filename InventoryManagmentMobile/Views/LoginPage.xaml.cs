using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.ViewModels;

namespace InventoryManagmentMobile.Views;

public partial class LoginPage : ContentPage
{
    public LoginViewModel _vm { get; set; }
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        _vm = viewModel;
        this.BindingContext = viewModel;
        this.userName.Focus();

    }


    private async void userName_Completed(object sender, EventArgs e)
    {
        await _vm.GetStoreListByUser(userName.Text);
        _vm.GetStoreByUser();
        this.password.Focus();
    }

    private void password_Completed(object sender, EventArgs e)
    {
        this.liststore.Focus();
    }


    private async void userName_Unfocused(object sender, FocusEventArgs e)
    {
        await _vm.GetStoreListByUser(userName.Text);
        _vm.GetStoreByUser();

    }
}