using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.Models.Dtos;
using InventoryManagmentMobile.Repositories;
using InventoryManagmentMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.ViewModels
{
    public class ReceptionCDViewModel : BaseViewModel
    {
        public Command GeneralCommand { get; }
        public Command ProductosCommand { get; }
        public Command DetalleCommand { get; }
        public Command ResumenCommand { get; }

        public Command AddItemCommand { get; }
        public Command<TransportationOrderItem> RemoveItemCommand { get; }
        public Command AddResumenCommand { get; }
        public Command RemoveResumenCommand { get; }
        public Command SaveReceptionCommand { get; }

        public ObservableCollection<DetailDto> Details { get; set; }
        public List<TranspOrderItem> Items { get; set; }
        public ObservableCollection<TransportationOrderItem> ReceptionItems { get; set; }
        public ObservableCollection<ItemSummary> ItemsSummary { get; set; }
        private readonly OleRepository repo;
        string _productNo = string.Empty;
        public string ProductNo { get { return _productNo; } set { SetProperty(ref _productNo, value); } }

        string _noLote = string.Empty;
        public string NoLote { get { return _noLote; } set { SetProperty(ref _noLote, value); } }

        string _unidad = string.Empty;
        public string Unidad { get { return _unidad; } set { SetProperty(ref _unidad, value); } }
        string _storeName = string.Empty;
        public string StoreName { get { return _storeName; } set { SetProperty(ref _storeName, value); } }

        string _invoice = string.Empty;
        public string InvoiceNumber { get { return _invoice; } set { SetProperty(ref _invoice, value); } }

        string _message = string.Empty;
        public string Message { get { return _message; } set { SetProperty(ref _message, value); } }
        string _image = string.Empty;
        public string Image { get { return _image; } set { SetProperty(ref _image, value); } }

        private TranspOrderResult _order;
        public TranspOrderResult Order { get => _order; set { SetProperty(ref _order, value); } }


        private TranspOrderItem _orderitem;
        public TranspOrderItem OrderItem { get => _orderitem; set { SetProperty(ref _orderitem, value); } }

        private TransportationOrderItem _item;
        public TransportationOrderItem Item { get => _item; set { SetProperty(ref _item, value); } }

        private TransportationOrderItem _itemSelected;
        public TransportationOrderItem ItemSelected { get => _itemSelected; set { SetProperty(ref _itemSelected, value); } }

        private ItemSummary _itemSummarySelected;
        public ItemSummary ItemSummrySelected { get => _itemSummarySelected; set { SetProperty(ref _itemSummarySelected, value); } }


        decimal _quantity;
        public decimal Quantity
        {
            get { return _quantity; }
            set
            {
                SetProperty(ref _quantity, value); if (value > 0) { TotalQty = Quantity; }
            }
        }
        decimal _totalQty;
        public decimal TotalQty
        {
            get { return _totalQty; }
            set { SetProperty(ref _totalQty, value); }
        }
        int _qtyUni;
        public int QtyUnit
        {
            get { return _qtyUni; }
            set { SetProperty(ref _qtyUni, value); }
        }
        public int Factor { get; set; }
        public ProductResult Product { get; set; }
        public bool IsBonus { get; set; } = false;

        public DateTime ExpirationDate { get; set; }
        public ObservableCollection<MeasurementUnit> MeasurementUnits { get; set; }
        public MeasurementUnit MeasurementSelected { get; set; }

        public TransportationOrder Reception { get; set; }

        public bool IsLotRequired { get; set; }

        private bool _isInvoiceRequired;
        public bool IsInvoiceRequired { get { return _isInvoiceRequired; } set { SetProperty(ref _isInvoiceRequired, value); } }

        private bool _showDialog;
        public bool ShowDialog { get { return _showDialog; } set { SetProperty(ref _showDialog, value); } }

        private bool _showContent;
        public bool ShowContent { get { return _showContent; } set { SetProperty(ref _showContent, value); } }

        public Command OkCommand { get; }
        public Command OrderByIdCommand { get; }
        public Command ProductByNoCommand { get; }

        public ReceptionCDViewModel(OleRepository _repo)
        {
            repo = _repo;
            Reception = new TransportationOrder();
            General = new PanelOption() { PanelVisible = true, BarColor = Color.FromRgba("#cc3300") };
            Productos = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
            Detalle = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
            Resumen = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };

            Items = new List<TranspOrderItem>();

            Details = new ObservableCollection<DetailDto>();


            GeneralCommand = new Command(() => GeneralOpcion());
            ProductosCommand = new Command(() => ProductosOpcion());
            DetalleCommand = new Command(() => DetalleOpcion());
            ResumenCommand = new Command(() => ResumenOpcion());
            OrderByIdCommand = new Command(async () => await OrderByIdAction());
            ProductByNoCommand = new Command(() => ProductByNo());
            AddItemCommand = new Command(() => AddItem());
            RemoveItemCommand = new Command<TransportationOrderItem>(RemoveItem);
            AddResumenCommand = new Command(() => AddItemSummary());
            RemoveResumenCommand = new Command(() => RemoveItemSummary());
            SaveReceptionCommand = new Command(() => SaveReception());
            OkCommand = new Command(() => OkReceptionAsync());
            OrderItem = new TranspOrderItem();
            ItemsSummary = new ObservableCollection<ItemSummary>();
            ReceptionItems = new ObservableCollection<TransportationOrderItem>();
            Item = new TransportationOrderItem();
            ItemSelected = new TransportationOrderItem();
            ItemSummrySelected = new ItemSummary();
            MeasurementUnits = new ObservableCollection<MeasurementUnit>();
            StoreName = Preferences.Get("storeName", "Default Value");

            ExpirationDate = DateTime.Now;
            ShowContent = true;
        }

        private async void RemoveItem(TransportationOrderItem item)
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", "Esta seguro de borrar?", "Si", "No");
            if (answer)
            {
                var det = Details.FirstOrDefault(x => x.ProductBarCode == item.ProductBarCode);
                ReceptionItems.Remove(ItemSelected);
                Details.Remove(det);
            }
        }

        private async Task OkReceptionAsync()
        {
            ShowContent = true;
            ShowDialog = false;
            await Shell.Current.Navigation.PopToRootAsync();
        }

        private async void SaveReception()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", "Desea guardar la Recepcion de la Orden de Compra?", "Yes", "No");
            if (answer)
            {
                if (ReceptionItems == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Guardar Recepcion", "No a Agregado detalle", "Aceptar");
                    return;
                }
                Reception.Items = ReceptionItems.ToArray();
                Reception.VendorId = Order.Data.VendorId;

                var Result = await repo.SaveTransporationOrder(OrderNo, Reception);
                if (Result != null && Result.IsSuccess)
                {

                    // ShowSucces("Transaccion Procesada Correctamente");
                    var dialogParam = new Dialog() { Icon = "checked2x", Description = "Receocion procesada correctamente", Title = "Recepcion Mercancia", Label = "Volver al Inicio" };



                    await Shell.Current.Navigation.PushModalAsync(new DialogAlert(new DialogAlertViewModel(dialogParam)));
                    Thread.Sleep(5000);

                    await Shell.Current.Navigation.PopToRootAsync();
                }
                else
                {

                    // ShowError(Result.MessagesFromErp[0].Message);
                    var dialogParam = new Dialog() { Icon = "cross2x", Description = "Receocion procesada correctamente", Title = "Recepcion Mercancia", Label = "Volver al Inicio" };



                    await Shell.Current.Navigation.PushModalAsync(new DialogAlert(new DialogAlertViewModel(dialogParam)));

                }

            }
            else
            {
                return;
            }
        }
        private void ShowError(string message)
        {
            ShowContent = false;
            ShowDialog = true;
            Message = message;
            Image = "cross2x.svg";
        }

        private void ShowSucces(string message)
        {
            ShowContent = false;
            ShowDialog = true;
            Message = message;
            Image = "checked2x.svg";
        }

        private void RemoveItemSummary()
        {
            throw new NotImplementedException();
        }

        private void AddItemSummary()
        {
            throw new NotImplementedException();
        }



        private async void AddItem()
        {

            if (Product == null)
            {
                await Application.Current.MainPage.DisplayAlert("Agregar Line", "Debe buscar un producto", "Aceptar");
                return;
            }
            if (Factor == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Agregar Line", "El Factor no debe ser Igual a Cero", "Aceptar");
                return;
            }
            if (TotalQty == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Agregar Line", "Debe digitar la cantidad  mayor de cero", "Aceptar");
                return;
            }

            int line = (ReceptionItems.Count == 0 ? 1 : ReceptionItems.Max(xc => xc.LineNo) + 1);
            if (!ReceptionItems.Any(xc => xc.ProductBarCode == ProductNo))
            {
                ReceptionItems.Add(new TransportationOrderItem() { ProductBarCode = ProductNo, ProductId = Product.Product.Id, Qty = TotalQty, QtyOrd = TotalQty, Um = OrderItem.Um });
                Details.Add(new DetailDto() { ProductBarCode = ProductNo, ProductId = Product.Product.Id, ProductName = OrderItem.ProductName, QtyPending = OrderItem.Qty - TotalQty, Quantity = OrderItem.Qty, QtyRecibida = TotalQty, Stock = 0 });
            }
            else
            {
                var recItem = ReceptionItems.FirstOrDefault(xc => xc.ProductBarCode == ProductNo);
                var detItem = Details.FirstOrDefault(xc => xc.ProductBarCode == ProductNo);
                bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", "El producto ya Existe desea Sumar (Sumar  = Yes - Sustituir = No)?", "Si", "No");
                if (answer)
                {

                    if (recItem != null)
                    {
                        ReceptionItems.ToList().ForEach((i) => { if (i.ProductBarCode == ProductNo) { i.Qty += TotalQty; } });
                        Details.ToList().ForEach((i) => { if (i.ProductBarCode == ProductNo) { i.QtyRecibida += TotalQty; i.QtyPending = i.Quantity - i.QtyRecibida; } });
                    }
                }
                else
                {

                    if (recItem != null)
                    {
                        recItem.Qty = TotalQty;
                        detItem.QtyRecibida = TotalQty;
                    }
                }
            }
            ProductNo = "";
            Quantity = 0;
            TotalQty = 0;
            QtyUnit = 0;
            Factor = 0;
            Unidad = "";
        }

        private async void ProductByNo()
        {
            try
            {
                //OrderItem = new OrderItem();
                if (string.IsNullOrEmpty(ProductNo)) return;
                OrderItem = Items.FirstOrDefault(xc => xc.ProductBarCode.Trim().Equals(ProductNo.Trim()));
                if (OrderItem == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Recepcion", "Producto no esta en la Orden de Transporte", "Aceptar");
                    return;
                }

                Product = new ProductResult();
                Product = await repo.ProductByBarCode(ProductNo);

                Product.Product.MeasurementUnits.ToList().ForEach((un) => { MeasurementUnits.Add(un); });
                var un = MeasurementUnits.FirstOrDefault(xc => xc.BaseUm == OrderItem.Um);
                if (un == null)
                {
                    throw new Exception($"Este Producto: {Product.Product.Name}  no contiene un factor para la unidad de medida:  {OrderItem.Um}");
                }
                Factor = un.Factor;
                Unidad = $"Cantidad ({OrderItem.Um})";

            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        private async Task OrderByIdAction()
        {
            try
            {


                Order = await repo.TranspOrderByOrderNo(OrderNo);
                if (Order.Data == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Trasanccion", Order.Message, "Aceptar");
                    return;
                }
                Items = Order.Data.Items.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        private void ResumenOpcion()
        {
            if (Order == null)
                return;

            ShowPanel("R");
        }

        private void DetalleOpcion()
        {

            ShowPanel("D");
        }

        private void ProductosOpcion()
        {
            if (Order == null)
                return;
            ShowPanel("P");
        }

        private void GeneralOpcion()
        {
            ShowPanel("G");
        }
        private void ShowPanel(string opcion)
        {
            switch (opcion)
            {
                case "G":
                    General = new PanelOption() { PanelVisible = true, BarColor = Color.FromRgba("#cc3300") };
                    Productos = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
                    Detalle = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
                    Resumen = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
                    break;
                case "P":
                    General = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
                    Productos = new PanelOption() { PanelVisible = true, BarColor = Color.FromRgba("#cc3300") };
                    Detalle = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
                    Resumen = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
                    break;
                case "D":
                    General = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
                    Productos = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
                    Detalle = new PanelOption() { PanelVisible = true, BarColor = Color.FromRgba("#cc3300") };
                    Resumen = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
                    break;
                case "R":
                    General = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
                    Productos = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
                    Detalle = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
                    Resumen = new PanelOption() { PanelVisible = true, BarColor = Color.FromRgba("#cc3300") };
                    break;

                default:
                    break;
            }
        }
    }
}
