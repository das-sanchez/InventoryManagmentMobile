using InventoryManagmentMobile.ViewModels;

namespace InventoryManagmentMobile.Views;

public partial class TransferPage : ContentPage
{
    public TransferPage(TransferViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}