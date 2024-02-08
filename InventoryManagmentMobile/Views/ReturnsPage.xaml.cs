using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.ViewModels;
using Microsoft.Maui.Controls;
using System.ComponentModel;
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
        SubscribeToEvents();
        _isEventSubscribed = true;
    }


    private bool _isEventSubscribed = false;
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (_isEventSubscribed)
        {
            UnsubscribeFromEvents();
            _isEventSubscribed = false;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (!_isEventSubscribed)
        {
            SubscribeToEvents();
            _isEventSubscribed = true;
        }
    }
    private void SubscribeToEvents()
    {
        if (BindingContext is ReturnViewModel viewModel1 && !_isEventSubscribed)
        {
            
            _vm.PropertyChanged += ViewModel_PropertyChanged;
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (BindingContext is ReturnViewModel viewModel1)
        {
            _vm.PropertyChanged -= ViewModel_PropertyChanged;
        }
    }


    private async void vendor_Completed(object sender, EventArgs e)
    {
        await _vm.VendorByNo();

        if (_vm.Vendor == null || (_vm.Vendor != null && _vm.Vendor.Data == null))
        {
            vendor.Focus();
            return;
        }
        else
        {
            _vm.VendorName = _vm.Vendor.Data.Name;
            _vm.HasVendor = true;
            this.storageNo.Focus();
        }
    }

    private async void productNo_Completed(object sender, EventArgs e)
    {
        await _vm.ProductByNo();

        if (_vm.Product == null || (_vm.Product != null && _vm.Product.Product == null))
        {
            _vm.InEdition = false;
            this.productNo.Focus();
            return;
        }
        else
        {
            this.qty.Focus();
        }


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
        //_vm.Vendor.Data == null
        if (!_vm.HasVendor )
        {
            Application.Current.MainPage.DisplayAlert("Error","Antes de recibir los productos, instroduzca un codigo de proveedor válido y presione enter para ejecutar la busqueda.", "Aceptar");
            this.vendor.Focus();
            return;
        }

        _vm.ProductosCommand.Execute(this);
        productNo.Focus();
    }



    private async void storageNo_Focused(object sender, FocusEventArgs e)
    {

    }



    //private void LookBarCode_Completed(object sender, EventArgs e)
    //{
    //    _vm.FindByProduct(this.LookBarCode.Text.Trim());
    //}

    private void productNo_Focused(object sender, FocusEventArgs e)
    {
        this.qty.Text = string.Empty;
        _vm.InEdition = false;
    }

    private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        
        if (e.PropertyName == nameof(ReturnViewModel.CanSave))
        {
            ManageToolbarItemVisibility();
        }
    }

    private void ManageToolbarItemVisibility()
    {
        var viewModel = (ReturnViewModel)BindingContext;

        if (!_vm.CanSave)
        {
            var itemToRemove = ToolbarItems.FirstOrDefault(item => item.Text == "Finalizar");
            if (itemToRemove != null)
                ToolbarItems.Remove(itemToRemove);
        }
        else if (!ToolbarItems.Any(item => item.Text == "Finalizar"))    
        {
            var toolbarItem = new ToolbarItem
            {
                Text = "Finalizar",
                Command = viewModel.SaveReturnCommand,
                IsEnabled = viewModel.CanSave
            };

            ToolbarItems.Add(toolbarItem);    
        }
    }
}