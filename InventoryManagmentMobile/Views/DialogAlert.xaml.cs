using InventoryManagmentMobile.ViewModels;

namespace InventoryManagmentMobile.Views
{
    public delegate void DialogResponseHandler(string response);
    public partial class DialogAlert : ContentPage
    {
        public event DialogResponseHandler OnDialogClosed;
        public DialogAlertViewModel _vm { get; set; }
        public DialogAlert(DialogAlertViewModel vm)
        {
            InitializeComponent();
            _vm = vm;

            this.BindingContext = _vm;
        }
        private async void btnAceptar_Clicked(object sender, EventArgs e)
        {
            string response = btnAceptar.Text; // Esto podría ser un valor de algún control en tu cuadro de diálogo
            OnDialogClosed?.Invoke(response);
            await Navigation.PopModalAsync();
        }
    }
}
