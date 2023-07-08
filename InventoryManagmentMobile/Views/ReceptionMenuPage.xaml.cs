using InventoryManagmentMobile.ViewModels;

namespace InventoryManagmentMobile.Views;

public partial class ReceptionMenuPage : ContentPage
{
    public ReceptionMenuPage(ReceptionMenuViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}