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
        this.qty.InputTransparent = true;
        this.qtyUnit.InputTransparent = true;
    }

    private void NoOrder_Completed(object sender, EventArgs e)
    {

        this.NoOrder.Unfocus();
    }

    private void productNo_Completed(object sender, EventArgs e)
    {
        qty.Focus();
    }

    private void qty_Completed(object sender, EventArgs e)
    {
        qtyUnit.Focus();
    }

    private void qtyUnit_Completed(object sender, EventArgs e)
    {
        btnAdd.Focus();
    }

    private void btnRecibir_Clicked(object sender, EventArgs e)
    {
        _vm.ProductosCommand.Execute(this);
        productNo.Focus();
    }
}