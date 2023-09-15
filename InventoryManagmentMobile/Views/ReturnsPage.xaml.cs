using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.ViewModels;
using Microsoft.Maui.Controls;
using System.Diagnostics;

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

    private async void vendor_Completed(object sender, EventArgs e)
    {

        _vm.Vendor = new VendorResult();
        _vm.Vendor = await _vm.Repo.VendorById(_vm.VendorNo);
        if (_vm.Vendor.Data == null)
        {
            await Application.Current.MainPage.DisplayAlert("Proveedor", "Proveedor no Existe", "Aceptar");
            return;
        }
        _vm.VendorName = _vm.Vendor.Data.Name;

        _vm.HasVendor = true;

        this.storageNo.Focus();

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

    private void storageNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnProduct.Focus();
    }

    private void btnProduct_Clicked(object sender, EventArgs e)
    {
        if (_vm.Vendor.Data == null)
        {
            this.vendor.Focus();
            return;
        }
        _vm.ProductosCommand.Execute(this);
        productNo.Focus();
    }



    private async void storageNo_Focused(object sender, FocusEventArgs e)
    {

    }



    private void LookBarCode_Completed(object sender, EventArgs e)
    {
        _vm.FindByProduct(this.LookBarCode.Text.Trim());
    }
}