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
    private void OnHideKeyboardClicked(object sender, EventArgs e)
    {
        // Call the platform-specific method to hide the keyboard
        var keyboardService = DependencyService.Get<IKeyboardService>();
        keyboardService?.HideKeyboard();
    }

    private void userName_Focused(object sender, FocusEventArgs e)
    {
        var keyboardService = DependencyService.Get<IKeyboardService>();
        keyboardService?.HideKeyboard();
    }
}