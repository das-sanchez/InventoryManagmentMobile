using InventoryManagmentMobile.ViewModels;
using Microsoft.Maui.Controls;

namespace InventoryManagmentMobile.Views;

public partial class ReturnsPage : ContentPage
{
    public ReturnViewModel _vm { get; set; }
    public ReturnsPage(ReturnViewModel viewModel)
    {
        InitializeComponent();
        _vm = viewModel;
        this.BindingContext = viewModel;
        //document.Focus();
    }



    protected override void OnAppearing()
    {
        base.OnAppearing();


    }

    private void vendor_Completed(object sender, EventArgs e)
    {
        _vm.ProductosCommand.Execute(this);
        productNo.Focus();
        //_vm.VendorNo = sender.ToString();
        //_vm.GetProductCommand.Execute(default);
    }

    private void productNo_Completed(object sender, EventArgs e)
    {
        //productNo.Text = string.Empty;
        qty.Focus();

    }

    private void qty_Completed(object sender, EventArgs e)
    {
        productNo.Focus();
    }
}