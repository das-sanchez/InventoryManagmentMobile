using InventoryManagmentMobile.ViewModels;

namespace InventoryManagmentMobile.Views;

public partial class RecepcionStorePage : ContentPage
{
    public RecepcionStorePage(ReceptionStoreViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}