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


    private void userName_Completed(object sender, EventArgs e)
    {
        _vm.GetStoreByUser();
        this.password.Focus();
    }

    private void password_Completed(object sender, EventArgs e)
    {
        this.liststore.Focus();
    }

    private void password_Focused(object sender, FocusEventArgs e)
    {
        _vm.GetStoreByUser();
    }
}