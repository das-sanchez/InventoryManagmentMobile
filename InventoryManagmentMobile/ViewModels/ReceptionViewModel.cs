using InventoryManagmentMobile.Database;
using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.Models.Dtos;
using InventoryManagmentMobile.Repositories;
using InventoryManagmentMobile.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static SQLite.SQLite3;

namespace InventoryManagmentMobile.ViewModels
{
    [QueryProperty(nameof(Type), nameof(Type))]
    public class ReceptionViewModel : BaseViewModel
    {
        public event Action FindProductRequested;
        public event Action FactorFocusRequested;
        public event Action QuantityFocusRequested;
        public event Action ProductFocusRequested;

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
        public Command PrintDiffCommand { get; }
        public Command SaveReceptionToolbarCommand { get; }
        public Command PrintDiffToolbarCommand { get; }



        ObservableCollection<DetailDto> _details;
        public ObservableCollection<DetailDto> Details { get { return _details; } set { SetProperty(ref _details, value); } }
        public List<OrderItem> Items { get; set; }
        public ObservableCollection<ReceptionItem> ReceptionItems { get; set; }
        public ObservableCollection<ItemSummary> ItemsSummary { get; set; }
        private readonly OleRepository repo;


        //string _itemTitle = string.Empty;
        //public string ItemTitle { get { return _itemTitle; } set { SetProperty(ref _itemTitle, value); } }

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

        private bool _switchBonusEnabled = false;
        public bool SwitchBonusEnabled { get { return _switchBonusEnabled; } set { SetProperty(ref _switchBonusEnabled, value); } }

        private bool _seachProductEnabled = true;
        public bool SeachProductEnabled { get { return _seachProductEnabled; } set { SetProperty(ref _seachProductEnabled, value); } }

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
        public bool CanSave 
        { 
            get { return _canSave; } 
            set { SetProperty(ref _canSave, value); } 
        }

