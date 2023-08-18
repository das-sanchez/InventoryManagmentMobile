
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
        this.document.InputTransparent = true;
    }

    private void NoOrder_Completed(object sender, EventArgs e)
    {

        this.NoOrder.Unfocus();
        this.document.Focus();

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
            _vm.QtyUnit = 0;
            qtyUnit.Focus();
            return;
        }
        btnAdd.Focus();
    }

    private void btnRecibir_Clicked(object sender, EventArgs e)
    {
        _vm.ProductosCommand.Execute(this);
        productNo.Focus();
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
}