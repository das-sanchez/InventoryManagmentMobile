using InventoryManagmentMobile.Database;
using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.Models.Dtos;
using InventoryManagmentMobile.Repositories;
using InventoryManagmentMobile.Views;
//using MetalPerformanceShadersGraph;
using System.Collections.ObjectModel;

namespace InventoryManagmentMobile.ViewModels
{
    public class ReceptionCDViewModel : BaseViewModel
    {
        public event Action FindProductRequested;
        public event Action FactorFocusRequested;
        public event Action QuantityFocusRequested;
        public event Action ProductFocusRequested;
        public event Action ManageToolbarItemVisibilityRequested;

        public Command GeneralCommand { get; }
        public Command ProductosCommand { get; }
        public Command DetalleCommand { get; }
        public Command ResumenCommand { get; }

        public Command AddItemCommand { get; }
        public Command<TransportationOrderItem> RemoveItemCommand { get; }
        public Command AddResumenCommand { get; }
        public Command RemoveResumenCommand { get; }
        public Command SaveReceptionCommand { get; }
        public Command PrintDiffCommand { get; }
        public Command SaveReceptionToolbarCommand { get; }
        

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

        string _itemTitle = string.Empty;
        public string ItemTitle { get { return _itemTitle; } set { SetProperty(ref _itemTitle, value); } }

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

        public string TypeTrans { get; set; } = "OT";
        private bool _NotEdition = true;
        public bool NotEdition { get { return _NotEdition; } set { SetProperty(ref _NotEdition, value); } }

        private bool _InEdition = false;
        public bool InEdition { get { return _InEdition; } set { SetProperty(ref _InEdition, value); } }

        private bool _canSave = false;
        public bool CanSave 
        { 
            get { return _canSave; } 
            set 
            { 
                SetProperty(ref _canSave, value);
            } 
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

        string _lookupBarCode = "*";
        public string LookupBarCode { get { return _lookupBarCode; } set { SetProperty(ref _lookupBarCode, value); } }

        string _productId = string.Empty;
        public string ProductId { get { return _productId; } set { SetProperty(ref _productId, value); } }

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
            ProductByNoCommand = new Command(async() => await ProductByNo());
            AddItemCommand = new Command(async () => await AddItem());
            RemoveItemCommand = new Command<TransportationOrderItem>(RemoveItem);
            AddResumenCommand = new Command(() => AddItemSummary());
            RemoveResumenCommand = new Command(() => RemoveItemSummary());
            SaveReceptionCommand = new Command(async() => await SaveReception());
            PrintDiffCommand = new Command(() => PrintDiff());
            SaveReceptionToolbarCommand = new Command(async () => await SaveReceptionFromToolbar());
            OkCommand = new Command(async () => await  OkReceptionAsync());
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
            CanSave = false;
        }

        private async void RemoveDetailItem(DetailDto dto)
        {
            try
            {
                var itemFromDb = _context.GetLine(TypeTrans, OrderNo, dto.ProductId, IsBonus: IsBonus);
                bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", $"¿Esta seguro de reestabler la cantidad recibida a cero?", "Si", "No");

                if (answer)
                {
                    if (itemFromDb.ProductInErpOrder)
                    {
                        _context.ExecuteSql($"UPDATE TransactionLine SET QtyRecibida = 0, QtyPending = Quantity Where TypeTrans='{TypeTrans}' AND OrderNo = '{OrderNo.Trim()}' AND ProductId = '{dto.ProductId}' ");
                        LoadItemsAsync();
                        MustToPrintDiff = true;
                    }
                    else
                    {
                        _context.ExecuteSql($"delete from TransactionLine Where TypeTrans='{TypeTrans}' AND OrderNo = '{OrderNo}' AND ProductId = '{dto.ProductId}' and  Bono = {dto.Bono}");
                        var itemRemoved = Items.FirstOrDefault(x => x.ProductId == itemFromDb.ProductId && x.Um == itemFromDb.Um);
                        Items.Remove(itemRemoved);
                        LoadItemsAsync();

                        MustToPrintDiff = IsThereDifferences();
                        ManageToolbarItemVisibilityRequested?.Invoke();
                    } 
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }

        }

        private async void BackSync()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Recepción", "¿Esta seguro de salir?", "Si", "No");

            if (answer)
                await Shell.Current.Navigation.PopAsync();
            
        }