        private bool _mustToPrintDiff;
        public bool MustToPrintDiff
        {
            get => _mustToPrintDiff;
            set
            {
                _mustToPrintDiff = value;
                OnPropertyChanged(nameof(MustToPrintDiff));
            }
        }

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
            PrintDiffCommand = new Command(() => PrintDiff());
            SaveReceptionToolbarCommand = new Command(() => SaveReceptionFromToolbar());
            OkCommand = new Command(() =>  OkReceptionAsync());
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
            CanSave = false;
           // ItemTitle = string.Empty;
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
                        ReceptionItems.Add(new ReceptionItem() { StoreId = line.StoreId, ProductBarCode = line.ProductBarCode, ProductId = line.ProductId, LineNo = line.LineNo, Qty = line.QtyRecibida, Um = line.Um, Bono = line.Bono, StorageId = line.StorageId });
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
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
                        ReceptionItems.Add(new ReceptionItem() { StoreId = line.StoreId, ProductBarCode = line.ProductBarCode, ProductId = line.ProductId, LineNo = line.LineNo, Qty = line.QtyRecibida, Um = line.Um, Bono = line.Bono, StorageId = line.StorageId });
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }
        private async void RemoveDetailItem(DetailDto dto)
        {
            try
            {
                if (dto.Bono)
                {
                    var bonusItem = _context.GetLine(Type, OrderNo, dto.ProductId, IsBonus: true);
                    var normalItem = _context.GetLine(Type, OrderNo, dto.ProductId, IsBonus: false);

                    if (bonusItem.QtyRecibida > 0 && normalItem != null && normalItem.QtyRecibida > 0)
                        throw new Exception("Para para reestablecer la cantidad de bono a cero primero debe hacerlo para la línea de la cantidad normal.");
                }

                bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", $"¿Esta seguro de reestabler la cantidad {(Type == "D" ? "despachada" : "recibida")} a cero?", "Si", "No");

                if (answer)
                {
                    _context.ExecuteSql($"UPDATE TransactionLine SET QtyRecibida = 0, QtyPending = Quantity Where TypeTrans='{Type}' AND OrderNo = '{OrderNo}' AND ProductId = '{dto.ProductId}' and  Bono = {dto.Bono}");
                    LoadItemsAsync();

                    if (Type != "P")
                        MustToPrintDiff = true;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }

        }

        private async void RemoveItem(ReceptionItem item)
        {
            try
            {
                if (item.Bono)
                {
                    var bonusItem = _context.GetLine(Type, OrderNo, item.ProductId, IsBonus: true);
                    var normalItem = _context.GetLine(Type, OrderNo, item.ProductId, IsBonus: false);
                    
                    if (bonusItem.QtyRecibida > 0 && normalItem != null && normalItem.QtyRecibida > 0)
                        throw new Exception("Para para reestabler la cantidad de bono a cero primero debe reestablecer la línea de la cantidad normal.");
                }
                
                bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", $"¿Esta seguro de reestabler la cantidad {(Type == "D" ? "despachada" :"recibida")} a cero?", "Si", "No");

                if (answer)
                {
                    _context.ExecuteSql($"UPDATE TransactionLine SET QtyRecibida = 0, QtyPending = Quantity Where TypeTrans='{Type}' AND OrderNo = '{OrderNo}' AND ProductId = '{ProductId}' and Bono = {item.Bono}");
                    LoadItemsAsync();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }  
        }

        private async void OkReceptionAsync()
        {
            ShowContent = true;
            ShowDialog = false;
            await Shell.Current.Navigation.PopToRootAsync();
        }

        private async void SaveReceptionFromToolbar()
        {
            if (CanSave)
                SaveReception();
        }

        private async void PrintDiff()
        {
            try
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Recepción", $"¿Esta seguro que desea imprimir el reporte de diferencia de {TypeDescription}?", "Si", "No");

                if (answer)
                {
                    Reception.Items = ReceptionItems.ToArray();
                    Reception.BillNo = InvoiceNumber;
                    Reception.OrderType = Type;
                    Reception.OrderNo = OrderNo;
                    IsBusy = true;
                    ShowContent = false;

                    var Result = new TransResult();

                    Result = await repo.PrintDiff(OrderNo, Reception);

                    IsBusy = false;
                    ShowContent = true;

                    if(Result == null)
                        throw new Exception("Se ha producido un error desconocido al tratar de imprimir el reporte de diferencias.");

                    if (Result.IsSuccess)
                    {
                        await Application.Current.MainPage.DisplayAlert("Impresión", Result.Message, "Aceptar");
                        MustToPrintDiff = false;
                    }
                    else
                    {
                        throw new Exception(Result.Message);
                    }
                }
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error Impresión", ex.Message, "Aceptar");
            }
                
        }
        private async void SaveReception()
        {
            try
            {
                if (string.IsNullOrEmpty(InvoiceNumber) && Type == "P")
                    throw new Exception("Debe digitar el numero de factura.");
                    
                if (ReceptionItems == null || (ReceptionItems != null && ReceptionItems.Count == 0))
                    throw new Exception("No has agregado detalle");
                    
                if (Order != null && Order.Data.Items.Length != ReceptionItems.Count())    
                    throw new Exception("La cantidad de líneas en la orden es diferente a las recibidas");

                if (!Details.Any(x => x.QtyRecibida > 0))
                    throw new Exception($"El documento no puede ser registrado ya que no se ha especificado la cantidad {(Type == "D" ? "despachada" : "recibida.")}");

                bool answer = await Application.Current.MainPage.DisplayAlert("Recepción", $"¿Esta seguro que desea guardar la recepción de {TypeDescription}?", "Si", "No");
               
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

                    IsBusy = false;
                    ShowContent = true;

                    if (Result.IsSuccess)
                    {
                        _context.DeleteTransationLineByOrderNo(Type, OrderNo);
                        var dialogParam = new Dialog() { Icon = "checked2x", Description = Result.Message, Title = "Recepcion Mercancia", Label = "Volver al Inicio" };
                        var dialogViewModel = new DialogAlertViewModel(dialogParam, goToBeginning: true);

                        await Shell.Current.Navigation.PushModalAsync(new DialogAlert(dialogViewModel));
                        await Task.Delay(5000);

                        if (!dialogViewModel.UserHasInteracted)
                            await Shell.Current.Navigation.PopToRootAsync();
                    }
                    else
                    {
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
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
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

        private bool HasBonus()
        {
            return _context.GetLine(Type, OrderNo, ProductId, IsBonus: true) != null;
        }

        private void TurnOffBonusSwith()
        {
            IsBonus = false;
        }

        private async void AddItem()
        {
            try
            {
                //ProductFocusRequested?.Invoke();
                if (!Product.IsSuccess)
                    throw new Exception(Product.Message);

                if (!Items.Any(xc => xc.ProductId.Trim().Equals(ProductId.Trim()) && xc.Bono == IsBonus))
                    throw new Exception($"Este Producto no {(IsBonus ? " Existe  en la Orden con Bono" : "Existe en la Orden")} ");

                OrderItem = Items.FirstOrDefault(xc => xc.ProductId.Trim().Equals(ProductId.Trim()) && xc.Bono == IsBonus);

                var un = MeasurementUnits.FirstOrDefault(xc => xc.BaseUm == OrderItem.Um);

                if (un == null)
                    throw new Exception($"Este Producto: {Product.Product.Name} no contiene un factor para la unidad de medida: {OrderItem.UmName}");

                if (!Product.Product.IsWeighed)
                {  
                    if (TotalQty % 1 != 0)
                    {
                        QuantityFocusRequested?.Invoke();
                        throw new Exception("Se permite cantidad fraccionada solo si trata de un producto pesado. Por favor, digite un número entero.");
                    }
                        
                    if ((string.IsNullOrWhiteSpace(QtyUnit) || QtyUnit == "0") || Factor == 0)
                    {
                        FactorFocusRequested?.Invoke();
                        throw new Exception("El factor es requerido");
                    }
                        
                    if ((decimal)Factor != Convert.ToDecimal(QtyUnit))
                    {
                        QtyUnit = "0";
                        FactorFocusRequested?.Invoke();
                        throw new Exception("El Factor digitado es diferente al de la unidad ordenada.");
                    }
                }
                else
                {
                    Factor = 1;
                    QtyUnit = "1";
                }

                if ((TotalQty) > OrderItem.Qty && OrderItem.Bono == IsBonus)
                {
                    QuantityFocusRequested?.Invoke();
                    throw new Exception($"La cantidad recibida {(IsBonus ? "en Bono " : "")} es mayor que la ordenada");
                }
                    
                if (!Items.Any(xc => xc.ProductId == ProductId && xc.Bono == IsBonus))
                {
                    QuantityFocusRequested?.Invoke();
                    throw new Exception($"La cantidad de bonos no puede ser registrada debido a que la orden no incluye bonos para el producto especificado");
                }
                    
                    
                if (Product == null)
                {
                    ProductFocusRequested?.Invoke();
                    throw new Exception("Debe buscar un producto.");
                }    
                    
                    
                if (TotalQty == 0)
                {
                    QuantityFocusRequested?.Invoke();
                    throw new Exception("Debe digitar la cantidad");
                }
                    

                if (Type =="P" && IsBonus && OrderItem.Qty != TotalQty)
                {
                    QuantityFocusRequested?.Invoke();
                    throw new Exception("La cantidad de bonos deber ser recibida en su totalidad.");
                }
                    
                var bonusItem = _context.GetLine(Type, OrderNo, ProductId, IsBonus: true);

                if(IsBonus && bonusItem == null)
                {
                    IsBonus = false;
                    throw new Exception("Este producto no contiene bono.");
                }
                    

                if (!IsBonus && bonusItem != null && bonusItem.Quantity != bonusItem.QtyRecibida)
                {
                    //throw new Exception("Este producto contiene bono, antes de registar la cantidad normal debe recibir los bonos en su totalidad.");
                    await Application.Current.MainPage.DisplayAlert("Error", "Este producto contiene bono, antes de registar la cantidad normal debe recibir los bonos en su totalidad.", "Aceptar");
                    IsBonus = true;
                    await ProductByNo(showBonusAlert:false);
                    QuantityFocusRequested?.Invoke();
                    return;
                }
                    

                int line = (ReceptionItems.Count == 0 ? 1 : ReceptionItems.Max(xc => xc.LineNo) + 1);
                string filter = (Type == "P" ? $" TypeTrans='{Type}' AND OrderNo = '{OrderNo}' AND ProductId = '{ProductId}' AND Bono = {(IsBonus ? 1 : 0)}" : $" OrderNo = '{OrderNo}' AND ProductId = '{ProductId}'");

                if (!_context.ValidExist(Type, OrderNo, ProductId, IsBonus))
                    _context.CreateTransactionLine(new TransactionLine { StoreId = OrderItem.StoreId, TypeTrans = Type, LineNo = OrderItem.LineNo, OrderNo = Order.Data.OrderNo, ProductId = Product.Product.Id, ProductBarCode = ProductNo, ProductName = Product.Product.Name, Quantity = (OrderItem.Bono != IsBonus ? TotalQty : OrderItem.Qty), QtyRecibida = TotalQty, QtyPending = (OrderItem.Bono != IsBonus ? TotalQty : OrderItem.Qty) - TotalQty, Um = OrderItem.Um, Bono = IsBonus, StorageId = OrderItem.StorageId, BarCodeScanned = false});

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
                                bool resp = await Application.Current.MainPage.DisplayAlert("Recepcion", $"El producto ya existe {(IsBonus ? " con Bono" : "")}, ¿Deseas Sustituir?", "Si", "No");

                                if (resp)
                                    _context.ExecuteSql($"UPDATE TransactionLine SET QtyRecibida = {TotalQty}, QtyPending = {(!IsBonus ? OrderItem.Qty - TotalQty : 0)} Where TypeTrans='{Type}' AND OrderNo = '{OrderNo}' AND ProductId = '{ProductId}' AND Bono = {IsBonus}");
                            }
                            else if (((TotalQty + qline.QtyRecibida) <= OrderItem.Qty && OrderItem.Bono == IsBonus))
                            {
                                bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", $"El producto ya existe {(IsBonus ? " con Bono" : "")}, ¿Deseas Sumar o Sustituir", "Sustituir", "Sumar");

                                if (answer)
                                    _context.ExecuteSql($"UPDATE TransactionLine SET QtyRecibida = {TotalQty}, QtyPending = {(!IsBonus ? OrderItem.Qty - TotalQty : 0)} Where TypeTrans='{Type}' AND OrderNo = '{OrderNo}' AND ProductId = '{ProductId}' AND Bono = {IsBonus}");
                                else
                                    _context.ExecuteSql($"UPDATE TransactionLine SET QtyRecibida = QtyRecibida +{TotalQty}, QtyPending = QtyPending-{TotalQty} Where TypeTrans='{Type}' AND OrderNo = '{OrderNo}' AND ProductId = '{ProductId}' AND Bono = {IsBonus}");
                            }
                        }
                    }
                }
                
                var normalItem = _context.GetLine(Type, OrderNo, ProductId, IsBonus: false);
                //bool executeFindProductRequested = Type == "P" && IsBonus && OrderItem.Qty == TotalQty && Items.Any(x => x.ProductId == ProductId && !x.Bono);
                bool executeFindProductRequested = Type == "P" && IsBonus && OrderItem.Qty == TotalQty && normalItem != null && normalItem.QtyRecibida == 0;

                if (!executeFindProductRequested)
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
                IsBonus = false;

                ProductFocusRequested?.Invoke();

                if (executeFindProductRequested)
                    FindProductRequested?.Invoke();
                
                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar"); 
            }
        }
        
