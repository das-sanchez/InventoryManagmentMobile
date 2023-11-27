

using InventoryManagmentMobile.Database;
using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.Models.Dtos;
using InventoryManagmentMobile.Repositories;
using InventoryManagmentMobile.Views;
using Microsoft.Maui.Graphics.Text;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.ViewModels
{
    [QueryProperty(nameof(Type), nameof(Type))]
    public class ReceptionViewModel : BaseViewModel
    {
        string _type = string.Empty;
        public string Type
        {
            get { return _type; }
            set
            {
                SetProperty(ref _type, value);
                ButtonTitle = "Recibir";
                if (value == "P")
                {
                    IsInvoiceRequired = true;
                    OrderLabel = "Orden de Compra";
                    Title = "Orden Compra";
                    TypeDescription = "la Orden de Compra";
                    ButtonTitle = "Recibir";
                    IsOPOrder = true;
                }
                else if (value == "T")
                {
                    OrderLabel = "No Transferencia";
                    OrderPlaceHolderLabel = "No Transferencia";
                    IsInvoiceRequired = false;
                    Title = "Transferencia";
                    TypeDescription = "la Orden de Transferencia";
                    ButtonTitle = "Recibir";
                }
                else
                {
                    OrderLabel = "No Envio";
                    OrderPlaceHolderLabel = "No Envio";
                    IsInvoiceRequired = false;
                    Title = "Envio";
                    TypeDescription = "el Envio de Mercancia";
                    ButtonTitle = "Enviar";
                }


            }
        }
        private readonly OleDataContext _context;
        public Command GeneralCommand { get; }
        public Command ProductosCommand { get; }
        public Command DetalleCommand { get; }
        public Command ResumenCommand { get; }

        public Command AddItemCommand { get; }
        public Command<ReceptionItem> RemoveItemCommand { get; }
        public Command<DetailDto> RemoveDetailItemCommand { get; }
        public Command AddResumenCommand { get; }
        public Command RemoveResumenCommand { get; }
        public Command SaveReceptionCommand { get; }


        ObservableCollection<DetailDto> _details;
        public ObservableCollection<DetailDto> Details { get { return _details; } set { SetProperty(ref _details, value); } }
        public List<OrderItem> Items { get; set; }
        public ObservableCollection<ReceptionItem> ReceptionItems { get; set; }
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
        string _typeDescription = string.Empty;
        public string TypeDescription { get { return _typeDescription; } set { SetProperty(ref _typeDescription, value); } }

        string _image = string.Empty;
        public string Image { get { return _image; } set { SetProperty(ref _image, value); } }

        private OrderResult _order;
        public OrderResult Order { get => _order; set { SetProperty(ref _order, value); } }


        private OrderItem _orderitem;
        public OrderItem OrderItem { get => _orderitem; set { SetProperty(ref _orderitem, value); } }

        private ReceptionItem _item;
        public ReceptionItem Item { get => _item; set { SetProperty(ref _item, value); } }

        private ReceptionItem _itemSelected;
        public ReceptionItem ItemSelected { get => _itemSelected; set { SetProperty(ref _itemSelected, value); } }

        private DetailDto _itemDetailSelected;
        public DetailDto ItemDetailSelected { get => _itemDetailSelected; set { SetProperty(ref _itemDetailSelected, value); } }

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
        int _factor;
        public int Factor
        {
            get { return _factor; }
            set { SetProperty(ref _factor, value); }
        }
        public ProductResult Product { get; set; }
        private bool _isBonus = false;
        public bool IsBonus { get { return _isBonus; } set { SetProperty(ref _isBonus, value); } }

        public DateTime ExpirationDate { get; set; }
        public ObservableCollection<MeasurementUnit> MeasurementUnits { get; set; }
        public MeasurementUnit MeasurementSelected { get; set; }

        public ReceptionHead Reception { get; set; }

        public bool IsLotRequired { get; set; }

        private bool _isInvoiceRequired;
        public bool IsInvoiceRequired { get { return _isInvoiceRequired; } set { SetProperty(ref _isInvoiceRequired, value); } }

        private bool _showDialog;
        public bool ShowDialog { get { return _showDialog; } set { SetProperty(ref _showDialog, value); } }

        private bool _showContent;
        public bool ShowContent { get { return _showContent; } set { SetProperty(ref _showContent, value); } }
        public Command OkCommand { get; }

        public bool IsVendeorOrder { get; set; } = false;
        string _buttonTitle = string.Empty;
        public string ButtonTitle { get { return _buttonTitle; } set { SetProperty(ref _buttonTitle, value); } }

        private bool _hasOrder = false;
        public bool HasOrder { get { return _hasOrder; } set { SetProperty(ref _hasOrder, value); } }
        public Command BackCommand { get; }

        string _storeNo = string.Empty;
        public string StoreNo { get { return _storeNo; } set { SetProperty(ref _storeNo, value); } }
        private bool _NotEdition = true;
        public bool NotEdition { get { return _NotEdition; } set { SetProperty(ref _NotEdition, value); } }

        private bool _InEdition = false;
        public bool InEdition { get { return _InEdition; } set { SetProperty(ref _InEdition, value); } }
        private bool _IsOPOrder = false;
        public bool IsOPOrder { get { return _IsOPOrder; } set { SetProperty(ref _IsOPOrder, value); } }
        string _lookupBarCode = "*";
        public string LookupBarCode { get { return _lookupBarCode; } set { SetProperty(ref _lookupBarCode, value); } }

        private bool _canSave = false;
        public bool CanSave { get { return _canSave; } set { SetProperty(ref _canSave, value); } }

        string _productId = string.Empty;
        public string ProductId { get { return _productId; } set { SetProperty(ref _productId, value); } }
        public ReceptionViewModel(OleRepository _repo, OleDataContext context)
        {
            repo = _repo;
            _context = context;
            Reception = new ReceptionHead();
            General = new PanelOption() { PanelVisible = true, BarColor = Color.FromRgba("#cc3300") };
            Productos = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
            Detalle = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
            Resumen = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };

            Items = new List<OrderItem>();

            Details = new ObservableCollection<DetailDto>();


            GeneralCommand = new Command(() => GeneralOpcion());
            ProductosCommand = new Command(() => ProductosOpcion());
            DetalleCommand = new Command(() => DetalleOpcion());
            ResumenCommand = new Command(() => ResumenOpcion());
            OrderByIdCommand = new Command(async () => await OrderByIdAction());
            ProductByNoCommand = new Command(async () => await ProductByNo());
            AddItemCommand = new Command(() => AddItem());
            RemoveItemCommand = new Command<ReceptionItem>(RemoveItem);
            RemoveDetailItemCommand = new Command<DetailDto>(RemoveDetailItem);
            AddResumenCommand = new Command(() => AddItemSummary());
            RemoveResumenCommand = new Command(() => RemoveItemSummary());
            SaveReceptionCommand = new Command(() => SaveReception());
            OkCommand = new Command(() => OkReceptionAsync());
            OrderItem = new OrderItem();
            ItemsSummary = new ObservableCollection<ItemSummary>();
            ReceptionItems = new ObservableCollection<ReceptionItem>();
            Item = new ReceptionItem();
            ItemSelected = new ReceptionItem();
            ItemSummrySelected = new ItemSummary();
            MeasurementUnits = new ObservableCollection<MeasurementUnit>();
            StoreName = Preferences.Get("storeName", "Default Value");
            StoreNo = Preferences.Get("storeNo", "Default Value");

            ExpirationDate = DateTime.Now;
            ShowContent = true;
            BackCommand = new Command(() => BackSync());
            //  var list = _context.GetTransactionLines().ToList();
            CanSave = false;

        }

        private async void BackSync()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", "Esta seguro de Salir?", "Si", "No");
            if (answer)
            {
                await Shell.Current.Navigation.PopAsync();
            }
        }

        public async void LoadItemsAsync()
        {
            try
            {
                var items = new List<TransactionLine>();
                Details.Clear();
                ReceptionItems.Clear();
                items = _context.GetTransactionLinesByOrderNo(Type, OrderNo);
                if (items is not null && items.Any())
                {

                    foreach (var line in items)
                    {
                        Details.Add(new DetailDto() { ProductBarCode = line.ProductBarCode, ProductId = line.ProductId, ProductName = line.ProductName, Quantity = line.Quantity, QtyRecibida = line.QtyRecibida, QtyPending = line.QtyPending, Stock = 0, Um = line.Um, Bono = line.Bono, Color = (line.Bono ? "#bdebca" : "#fffffff") });
                        ReceptionItems.Add(new ReceptionItem() { ProductBarCode = line.ProductBarCode, ProductId = line.ProductId, LineNo = line.LineNo, Qty = line.QtyRecibida, Um = line.Um, Bono = line.Bono });
                    }
                }
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Errorn", ex.Message, "Aceptar");
            }


        }
        public async void FindByProduct(string barCode)
        {
            try
            {
                var items = new List<TransactionLine>();
                Details.Clear();
                ReceptionItems.Clear();
                items = _context.GetTransactionLinesByOrderNo(Type, OrderNo, barCode);
                if (items is not null && items.Any())
                {

                    foreach (var line in items)
                    {
                        Details.Add(new DetailDto() { ProductBarCode = line.ProductBarCode, ProductId = line.ProductId, ProductName = line.ProductName, Quantity = line.Quantity, QtyRecibida = line.QtyRecibida, QtyPending = line.QtyPending, Stock = 0, Um = line.Um, Bono = line.Bono, Color = (line.Bono ? "#bdebca" : "#fffffff") });
                        ReceptionItems.Add(new ReceptionItem() { ProductBarCode = line.ProductBarCode, ProductId = line.ProductId, LineNo = line.LineNo, Qty = line.QtyRecibida, Um = line.Um, Bono = line.Bono });
                    }
                }
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Errorn", ex.Message, "Aceptar");
            }


        }
        private async void RemoveDetailItem(DetailDto dto)
        {
            try
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", "Esta seguro de borrar?", "Si", "No");
                if (answer)
                {
                    var item = ReceptionItems.FirstOrDefault(x => x.ProductId == dto.ProductId && x.Bono == dto.Bono);
                    ReceptionItems.Remove(item);
                    //Details.Remove(dto);
                    _context.ExecuteSql($"DELETE FROM TransactionLine Where OrderNo = '{OrderNo}' AND ProductId = '{dto.ProductId}' AND Bono ={dto.Bono}");
                    LoadItemsAsync();
                }
            }
            catch (Exception ex)
            {


                await Application.Current.MainPage.DisplayAlert("Errorn", ex.Message, "Aceptar");
            }

        }

        private async void RemoveItem(ReceptionItem item)
        {

            bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", "Esta seguro de borrar?", "Si", "No");
            if (answer)
            {
                var det = Details.FirstOrDefault(x => x.ProductId == item.ProductId);
                ReceptionItems.Remove(item);
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
            try
            {
                if (string.IsNullOrEmpty(InvoiceNumber) && Type == "P")
                {
                    await Application.Current.MainPage.DisplayAlert("Recepcion", "Debe Digitar el Numero de Factura", "Aceptar");
                    return;
                }
                if (ReceptionItems == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Guardar Recepcion", "No a Agregado detalle", "Aceptar");
                    return;
                }
                if (Order != null)
                {
                    if (Order.Data.Items.Length != ReceptionItems.Count())
                    {
                        await Application.Current.MainPage.DisplayAlert("Guardar Recepcion", "La Cantidad de Lineas en la Orden es Diferente a la Recibidas", "Aceptar");
                        return;
                    }
                }

                bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", $"Desea guardar la Recepcion de {TypeDescription}?", "Yes", "No");
                if (answer)
                {

                    Reception.Items = ReceptionItems.ToArray();
                    Reception.BillNo = InvoiceNumber;
                    Reception.OrderType = Type;
                    Reception.OrderNo = OrderNo;
                    IsBusy = true;
                    ShowContent = false;
                    var Result = new TransResult();

                    Result = await repo.SaveReception(OrderNo, Reception);


                    Thread.Sleep(5000);
                    IsBusy = false;
                    ShowContent = true;
                    if (Result.Data != null && Result.IsSuccess)
                    {
                        _context.DeleteTransationLineByOrderNo(Type, OrderNo);
                        //ShowSucces("Transaccion Procesada Correctamente");
                        var dialogParam = new Dialog() { Icon = "checked2x", Description = Result.Message, Title = "Recepcion Mercancia", Label = "Volver al Inicio" };


                        await Shell.Current.Navigation.PushModalAsync(new DialogAlert(new DialogAlertViewModel(dialogParam)));

                        Thread.Sleep(5000);
                        await Shell.Current.Navigation.PopToRootAsync();

                    }
                    else
                    {

                        //ShowError(Result.Message);
                        var dialogParam = new Dialog() { Icon = "cross2x", Description = Result.Message, Title = "Recepcion Mercancia", Label = "Volver al Inicio" };



                        await Shell.Current.Navigation.PushModalAsync(new DialogAlert(new DialogAlertViewModel(dialogParam)));

                    }

                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Errorn", ex.Message, "Aceptar");
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
            try
            {
                if (Items.Any(xc => xc.ProductId.Trim().Equals(ProductId.Trim()) && xc.Bono == IsBonus))
                {
                    OrderItem = Items.FirstOrDefault(xc => xc.ProductId.Trim().Equals(ProductId.Trim()) && xc.Bono == IsBonus);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Agregar Line", $"Este Producto no {(IsBonus ? " Existe  en la Orden con Bono" : "Existe en la Orden")} ", "Aceptar");
                    return;
                }


                var un = MeasurementUnits.FirstOrDefault(xc => xc.BaseUm == OrderItem.Um);
                if (un == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Agregar Line", $"Este Producto: {Product.Product.Name}  no contiene un factor para la unidad de medida:  {OrderItem.Um}", "Aceptar");
                    return;
                }
                if (!Product.Product.IsWeighed)
                {

                    if (string.IsNullOrWhiteSpace(QtyUnit) || QtyUnit == "0")
                    {
                        await Application.Current.MainPage.DisplayAlert("Agregar Line", "El factor es requerido", "Aceptar");
                        return;
                    }
                    if (Factor == 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Agregar Line", "El factor es requerido", "Aceptar");
                        return;
                    }

                    if (Factor != Convert.ToInt32(QtyUnit))
                    {

                        await Application.Current.MainPage.DisplayAlert("Recepcion", "El Factor digitado es diferente al de la unidad ordenada.", "Aceptar");

                        QtyUnit = "0";

                        return;
                    }
                }
                else
                {
                    Factor = 1;
                    QtyUnit = "1";
                }


                if ((TotalQty) > OrderItem.Qty && OrderItem.Bono == IsBonus)
                {
                    await Application.Current.MainPage.DisplayAlert("Agregar Line", $"La Recibida {(IsBonus ? "En Bono " : "")} es Mayor que la Ordenada", "Aceptar");
                    return;
                }
                if (!Items.Any(xc => xc.ProductId == ProductId && xc.Bono == IsBonus))
                {
                    await Application.Current.MainPage.DisplayAlert("Agregar Line", $"La cantidad de bonos no puede ser registrada debido a que la orden no incluye bonos para el producto especificado", "Aceptar");
                    return;
                }

                if (Product == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Agregar Line", "Debe buscar un producto", "Aceptar");
                    return;
                }

                if (TotalQty == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Agregar Line", "Debe digitar la cantidad", "Aceptar");
                    return;
                }

                int line = (ReceptionItems.Count == 0 ? 1 : ReceptionItems.Max(xc => xc.LineNo) + 1);

                string filter = (Type == "P" ? $" TypeTrans='{Type}' AND OrderNo = '{OrderNo}' AND ProductId = '{ProductId}' AND Bono = {(IsBonus ? 1 : 0)}" : $" OrderNo = '{OrderNo}' AND ProductId = '{ProductId}'");

                //exist = _context.ValidExist(filter);
                if (!_context.ValidExist(Type, OrderNo, ProductId, IsBonus))
                {

                    //Details.Add(new DetailDto() { ProductBarCode = ProductNo, ProductId = Product.Product.Id, ProductName = OrderItem.ProductName, QtyPending = OrderItem.Qty - TotalQty, Quantity = OrderItem.Qty, QtyRecibida = TotalQty, Stock = 0 });
                    // _context.DeleteTransationLineByOrderNo(OrderNo);
                    _context.CreateTransactionLine(new TransactionLine { TypeTrans = Type, LineNo = OrderItem.LineNo, OrderNo = Order.Data.OrderNo, ProductId = Product.Product.Id, ProductBarCode = ProductNo, ProductName = Product.Product.Name, Quantity = (OrderItem.Bono != IsBonus ? TotalQty : OrderItem.Qty), QtyRecibida = TotalQty, QtyPending = (OrderItem.Bono != IsBonus ? TotalQty : OrderItem.Qty) - TotalQty, Um = OrderItem.Um, Bono = IsBonus });

                    //_context.AddItemAsync<TransactionLine>();
                }
                else
                {
                    var qline = _context.GetLine(Type, OrderNo, ProductId, IsBonus);

                    if (qline != null)
                    {
                        if (qline.QtyRecibida == 0)
                        {
                            _context.ExecuteSql($"UPDATE TransactionLine SET QtyRecibida = {TotalQty}, QtyPending = {(!IsBonus ? OrderItem.Qty - TotalQty : 0)} Where TypeTrans='{Type}' AND OrderNo = '{OrderNo}' AND ProductId = '{ProductId}' AND Bono = {IsBonus}");
                        }
                        else
                        {
                            if ((TotalQty + qline.QtyRecibida) > OrderItem.Qty && OrderItem.Bono == IsBonus)
                            {
                                bool resp = await Application.Current.MainPage.DisplayAlert("Recepcion", $"El producto ya Existe {(IsBonus ? " con Bono" : "")} desea  Sustituir)?", "Si", "no");
                                if (resp)
                                {


                                    //Details.ToList().ForEach((i) => { if (i.ProductBarCode == ProductNo) { i.QtyRecibida += TotalQty; i.QtyPending = i.Quantity - i.QtyRecibida; } });
                                    _context.ExecuteSql($"UPDATE TransactionLine SET QtyRecibida = {TotalQty}, QtyPending = {(!IsBonus ? OrderItem.Qty - TotalQty : 0)} Where TypeTrans='{Type}' AND OrderNo = '{OrderNo}' AND ProductId = '{ProductId}' AND Bono = {IsBonus}");

                                }
                            }
                            else if (((TotalQty + qline.QtyRecibida) <= OrderItem.Qty && OrderItem.Bono == IsBonus))
                            {
                                bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", $"El producto ya Existe {(IsBonus ? " con Bono" : "")} desea Sumar o Sustituir)?", "Sustituir", "Sumar");
                                if (answer)
                                {
                                    //Details.ToList().ForEach((i) => { if (i.ProductBarCode == ProductNo) { i.QtyRecibida += TotalQty; i.QtyPending = i.Quantity - i.QtyRecibida; } });

                                    _context.ExecuteSql($"UPDATE TransactionLine SET QtyRecibida = {TotalQty}, QtyPending = {(!IsBonus ? OrderItem.Qty - TotalQty : 0)} Where TypeTrans='{Type}' AND OrderNo = '{OrderNo}' AND ProductId = '{ProductId}' AND Bono = {IsBonus}");
                                }
                                else
                                {
                                    _context.ExecuteSql($"UPDATE TransactionLine SET QtyRecibida = QtyRecibida +{TotalQty}, QtyPending = QtyPending-{TotalQty} Where TypeTrans='{Type}' AND OrderNo = '{OrderNo}' AND ProductId = '{ProductId}' AND Bono = {IsBonus}");
                                }
                            }
                        }
                    }


                }
                ProductNo = "";
                ProductId = "";
                Quantity = "";
                TotalQty = 0;
                QtyUnit = string.Empty;
                Factor = 0;
                Unidad = "";
                Product = new ProductResult();
                OrderItem = new OrderItem();
                NotEdition = true;
                InEdition = false;
            }
            catch (Exception ex)
            {


                await Application.Current.MainPage.DisplayAlert("Errorn", ex.Message, "Aceptar"); ;
            }


        }

        public async Task ProductByNo()
        {
            bool pExist = false;
            try
            {
                //OrderItem = new OrderItem();
                if (string.IsNullOrEmpty(ProductNo)) return;



                Product = new ProductResult();

                Product = await repo.ProductByBarCode(ProductNo);
                if (!Product.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Producdto", $"Producto no existe", "Aceptar");
                    return;
                }


                ProductId = Product.Product.Id;
                var productIds = Product.Product.MeasurementUnits.ToList();
                if (productIds.Count() > 0)
                {

                    productIds.ForEach((p) =>
                    {
                        if (!pExist)
                        {
                            if (Items.Any(xc => xc.ProductId == Product.Product.Id && xc.Um == p.BaseUm))
                            {
                                ProductNo = p.BarCode;

                                pExist = true;
                            }
                        }

                    });


                }

                OrderItem = Items.FirstOrDefault(xc => xc.ProductId.Trim().Equals(ProductId.Trim()) && xc.Bono == IsBonus);
                if (OrderItem == null)
                {
                    if (Type == "P")
                        TypeDescription = "la Orden de Compra";
                    if (Type == "T")
                        TypeDescription = "la Orden de Transferencia";
                    if (Type == "D")
                        TypeDescription = "el Envio de Mercancia";

                    await Application.Current.MainPage.DisplayAlert("Recepcion", $"Producto no esta en {TypeDescription}", "Aceptar");
                    ProductNo = "";
                    ProductId = "";
                    Quantity = "";
                    TotalQty = 0;
                    QtyUnit = string.Empty;
                    Factor = 0;
                    NotEdition = true;
                    InEdition = false;
                    return;
                }

                //Thread.Sleep(3000);
                if (Product.Product == null)
                {
                    Product = await repo.ProductByBarCode(ProductNo);

                }
                MeasurementUnits = new ObservableCollection<MeasurementUnit>();
                Product.Product.MeasurementUnits.ToList().ForEach((un) => { MeasurementUnits.Add(un); });
                var un = MeasurementUnits.FirstOrDefault(xc => xc.BaseUm == OrderItem.Um);
                if (un == null && !Product.Product.IsWeighed)
                {
                    throw new Exception($"Este Producto: {Product.Product.Name}  no contiene un factor para la unidad de medida:  {OrderItem.Um}");
                }
                Factor = (Product.Product.IsWeighed ? 1 : un.Factor);
                Unidad = $"Cantidad ({OrderItem.Um})";
                IsLotRequired = OrderItem.IsLotNoRequired;
                NotEdition = false;
                InEdition = true;
                pExist = false;
            }
            catch (Exception ex)
            {
                NotEdition = true;
                InEdition = false;
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        private async Task OrderByIdAction()
        {
            bool hasClear = false;
            try
            {
                LoadItemsAsync();
                if (Details.Count() > 0)
                {
                    bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", $"Esta Recepcion de {TypeDescription} ya fue iniciada Desea Reanudar o Limpiar  e iniciar desde Cero?", "Reanudar", "Limpiar");
                    if (!answer)
                    {
                        _context.DeleteTransationLineByOrderNo(Type, OrderNo);
                        Details.Clear();
                        ReceptionItems.Clear();
                        hasClear = true;
                    }




                }

                Order = new OrderResult();
                if (Type == "T")
                {
                    Order = await repo.InboundDeliveryByOrderNo(OrderNo);
                }
                else
                {
                    Order = await repo.OrderByOrderNo(OrderNo);
                }
                if (Order == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Trasanccion", "Transaccion no Encontrada", "Aceptar");
                    return;
                }
                else
                {
                    if (Order.Data == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Trasanccion", Order.Message, "Aceptar");
                        return;
                    }
                    if (Order.Data.OrderType != Type)
                    {
                        Order = new OrderResult();
                        await Application.Current.MainPage.DisplayAlert("Trasanccion", "Esta orden  tiene un tipo diferente al que esta trabajando Tipo: " + Type, "Aceptar");

                        return;
                    }
                    if (Order.Data.Items.Any(xc => xc.StoreId != StoreNo))
                    {
                        Order = new OrderResult();
                        await Application.Current.MainPage.DisplayAlert("Trasanccion", "Esta orden no pertenece a este Store: " + StoreNo, "Aceptar");
                        return;
                    }
                }



                var plist = Order.Data.Items
                                  .Where(x => x.Bono == false)
                                  .GroupBy(x => new { x.ProductId })
                                  .Select(x => new
                                  {
                                      ProductId = x.Key.ProductId,

                                      Qty = x.Count(xc => xc.Bono)
                                  }).ToList();

                if (plist.Count() > 0)
                {
                    if (plist.Any(xc => xc.Qty > 1))
                    {
                        Order = new OrderResult();
                        await Application.Current.MainPage.DisplayAlert("Trasanccion", "Esta Orden tiene productos Repetidos", "Aceptar");
                        return;
                    }
                }
                var blist = Order.Data.Items
                                  .Where(x => x.Bono == true)
                                  .GroupBy(x => new { x.ProductId })
                                  .Select(x => new
                                  {
                                      ProductId = x.Key.ProductId,

                                      Qty = x.Count(xc => xc.Bono)
                                  }).ToList();

                if (blist.Count() > 0)
                {
                    if (blist.Any(xc => xc.Qty > 1))
                    {
                        Order = new OrderResult();
                        await Application.Current.MainPage.DisplayAlert("Trasanccion", "Esta orden contiene productos en Bono repetidos", "Aceptar");
                        return;
                    }
                }
                HasOrder = true;
                Items = Order.Data.Items.ToList();
                if (hasClear || ReceptionItems.Count() == 0)
                {
                    if (Order.Data.Items.Count() > 0)
                    {
                        Order.Data.Items.ToList().ForEach((l) =>
                        {
                            var line = new TransactionLine { TypeTrans = Type, LineNo = l.LineNo, OrderNo = Order.Data.OrderNo, ProductId = l.ProductId, ProductBarCode = l.ProductBarCode, ProductName = l.ProductName, Quantity = l.Qty, QtyRecibida = 0, QtyPending = 0, Um = l.Um, Bono = l.Bono };
                            _context.SaveTransLine(line);
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public Command OrderByIdCommand { get; }
        public Command ProductByNoCommand { get; }

        private void ResumenOpcion()
        {
            if (!HasOrder)
                return;

            ShowPanel("R");
            LoadItemsAsync();
            if (ReceptionItems.Count() > 0)
            {
                CanSave = true;
            }
        }

        private void DetalleOpcion()
        {
            if (!HasOrder)
                return;
            ShowPanel("D");
            CanSave = false;
        }

        private void ProductosOpcion()
        {
            if (!HasOrder)
                return;
            ShowPanel("P");
            CanSave = false;
        }

        private void GeneralOpcion()
        {
            ShowPanel("G");
            CanSave = false;
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