        public async void FindByProduct(string barCode)
        {
            try
            {
                var items = new List<TransactionLine>();
                Details.Clear();
                ReceptionItems.Clear();
                items = _context.GetTransactionLinesByOrderNo(TypeTrans, OrderNo, barCode);

                if (items is not null && items.Any() && items.Any(x => x.QtyRecibida > 0))
                {
                    foreach (var line in items)
                    {
                        Details.Add(new DetailDto() { ProductBarCode = line.ProductBarCode, ProductId = line.ProductId, ProductName = line.ProductName, Quantity = line.Quantity, QtyRecibida = line.QtyRecibida, QtyPending = line.QtyPending, Stock = 0, Um = line.Um, Bono = line.Bono, Color = (line.Bono ? "#bdebca" : "#fffffff"), StoreId = line.StoreId });
                        ReceptionItems.Add(new TransportationOrderItem() { ProductBarCode = line.ProductBarCode, ProductId = line.ProductId, LineNo = line.LineNo, Qty = line.QtyRecibida, Um = line.Um, QtyOrd = line.Quantity, StoreId = line.StoreId, StorageId = line.StorageId });
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        public void LoadItemsAsync()
        {
            var items = new List<TransactionLine>();
            Details.Clear();
            ReceptionItems.Clear();
            items = _context.GetTransactionLinesByOrderNo(TypeTrans, OrderNo).OrderBy(x => x.LineNo).ToList();
            if (items is not null && items.Any())
            {
                foreach (var line in items)
                {
                    Details.Add(new DetailDto() { ProductBarCode = line.ProductBarCode, ProductId = line.ProductId, ProductName = line.ProductName, Quantity = line.Quantity, QtyRecibida = line.QtyRecibida, QtyPending = line.QtyPending, Stock = 0, Um = line.Um, Bono = line.Bono, Color = (line.Bono ? "#bdebca" : "#fffffff"), StoreId = line.StoreId });
                    ReceptionItems.Add(new TransportationOrderItem() { ProductBarCode = line.ProductBarCode, ProductId = line.ProductId, LineNo = line.LineNo, Qty = line.QtyRecibida, Um = line.Um, QtyOrd = line.Quantity, StoreId = line.StoreId, StorageId = line.StorageId });
                }
            }

        }
        private async void RemoveItem(TransportationOrderItem item)
        {
            try
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", "¿Esta seguro de borrar?", "Si", "No");

                if (answer)
                {
                    var det = Details.FirstOrDefault(x => x.ProductId == item.ProductId);
                    ReceptionItems.Remove(ItemSelected);
                    Details.Remove(det);
                }
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }   
        }

        private async Task OkReceptionAsync()
        {
            ShowContent = true;
            ShowDialog = false;
            await Shell.Current.Navigation.PopToRootAsync();
        }

        private async Task SaveReceptionFromToolbar()
        {
            if (CanSave)
                await SaveReception();
        }

        private async void PrintDiff()
        {
            try
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Recepción", $"¿Esta seguro que desea imprimir el reporte de diferencia?", "Si", "No");

                if (answer)
                {
                    Reception.Items = ReceptionItems.ToArray();
                    Reception.OrderNo = OrderNo;
                    Reception.VendorId = Order.Data.VendorId;
                    IsBusy = true;
                    ShowContent = false;

                    var Result = new TransResult();

                    Result = await repo.PrintCDDiff(Reception);

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
        private async Task SaveReception()
        {
            try
            {
                if (ReceptionItems == null || (ReceptionItems != null && ReceptionItems.Count == 0))
                    throw new Exception("No se ha agregado ningun producto.");
                    
                if (!ReceptionItems.Any(xc => xc.Qty > 0))
                    throw new Exception("No se han recibido productos de la orden.");

                //if (Order != null && Order.Data.Items.Length > ReceptionItems.Count())
                //    throw new Exception("La cantidad de lineas en la orden es diferente a las recibidas.");
                    
                bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", "Desea guardar la recepción del documento de transporte?", "Si", "No");
               
                if (answer)
                {
                    Reception.Items = ReceptionItems.ToArray();
                    Reception.VendorId = Order.Data.VendorId;

                    var Result = await repo.SaveTransporationOrder(OrderNo, Reception);

                    if (Result.IsSuccess)
                    {
                        IsBusy = true;
                        ShowContent = false;

                        _context.DeleteTransationLineByOrderNo(TypeTrans, OrderNo);

                        IsBusy = false;
                        ShowContent = true;

                        var dialogParam = new Dialog() { Icon = "checked2x", Description = Result.Message, Title = "Recepcion Mercancia", Label = "Volver al Inicio" };
                        var dialogAlertViewModel = new DialogAlertViewModel(dialogParam, goToBeginning: true);

                        await Shell.Current.Navigation.PushModalAsync(new DialogAlert(dialogAlertViewModel));
                        await Task.Delay(5000);

                        if (dialogAlertViewModel.UserHasInteracted)
                            await Shell.Current.Navigation.PopToRootAsync();
                    }
                    else
                    {
                        var dialogParam = new Dialog() { Icon = "cross2x", Description = Result.Message, Title = "Recepcion Mercancia", Label = "Volver al Inicio" };
                        await Shell.Current.Navigation.PushModalAsync(new DialogAlert(new DialogAlertViewModel(dialogParam)));
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Guardar Recepción", ex.Message, "Aceptar");
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

        private async Task AddItem()
        {
            try
            {
                ProductFocusRequested?.Invoke();

                if (!Items.Any(xc => xc.ProductId.Trim().Equals(ProductId.Trim())))
                {
                    var unidadMedida = MeasurementUnits.FirstOrDefault(xc => xc.Factor == Convert.ToDecimal(QtyUnit));

                    if (unidadMedida == null)
                        throw new Exception($"Este Producto no contiene una unidad de medida para el factor digitado.");

                    int nextLine = (ReceptionItems.Count == 0 ? 999 : ReceptionItems.Max(xc => xc.LineNo) + 1);
                    var transactionLine = _context.GetTransactionLinesByOrderNo(TypeTrans, OrderNo).FirstOrDefault();
                    string storageId = transactionLine.StorageId;
                    string storeId = transactionLine.StoreId;

                    if (!_context.ValidExist(TypeTrans, OrderNo, ProductId, IsBonus))
                    {
                        Items.Add(new TranspOrderItem
                        {
                            StoreId = storeId,
                            StorageId = storageId,
                            ProductId = Product.Product.Id,
                            ProductBarCode = ProductNo,
                            ProductName = Product.Product.Name,
                            Qty = 0,
                            Um = unidadMedida.BaseUm,
                            UmName = unidadMedida.BaseUm,
                        });

                        Factor = unidadMedida.Factor;

                        _context.CreateTransactionLine(new TransactionLine { StoreId = storeId, TypeTrans = TypeTrans, LineNo = nextLine, OrderNo = Order.Data.OrderNo, ProductId = Product.Product.Id, ProductBarCode = ProductNo, ProductName = Product.Product.Name, Quantity = 0, QtyRecibida = 0, QtyPending = 0 - TotalQty, Um = unidadMedida.BaseUm, Bono = IsBonus, StorageId = storageId, BarCodeScanned = true, ProductInErpOrder = false });

                        LoadItemsAsync();
                    }
                }

                OrderItem = Items.FirstOrDefault(xc => xc.ProductId.Trim().Equals(ProductId.Trim()));

                //if ((TotalQty) > OrderItem.Qty)
                //{
                //    QuantityFocusRequested?.Invoke();
                //    throw new Exception("La cantidad recibida es mayor que la ordenada");
                //}
                    
                var un = MeasurementUnits.FirstOrDefault(xc => xc.BaseUm == OrderItem.Um);

                if (un == null)
                    throw new Exception($"Este Producto: {Product.Product.Name}  no contiene un factor para la unidad de medida:  {OrderItem.UmName}");
                    
                if (Product == null)
                    throw new Exception("Debe buscar un producto");
                    
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
                /*else
                {
                    Factor = 1;
                    QtyUnit = "1";
                }*/

                if (TotalQty == 0)
                {
                    QuantityFocusRequested?.Invoke();
                    throw new Exception("Debe digitar la cantidad  mayor de cero");
                }
                    
                int line = _context.NextTransactionLinesByOrderNo(TypeTrans, OrderNo);
                int exist = 0;

                if (!_context.ValidExist(TypeTrans, OrderNo, ProductId, IsBonus))
                {
                    _context.CreateTransactionLine(new TransactionLine { TypeTrans = TypeTrans, LineNo = line, OrderNo = Order.Data.OrderNo, ProductId = Product.Product.Id, ProductBarCode = ProductNo, ProductName = Product.Product.Name, Quantity = OrderItem.Qty, QtyRecibida = TotalQty, QtyPending = OrderItem.Qty - TotalQty, Um = OrderItem.Um, Bono = IsBonus, StoreId = OrderItem.StoreId, BarCodeScanned = false, ProductInErpOrder = true });
                }
                else
                {
                    var qline = _context.GetLine(TypeTrans, OrderNo, ProductId, IsBonus);

                    if (qline != null)
                    {
                        if (qline.QtyRecibida == 0)
                        {
                            _context.ExecuteSql($"UPDATE TransactionLine SET QtyRecibida = {TotalQty}, QtyPending = {(!IsBonus ? OrderItem.Qty - TotalQty : 0)} Where TypeTrans='{TypeTrans}' AND OrderNo = '{OrderNo}' AND ProductId = '{ProductId}'");
                        }
                        else
                        {

                            if (qline.QtyRecibida > 0 && (TotalQty + qline.QtyRecibida) > OrderItem.Qty && TypeTrans == "OT")
                            {
                                bool resp = await Application.Current.MainPage.DisplayAlert("Recepcion", $"El producto ya existe {(IsBonus ? " con Bono" : "")}, ¿Deseas  sustituir?", "Si", "No");

                                if (resp)
                                    _context.ExecuteSql($"UPDATE TransactionLine SET QtyRecibida = {TotalQty}, QtyPending = {(!IsBonus ? OrderItem.Qty - TotalQty : 0)} Where TypeTrans='{TypeTrans}' AND OrderNo = '{OrderNo}' AND ProductId = '{ProductId}' AND Bono = {IsBonus}");
                            }
                            else if (((TotalQty + qline.QtyRecibida) <= OrderItem.Qty && TypeTrans == "OT") || qline.QtyRecibida == 0)
                            {
                                bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", $"El producto ya existe {(IsBonus ? " con Bono" : "")}, ¿Deseas Sumar o Sustituir?", "Sustituir", "Sumar");

                                if (answer)
                                    _context.ExecuteSql($"UPDATE TransactionLine SET QtyRecibida = {TotalQty}, QtyPending = {(!IsBonus ? OrderItem.Qty - TotalQty : 0)} Where TypeTrans='{TypeTrans}' AND OrderNo = '{OrderNo}' AND ProductId = '{ProductId}' AND Bono = {IsBonus}");
                                else
                                    _context.ExecuteSql($"UPDATE TransactionLine SET QtyRecibida = QtyRecibida +{TotalQty}, QtyPending = QtyPending-{TotalQty} Where TypeTrans='{TypeTrans}' AND OrderNo = '{OrderNo}' AND ProductId = '{ProductId}' AND Bono = {IsBonus}");
                            }
                        }
                    }
                }

                ProductNo = "";
                ProductId = "";
                Quantity = "";
                TotalQty = 0;
                QtyUnit = "";
                Factor = 0;
                Unidad = "";
                Product = new ProductResult();
                NotEdition = true;
                InEdition = false;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        public async Task ProductByNo()
        {
            bool pExist = false;

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

                //if (!Items.Any(xc => xc.ProductId == Product.Product.Id))
                //    throw new Exception("Producto no esta orden de transportacion");
                    
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

                    //if (!pExist)
                    //    throw new Exception($"Este producto no tiene definida la unidad de medida con la que se encuentra en el documento.");
                }

                OrderItem = Items.FirstOrDefault(xc => xc.ProductId.Trim().Equals(ProductId.Trim()));

                //if (OrderItem == null)
                //{
                //    ProductNo = "";
                //    Quantity = "";
                //    TotalQty = 0;
                //    QtyUnit = string.Empty;
                //    Factor = 0;
                //    NotEdition = true;
                //    InEdition = false;
                //    throw new Exception("Producto no se encuentra en la Orden de Transporte");   
                //}

                //Product = new ProductResult();
                //Product = await repo.ProductByBarCode(ProductNo);
                MeasurementUnits = new ObservableCollection<MeasurementUnit>();
                Product.Product.MeasurementUnits.ToList().ForEach((un) => { MeasurementUnits.Add(un); });

                MeasurementUnit? un = null;

                if (OrderItem != null)
                    un = MeasurementUnits.FirstOrDefault(xc => xc.BaseUm == OrderItem.Um);

                //if (un == null)
                //    throw new Exception($"Este Producto: {Product.Product.Name}  no contiene un factor para la unidad de medida:  {OrderItem.Um}");

                var qline = _context.GetLine(TypeTrans, OrderNo, ProductId, IsBonus);

                if (qline != null && !qline.BarCodeScanned)
                    _context.ExecuteSql($"UPDATE TransactionLine SET BarCodeScanned = 1, ProductBarCode = '{ProductNo}'  Where TypeTrans='{TypeTrans}' AND OrderNo = '{OrderNo}' AND ProductId = '{ProductId}' ");

                Factor = un!= null ? un.Factor : 0;
                Unidad = $"Cantidad ({(OrderItem != null ? OrderItem.UmName : "")})";
                NotEdition = false;
                InEdition = true;
                pExist = false;
            }
            catch (Exception ex)
            {
                NotEdition = true;
                pExist = false;
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        private async Task OrderByIdAction()
        {
            bool hasClear = false;

            try
            {
                LoadItemsAsync();

                if (Details.Count > 0 && Details.Any(x => x.QtyRecibida > 0))
                {
                    bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", $"Esta Orden de Transaporte ya fue iniciada. ¿Desea Reanudar o Limpiar?", "Reanudar", "Limpiar");

                    if (!answer)
                    {
                        _context.DeleteTransationLineByOrderNo(TypeTrans, OrderNo);
                        Details.Clear();
                        ReceptionItems.Clear();
                        hasClear = true;
                    }
                }
                else
                {
                    _context.DeleteTransationLineByOrderNo(TypeTrans, OrderNo);
                    hasClear = true;
                }

                Order = await repo.TranspOrderByOrderNo(OrderNo);

                if (Order == null)
                    throw new Exception("Se produjo un error al consultar el documento, no se obtuvo ninguna información.");

                if (!Order.IsSuccess)
                    throw new Exception(Order.Message);

                if (Order.Data == null)
                    throw new Exception("No se recibieron datos datos. Por favor, póngase en contacto con el administrador del sistema.");

                if (!Order.IsSuccess)
                    throw new Exception($"Se ha producido un error al tratar al consultar la información: {Order.Message}.");

                if (Order.Data.Items.Any(xc => xc.StoreId != StoreNo))
                    throw new Exception($"Esta orden no esta configurada para ser procesada en la tienda {StoreName}");
                    
                if (Order.Data.Items.Any(xc => string.IsNullOrEmpty(xc.ProductBarCode)))
                    throw new Exception("La Orden contiene productos sin código de barra");
                    
                HasOrder = true;
                Items = Order.Data.Items.ToList();

                if (hasClear || (ReceptionItems.Count() == 0) && Order.Data.Items.Count() > 0)
                {
                    int c = 1;
                    Order.Data.Items.ToList().ForEach((l) =>
                    {
                        var line = new TransactionLine { TypeTrans = TypeTrans, LineNo = c, OrderNo = Order.Data.OrderNo, ProductId = l.ProductId , ProductBarCode = l.ProductBarCode, ProductName = l.ProductName, Quantity = l.Qty, QtyRecibida = 0, QtyPending = l.Qty, Um =l.Um, Bono = false, StoreId =l.StoreId, StorageId = l.StorageId, BarCodeScanned = false, ProductInErpOrder = true };
                        _context.SaveTransLine(line);
                        c++;
                    });
                }
            }
            catch (Exception ex)
            {
                OrderNo = "";
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        private void ResumenOpcion()
        {
            if (!HasOrder)
                return;

            ShowPanel("R");
            LoadItemsAsync();

            if (ReceptionItems.Count() > 0)
            {
                CanSave = true;

                if (IsThereDifferences())
                    MustToPrintDiff = true;
                else
                {
                    ManageToolbarItemVisibilityRequested?.Invoke(); //Fuerza la ejecucion de la funcion que muestra el boton guardar en el toolbar, pues
                                                                    //en otros casos esta metodo solo se ejecuta si la propiedad MustToPrintDiff cambia,
                                                                    //como en este caso no cambia hay que invocarlo
                }
            }
        }

        private bool IsThereDifferences()
        {
            var diff1 = (from a in Order.Data.Items
                         join b in ReceptionItems
                         on new { a.ProductId, a.Um } equals new { b.ProductId, b.Um }
                         into groupjoin
                         from c in groupjoin.DefaultIfEmpty()
                         where a.Qty - (c?.Qty != null ? c?.Qty : 0) != 0
                         select a.ProductId).ToList();

            var diff2 = (from a in ReceptionItems
                         join b in Order.Data.Items
                         on new { a.ProductId, a.Um } equals new { b.ProductId, b.Um }
                         into groupjoin
                         from c in groupjoin.DefaultIfEmpty()
                         where a.Qty - (c?.Qty != null ? c?.Qty : 0) != 0
                         select a.ProductId).ToList();

            var diff = diff1.Concat(diff2);

            return (diff.Count() > 0);
        }

        private void DetalleOpcion()
        {

            ShowPanel("D");
            ItemTitle = "";
        }

        private void ProductosOpcion()
        {
            if (!HasOrder)
                return;
            ShowPanel("P");
            CanSave = false;
            MustToPrintDiff = false;
            ItemTitle = "";
        }

        private void GeneralOpcion()
        {
            ShowPanel("G");
            CanSave = false;
            MustToPrintDiff = false;
            ItemTitle = "";
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