        public async Task ProductByNo(bool showBonusAlert = true)
        {
            bool pExist = false;
            SwitchBonusEnabled = false;
            SeachProductEnabled = false;

            try
            {
                if (string.IsNullOrEmpty(ProductNo)) 
                    return;

                if (ProductNo.Length == 4)
                    ProductNo = "200" + ProductNo + "00000";

                Product = new ProductResult();
                Product = await repo.ProductByBarCode(ProductNo);

                if (Product == null)
                    throw new Exception("Se produjo un error al consultar el producto, no se obtuvo ninguna información.");

                if (!Product.IsSuccess)
                    throw new Exception(Product.Message);

                if (Product.Product == null)
                    throw new Exception("Producto no existe.");

                ProductId = Product.Product.Id;

                if (!Items.Any(xc => xc.ProductId == Product.Product.Id))
                    throw new Exception($"Producto no se encuentra en la orden");
                
                var productIds = Product.Product.MeasurementUnits.ToList();

                if (productIds.Count() > 0)
                {
                    productIds.ForEach((p) =>
                    {
                        if (!pExist)
                        {
                            if (Items.Any(xc => xc.ProductId == Product.Product.Id && xc.Um == p.BaseUm))
                                pExist = true;
                        }
                    });

                    if (!pExist)
                        throw new Exception($"Este producto no tiene definida la unidad de medida con la que se encuentra en el documento.");
                }

                //Tratamientos de bonos
                string bonusMessage = "";
                var bonusItem = _context.GetLine(Type, OrderNo, ProductId, IsBonus: true);
                var normalItem = _context.GetLine(Type, OrderNo, ProductId, IsBonus: false);

                if (IsBonus && bonusItem == null)
                {
                    IsBonus = false;
                    throw new Exception($"Este producto no contiene bonos.");
                }

                if (bonusItem != null && showBonusAlert) // Si tiene bonos
                {
                    if (bonusItem.Quantity != bonusItem.QtyRecibida)
                    {
                        bonusMessage = "Este producto contiene bonos. Favor recibir la cantidad total de bonos primero y luego la cantidad normal.";
                        IsBonus = true;
                    }
                    else if (normalItem == null || (normalItem != null && normalItem.QtyRecibida == 0))
                    {
                        bonusMessage = " Habiendo registado la cantidad total de bonos, ahora favor registrar la cantidad normal.";
                        IsBonus = false;
                    }

                    if (!String.IsNullOrEmpty(bonusMessage))
                        await Application.Current.MainPage.DisplayAlert("Aviso Bono", bonusMessage, "Aceptar");
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

                if (Product.Product == null)
                    Product = await repo.ProductByBarCode(ProductNo);

                MeasurementUnits = new ObservableCollection<MeasurementUnit>();
                Product.Product.MeasurementUnits.ToList().ForEach((un) => { MeasurementUnits.Add(un); });

                var un = MeasurementUnits.FirstOrDefault(xc => xc.BaseUm == OrderItem.Um);

                if (un == null && !Product.Product.IsWeighed)
                    throw new Exception($"Este Producto: {Product.Product.Name}  no contiene un factor para la unidad de medida:  {OrderItem.Um}");

                var tLine = _context.GetLine(Type, OrderNo, ProductId, IsBonus);

                if (!tLine.BarCodeScanned)
                    _context.ExecuteSql($"UPDATE TransactionLine SET BarCodeScanned = 1, ProductBarCode = '{ProductNo}'  Where TypeTrans='{Type}' AND OrderNo = '{OrderNo}' AND ProductId = '{ProductId}' ");


                Factor = (Product.Product.IsWeighed ? 1 : un.Factor);
                Unidad = $"Cantidad ({OrderItem.UmName})";
                IsLotRequired = OrderItem.IsLotNoRequired;
                NotEdition = false;
                InEdition = true;
                pExist = false;
                QuantityFocusRequested?.Invoke();
            }
            catch (Exception ex)
            {
                NotEdition = true;
                InEdition = false;
                SwitchBonusEnabled = true;
                SeachProductEnabled = true;
                ProductFocusRequested?.Invoke();
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }
            finally
            {
                SwitchBonusEnabled = true;
                SeachProductEnabled = true;
            }
        }

        private async Task OrderByIdAction()
        {
            bool hasClear = false;

            try
            {
                if (string.IsNullOrEmpty(OrderNo))
                    return;

                LoadItemsAsync();

                if (Details.Count() > 0 && Details.Any(x => x.QtyRecibida > 0))
                {
                    bool answer = await Application.Current.MainPage.DisplayAlert("Recepción", $"Esta Recepcion de {TypeDescription} ya fue iniciada. ¿Desea Reanudar o Limpiar?", "Reanudar", "Limpiar");

                    if (!answer)
                    {
                        _context.DeleteTransationLineByOrderNo(Type, OrderNo);
                        Details.Clear();
                        ReceptionItems.Clear();
                        hasClear = true;
                    }
                }
                else
                {
                    _context.DeleteTransationLineByOrderNo(Type, OrderNo);
                    hasClear = true;
                }

                Order = new OrderResult();

                if (Type == "T")
                    Order = await repo.InboundDeliveryByOrderNo(OrderNo);
                else
                    Order = await repo.OrderByOrderNo(OrderNo);

                if (Order == null)
                    throw new Exception("Se produjo un error al consultar el documento, no se obtuvo ninguna información.");

                if (!Order.IsSuccess)
                    throw new Exception(Order.Message);
                
                if (Order.Data == null)                    
                    throw new Exception("No se recibieron datos datos. Por favor, póngase en contacto con el administrador del sistema.");
                        
                if (Order.Data.OrderType != Type)
                    throw new Exception($"Esta orden  tiene un tipo diferente al que esta trabajando Tipo: {Type}" );

                if (Type == "D" || Type == "T")
                {
                    if (StoreNo != Order.Data.VendorId)
                        throw new Exception($"Esta orden no esta configurada para ser procesada en la tienda {StoreName}");
                }
                else if (Order.Data.Items.Any(xc => xc.StoreId != StoreNo))    
                    throw new Exception($"Esta orden no esta configurada para ser procesada en la tienda {StoreName}");

                OrderNo = Order.Data.OrderNo; //Aveces el numero de documento viende con ceros a la izquierda y no coincide con el numero de orden originalmente usado para la consulta.

                var plist = Order.Data.Items
                                  .Where(x => x.Bono == false)
                                  .GroupBy(x => new { x.ProductId })
                                  .Select(x => new
                                  {
                                      ProductId = x.Key.ProductId,

                                      Qty = x.Count(xc => xc.Bono)
                                  }).ToList();

                if (plist.Count() > 0 && plist.Any(xc => xc.Qty > 1))
                    throw new Exception($"Esta Orden tiene productos Repetidos");
                    
                var blist = Order.Data.Items
                                  .Where(x => x.Bono == true)
                                  .GroupBy(x => new { x.ProductId })
                                  .Select(x => new
                                  {
                                      ProductId = x.Key.ProductId,

                                      Qty = x.Count(xc => xc.Bono)
                                  }).ToList();

                if (blist.Count() > 0 && blist.Any(xc => xc.Qty > 1))
                    throw new Exception($"Esta orden contiene productos en Bono repetidos");

                HasOrder = true;
                Items = Order.Data.Items.ToList();

                if ((hasClear || ReceptionItems.Count() == 0) && Order.Data.Items.Count() > 0)
                {
                    Order.Data.Items.ToList().ForEach((l) =>
                    {
                        var line = new TransactionLine { StoreId = l.StoreId, TypeTrans = Type, LineNo = l.LineNo, OrderNo = Order.Data.OrderNo, ProductId = l.ProductId, ProductBarCode = l.ProductBarCode, ProductName = l.ProductName, Quantity = l.Qty, QtyRecibida = 0, QtyPending = 0, Um = l.Um, Bono = l.Bono, StorageId = l.StorageId, BarCodeScanned = false };
                        _context.SaveTransLine(line);
                    });   
                }
            }
            catch (Exception ex)
            {
                Order = new OrderResult();
                await Application.Current.MainPage.DisplayAlert("Trasanccion", ex.Message, "Aceptar");
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

                var diff = (from a in Order.Data.Items
                           join b in ReceptionItems
                           on new { a.ProductId, a.Um } equals new { b.ProductId, b.Um }
                           into groupjoin
                           from c in groupjoin.DefaultIfEmpty()
                           where a.Qty - (c.Qty != null ? c.Qty : 0) != 0
                           select c).ToList();
                
                if (diff.Count() > 0 && Type != "P")
                    MustToPrintDiff = true;
                
            }

            //ItemTitle = "Finalizar";
        }

        private void DetalleOpcion()
        {
            if (!HasOrder)
                return;
            ShowPanel("D");
            CanSave = false;
            MustToPrintDiff = false;
           // ItemTitle = string.Empty;
        }

        private void ProductosOpcion()
        {
            if (!HasOrder)
                return;
            ShowPanel("P");
            CanSave = false;
            MustToPrintDiff = false;
            //ItemTitle = string.Empty;
        }

        private void GeneralOpcion()
        {
            ShowPanel("G");
            CanSave = false;
            MustToPrintDiff = false;
            //ItemTitle = string.Empty;
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
