using InventoryManagmentMobile.ViewModels;

namespace InventoryManagmentMobile.Views
{
    public partial class DialogAlert : ContentPage
    {
        
        public DialogAlertViewModel _vm { get; set; }
        public DialogAlert(DialogAlertViewModel vm)
        {
            InitializeComponent();
            _vm = vm;

            this.BindingContext = _vm;
        }

        private void btnAceptar_Clicked(object sender, EventArgs e)
        {

        }
    }
}
