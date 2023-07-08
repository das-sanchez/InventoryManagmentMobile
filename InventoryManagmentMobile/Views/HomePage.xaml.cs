using InventoryManagmentMobile.ViewModels;

namespace InventoryManagmentMobile.Views;

public partial class HomePage : ContentPage
{
    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}