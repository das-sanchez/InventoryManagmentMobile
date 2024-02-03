using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.ViewModels;
using System.ComponentModel;

namespace InventoryManagmentMobile.Views;

public partial class ReceptionCDPage : ContentPage
{
    public ReceptionCDViewModel _vm { get; set; }

    public ReceptionCDPage(ReceptionCDViewModel viewModel)
    {
        InitializeComponent();
        _vm = viewModel;
        this.BindingContext = _vm;
        _vm.PropertyChanged += ViewModel_PropertyChanged;
        _vm.CanSave = false;
        _vm.MustToPrintDiff = false;


        if (BindingContext is ReceptionCDViewModel viewModel1)
        {
            viewModel1.FindProductRequested += OnFindProductRequested;
            viewModel1.FactorFocusRequested += OnFactorFocusRequested;
            viewModel1.QuantityFocusRequested += OnQuantityFocusRequested;
            viewModel1.ProductFocusRequested += OnProductFocusRequested;
            viewModel1.ManageToolbarItemVisibilityRequested += OnManageToolbarItemVisibilityRequested;
            _isEventSubscribed = true;
        }
    }

    private bool _isEventSubscribed = false;

    private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ReceptionCDViewModel.MustToPrintDiff))
        {
            ManageToolbarItemVisibility();
        }
    }

    private void ManageToolbarItemVisibility()
    {
        var viewModel = (ReceptionCDViewModel)BindingContext;

        if (!_vm.CanSave)
        {
            var itemToRemove = ToolbarItems.FirstOrDefault(item => item.Text == "Finalizar");
            if (itemToRemove != null)
                ToolbarItems.Remove(itemToRemove);
        }

        if (viewModel.MustToPrintDiff)
        {
            if (!ToolbarItems.Any(item => item.Text == "Imprimir Dif"))
            {
                var toolbarItem = new ToolbarItem
                {
                    Text = "Imprimir Dif",
                    Command = viewModel.PrintDiffCommand,
                    IsEnabled = viewModel.CanSave
                };

                ToolbarItems.Add(toolbarItem);
            }
        }
        else
        {
            var itemToRemove = ToolbarItems.FirstOrDefault(item => item.Text == "Imprimir Dif");
            if (itemToRemove != null)
                ToolbarItems.Remove(itemToRemove);

            if (_vm.CanSave && !ToolbarItems.Any(item => item.Text == "Finalizar"))
            {
                var toolbarItem = new ToolbarItem
                {
                    Text = "Finalizar",
                    Command = viewModel.SaveReceptionToolbarCommand,
                    IsEnabled = viewModel.CanSave
                };

                ToolbarItems.Add(toolbarItem);
            }
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (BindingContext is ReceptionCDViewModel viewModel1)
        {
            viewModel1.FindProductRequested -= OnFindProductRequested;
            viewModel1.FactorFocusRequested -= OnFactorFocusRequested;
            viewModel1.QuantityFocusRequested -= OnQuantityFocusRequested;
            viewModel1.ProductFocusRequested -= OnProductFocusRequested;
            viewModel1.ManageToolbarItemVisibilityRequested -= OnManageToolbarItemVisibilityRequested;
            _vm.PropertyChanged -= ViewModel_PropertyChanged;
            _isEventSubscribed = false;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ReceptionCDViewModel viewModel1 && !_isEventSubscribed)
        {
            _vm.FindProductRequested += OnFindProductRequested;
            _vm.FactorFocusRequested += OnFactorFocusRequested;
            _vm.QuantityFocusRequested += OnQuantityFocusRequested;
            _vm.ProductFocusRequested += OnProductFocusRequested;
            _vm.ManageToolbarItemVisibilityRequested += OnManageToolbarItemVisibilityRequested;
            _vm.PropertyChanged += ViewModel_PropertyChanged;
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

    public void OnManageToolbarItemVisibilityRequested()
    {
        ManageToolbarItemVisibility();
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

        qtyUnit.Focus();

        //if (!_vm.Product.Product.IsWeighed)
        //{
        //    qtyUnit.Focus();
        //}
        //else
        //{
        //    qtyUnit.Text = "1";
        //    btnAdd.Focus();
        //}
    }

    private async void qtyUnit_Completed(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(qtyUnit.Text))
        {
            qtyUnit.Text = "0";
            return;
        }

        //if (!_vm.Product.Product.IsWeighed)
        //{
        //    if ((decimal)_vm.Factor != Convert.ToDecimal(qtyUnit.Text))
        //    {

        //        await Application.Current.MainPage.DisplayAlert("Recepcion", "El Factor digitado es diferente al de la unidad ordenada.", "Aceptar");
        //        _vm.QtyUnit = "";
        //        qtyUnit.Focus();
        //        return;
        //    }
        //}
        //else
        //{
        //    qtyUnit.Text = "1";
        //}

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
