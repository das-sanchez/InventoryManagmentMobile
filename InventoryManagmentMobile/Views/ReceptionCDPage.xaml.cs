using InventoryManagmentMobile.ViewModels;

namespace InventoryManagmentMobile.Views;

public partial class ReceptionCDPage : ContentPage
{
    public ReceptionCDPage(ReceptionCDViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}