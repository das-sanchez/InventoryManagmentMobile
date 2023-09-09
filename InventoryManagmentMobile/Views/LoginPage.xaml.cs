using InventoryManagmentMobile.ViewModels;

namespace InventoryManagmentMobile.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
        this.userName.Focus();

    }


    private void userName_Completed(object sender, EventArgs e)
    {
        this.password.Focus();
    }

    private void password_Completed(object sender, EventArgs e)
    {
        this.liststore.Focus();
    }
}