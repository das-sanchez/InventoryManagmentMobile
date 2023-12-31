using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.ViewModels;
using System.Reflection.Metadata;

namespace InventoryManagmentMobile.Views;

public partial class ReceptionCDPage : ContentPage
{
    public ReceptionCDViewModel _vm { get; set; }

    public ReceptionCDPage(ReceptionCDViewModel viewModel)
    {
        InitializeComponent();
        _vm = viewModel;
        this.BindingContext = viewModel;

        if (BindingContext is ReceptionCDViewModel viewModel1)
        {
            viewModel1.FindProductRequested += OnFindProductRequested;
            viewModel1.FactorFocusRequested += OnFactorFocusRequested;
            viewModel1.QuantityFocusRequested += OnQuantityFocusRequested;
            viewModel1.ProductFocusRequested += OnProductFocusRequested;
        }

    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (BindingContext is ReceptionViewModel viewModel1)
        {
            viewModel1.FindProductRequested -= OnFindProductRequested;
            viewModel1.FactorFocusRequested -= OnFactorFocusRequested;
            viewModel1.QuantityFocusRequested -= OnQuantityFocusRequested;
            viewModel1.ProductFocusRequested -= OnProductFocusRequested;
        }
    }
    private void OnFindProductRequested()
    {
        if (!String.IsNullOrEmpty(this.productNo.Text))
            productNo_Completed(productNo, EventArgs.Empty);
    }

    public void OnFactorFocusRequested()
    {
        qtyUnit.Focus();
    }
    public void OnQuantityFocusRequested()
    {
        qty.Focus();
    }
    public void OnProductFocusRequested()
    {
        productNo.Focus();
    }
    private void NoOrder_Completed(object sender, EventArgs e)
    {
        this.NoOrder.Unfocus();
        _vm.ProductosCommand.Execute(this);
        productNo.Focus();
    }

    private async void productNo_Completed(object sender, EventArgs e)
    {
        await _vm.ProductByNo();

        if (_vm.Product == null || (_vm.Product != null && _vm.Product.Product == null))
        {
            _vm.NotEdition = true;
            _vm.InEdition = false;
            this.productNo.Focus();
            return;
        }

        qty.Focus();
    }

    private void qty_Completed(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(qty.Text))
        {
            qty.Text = "0";
            return;
        }

        if (!_vm.Product.Product.IsWeighed)
        {
            qtyUnit.Focus();
        }
        else
        {
            qtyUnit.Text = "1";
            btnAdd.Focus();
        }
    }

    private async void qtyUnit_Completed(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(qtyUnit.Text))
        {
            qtyUnit.Text = "0";
            return;
        }

        if (!_vm.Product.Product.IsWeighed)
        {
            if ((decimal)_vm.Factor != Convert.ToDecimal(qtyUnit.Text))
            {

                await Application.Current.MainPage.DisplayAlert("Recepcion", "El Factor digitado es diferente al de la unidad ordenada.", "Aceptar");
                _vm.QtyUnit = "";
                qtyUnit.Focus();
                return;
            }
        }
        else
        {
            qtyUnit.Text = "1";
        }

        btnAdd.Focus();
    }

    private void btnRecibir_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (_vm.Order == null)
            {
                NoOrder.Focus();
                return;
            }

            _vm.ProductosCommand.Execute(this);
            productNo.Focus();
        }
        catch (Exception ex )
        {
            Application.Current.MainPage.DisplayAlert("Recepcion", ex.Message, "Aceptar");
        }

    }



    private void document_Completed(object sender, EventArgs e)
    {

        this.btnRecibir.Focus();
    }

    private async void btnFinalizar_Clicked(object sender, EventArgs e)
    {
        var dialogParam = new Dialog() { Icon = "cross2x", Description = "Error al Procesar la Recepcion", Title = "Recepcion Mercancia", Label = "Volver" };
        await Shell.Current.Navigation.PushModalAsync(new DialogAlert(new DialogAlertViewModel(dialogParam)));
    }

    private void NoOrder_TextChanged(object sender, TextChangedEventArgs e)
    {
        //DependencyService.Get<IKeyboardService>().HideKeyboard();
    }

    private void btnAdd_Clicked(object sender, EventArgs e)
    {

        //this.productNo.Focus();
    }

    private void LookBarCode_Completed(object sender, EventArgs e)
    {
        _vm.FindByProduct(this.LookBarCode.Text);
    }
}
