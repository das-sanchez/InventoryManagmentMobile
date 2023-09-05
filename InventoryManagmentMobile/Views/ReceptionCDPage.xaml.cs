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



    }
    private void NoOrder_Completed(object sender, EventArgs e)
    {

        this.NoOrder.Unfocus();
        _vm.ProductosCommand.Execute(this);
        productNo.Focus();


    }

    private void productNo_Completed(object sender, EventArgs e)
    {
        if (_vm.OrderItem == null)
            productNo.Focus();

        qty.Focus();
    }

    private void qty_Completed(object sender, EventArgs e)
    {
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

        if (!_vm.Product.Product.IsWeighed)
        {
            if (_vm.Factor != Convert.ToInt32(qtyUnit.Text))
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
        if (_vm.Order == null)
        {
            NoOrder.Focus();
            return;
        }
        _vm.ProductosCommand.Execute(this);
        productNo.Focus();
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
}
