using InventoryManagmentMobile.Database;
using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.Models.Dtos;
using InventoryManagmentMobile.Repositories;
using InventoryManagmentMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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


        string _quantity;
        public string Quantity
        {
            get { return _quantity; }
            set
            {
                SetProperty(ref _quantity, value); if (!string.IsNullOrEmpty(value)) { TotalQty = Convert.ToDecimal(Quantity); }
            }
        }
        decimal _totalQty;
        public decimal TotalQty
        {
            get { return _totalQty; }
            set { SetProperty(ref _totalQty, value); }
        }
        string _qtyUni;
        public string QtyUnit
        {
            get { return _qtyUni; }
            set { SetProperty(ref _qtyUni, value); }
        }
        int _factor = 0;
        public int Factor
        {
            get { return _factor; }
            set { SetProperty(ref _factor, value); }
        }
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
        private bool _hasOrder = false;
        public bool HasOrder { get { return _hasOrder; } set { SetProperty(ref _hasOrder, value); } }


        public Command BackCommand { get; }

        string _storeNo = string.Empty;
        public string StoreNo { get { return _storeNo; } set { SetProperty(ref _storeNo, value); } }

        private readonly OleDataContext _context;
        public Command<DetailDto> RemoveDetailItemCommand { get; }

        public ReceptionCDViewModel(OleRepository _repo, OleDataContext context)
        {
            repo = _repo;
            _context = context;
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
            StoreNo = Preferences.Get("storeNo", "Default Value");

            ExpirationDate = DateTime.Now;
            ShowContent = true;
            BackCommand = new Command(() => BackSync());
            RemoveDetailItemCommand = new Command<DetailDto>(RemoveDetailItem);
        }
        private async void RemoveDetailItem(DetailDto dto)
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", "Esta seguro de borrar?", "Si", "No");
            if (answer)
            {
                var item = ReceptionItems.FirstOrDefault(x => x.ProductBarCode == dto.ProductBarCode);
                ReceptionItems.Remove(item);
                //Details.Remove(dto);
                _context.ExecuteSql($"DELETE FROM TransactionLine Where OrderNo = '{OrderNo}' AND ProductBarCode = '{dto.ProductBarCode}'");
                LoadItemsAsync();
            }
        }
        private async void BackSync()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", "Esta seguro de Salir?", "Si", "No");
            if (answer)
            {
                await Shell.Current.Navigation.PopAsync();
            }
        }
        public void LoadItemsAsync()
        {
            var items = new List<TransactionLine>();
            Details.Clear();
            ReceptionItems.Clear();
            items = _context.GetTransactionLinesByOrderNo(OrderNo);
            if (items is not null && items.Any())
            {

                foreach (var line in items)
                {
                    Details.Add(new DetailDto() { ProductBarCode = line.ProductBarCode, ProductId = line.ProductId, ProductName = line.ProductName, Quantity = line.Quantity, QtyRecibida = line.QtyRecibida, QtyPending = line.QtyPending, Stock = 0, Um = line.Um, Bono = line.Bono, Color = (line.Bono ? "#bdebca" : "#fffffff") });
                    ReceptionItems.Add(new TransportationOrderItem() { ProductBarCode = line.ProductBarCode, ProductId = line.ProductId, LineNo = line.LineNo, Qty = line.QtyRecibida, Um = line.Um, QtyOrd = line.Quantity });
                }
            }

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
                    var dialogParam = new Dialog() { Icon = "checked2x", Description = Result.Message, Title = "Recepcion Mercancia", Label = "Volver al Inicio" };



                    await Shell.Current.Navigation.PushModalAsync(new DialogAlert(new DialogAlertViewModel(dialogParam)));
                    Thread.Sleep(5000);

                    await Shell.Current.Navigation.PopToRootAsync();
                }
                else
                {

                    // ShowError(Result.MessagesFromErp[0].Message);
                    var dialogParam = new Dialog() { Icon = "cross2x", Description = Result.Message, Title = "Recepcion Mercancia", Label = "Volver al Inicio" };



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
            if (string.IsNullOrWhiteSpace(QtyUnit) || QtyUnit == "0")
            {
                await Application.Current.MainPage.DisplayAlert("Agregar Line", "El factor es requerido", "Aceptar");
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
                //ReceptionItems.Add(new TransportationOrderItem() { ProductBarCode = ProductNo, ProductId = Product.Product.Id, Qty = TotalQty, QtyOrd = TotalQty, Um = OrderItem.Um });
                _context.CreateTransactionLine(new TransactionLine { LineNo = line, OrderNo = Order.Data.OrderNo, ProductId = Product.Product.Id, ProductBarCode = ProductNo, ProductName = Product.Product.Name, Quantity = OrderItem.Qty, QtyRecibida = TotalQty, QtyPending = OrderItem.Qty - TotalQty, Um = OrderItem.Um, Bono = IsBonus });

            }
            else
            {
                var recItem = ReceptionItems.FirstOrDefault(xc => xc.ProductBarCode == ProductNo);
                var detItem = Details.FirstOrDefault(xc => xc.ProductBarCode == ProductNo);
                bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", $"El producto ya Existe {(IsBonus ? " con Bono" : "")} desea Sumar o Sustituir)?", "Sumar", "Sustituir");
                if (answer)
                {

                    if (recItem != null)
                    {
                        ReceptionItems.ToList().ForEach((i) => { if (i.ProductBarCode == ProductNo) { i.Qty += TotalQty; } });
                        //Details.ToList().ForEach((i) => { if (i.ProductBarCode == ProductNo) { i.QtyRecibida += TotalQty; i.QtyPending = i.Quantity - i.QtyRecibida; } });
                        _context.ExecuteSql($"UPDATE TransactionLine SET QtyRecibida = QtyRecibida +{TotalQty}, QtyPending = QtyPending-{TotalQty} Where OrderNo = '{OrderNo}' AND ProductBarCode = '{ProductNo}'");
                    }
                }
                else
                {

                    if (recItem != null)
                    {
                        recItem.Qty = TotalQty;
                        _context.ExecuteSql($"UPDATE TransactionLine SET QtyRecibida = {TotalQty}, QtyPending = {(!IsBonus ? OrderItem.Qty - TotalQty : 0)} Where OrderNo = '{OrderNo}' AND ProductBarCode = '{ProductNo}' ");
                    }
                }
            }
            ProductNo = "";
            Quantity = "";
            TotalQty = 0;
            QtyUnit = "";
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

                LoadItemsAsync();
                if (Details.Count > 0)
                {
                    bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", $"Esta Orden de Transaporte, ya fue iniciada Desea Reanudar o Limpiar  e iniciar desde Cero?", "Reanudar", "Limpiar");
                    if (!answer)
                    {
                        _context.DeleteTransationLineByOrderNo(OrderNo);
                        Details.Clear();
                        ReceptionItems.Clear();
                    }




                }
                Order = await repo.TranspOrderByOrderNo(OrderNo);
                if (Order.Data == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Trasanccion", Order.Message, "Aceptar");
                    return;
                }
                HasOrder = true;
                Items = Order.Data.Items.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        private void ResumenOpcion()
        {
            if (!HasOrder)
                return;

            ShowPanel("R");
            LoadItemsAsync();
        }

        private void DetalleOpcion()
        {

            ShowPanel("D");
        }

        private void ProductosOpcion()
        {
            if (!HasOrder)
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
                //case "D":
                //    General = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
                //    Productos = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
                //    Detalle = new PanelOption() { PanelVisible = true, BarColor = Color.FromRgba("#cc3300") };
                //    Resumen = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
                //    break;
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
