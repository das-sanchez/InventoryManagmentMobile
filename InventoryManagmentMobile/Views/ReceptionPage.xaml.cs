
using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.ViewModels;

namespace InventoryManagmentMobile.Views;

public partial class ReceptionPage : ContentPage
{
    public ReceptionViewModel _vm { get; set; }
    public ReceptionPage(ReceptionViewModel viewModel)
    {
        InitializeComponent();
        _vm = viewModel;
        this.BindingContext = viewModel;

    }

    private void NoOrder_Completed(object sender, EventArgs e)
    {

        this.NoOrder.Unfocus();
        if (_vm.Type == "OC")
        {
            this.document.Focus();
        }
        else
        {
            _vm.ProductosCommand.Execute(this);
            productNo.Focus();
        }


    }

    private void productNo_Completed(object sender, EventArgs e)
    {
        if (_vm.OrderItem == null)
            this.productNo.Focus();
        qty.Focus();
    }

    private void qty_Completed(object sender, EventArgs e)
    {
        qtyUnit.Focus();
    }

    private async void qtyUnit_Completed(object sender, EventArgs e)
    {

        if (_vm.Factor != Convert.ToInt32(qtyUnit.Text))
        {

            await Application.Current.MainPage.DisplayAlert("Recepcion", "El Factor digitado es diferente al de la unidad ordenada.", "Aceptar");
            _vm.QtyUnit = string.Empty;

            qtyUnit.Focus();
            return;
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
        catch (Exception)
        {

            throw;
        }

    }



    private void document_Completed(object sender, EventArgs e)
    {
        this.document.Unfocus();
        this.btnRecibir.Focus();
    }

    private async void btnFinalizar_Clicked(object sender, EventArgs e)
    {
        var dialogParam = new Dialog() { Icon = "cross2x", Description = "Error al Procesar la Recepcion", Title = "Recepcion Mercancia", Label = "Volver" };



        await Shell.Current.Navigation.PushModalAsync(new DialogAlert(new DialogAlertViewModel(dialogParam)));
    }

    private void btnAdd_Clicked(object sender, EventArgs e)
    {
        this.productNo.Focus();
    }
}