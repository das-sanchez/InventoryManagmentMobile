
using InventoryManagmentMobile.Database;
using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.Repositories;
using InventoryManagmentMobile.Views;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.ViewModels
{
    public class ReturnViewModel : BaseViewModel

    {
        public Command GeneralCommand { get; }
        public Command ProductosCommand { get; }
        public Command DetalleCommand { get; }
        public Command ResumenCommand { get; }

        public Command GetVendorCommand { get; }
        public Command GetProductCommand { get; }
        public Command AddProductCommand { get; }

        public Command SaveReturnCommand { get; }
        public Command<ReturnItem> RemoveItemCommand { get; }
        public ObservableCollection<DetailDto> Details { get; set; }
        public ObservableCollection<TransDetail> TrDetails { get; set; }

        public ObservableCollection<ReturnItem> ReturnDetails { get; set; }
        public OrderResult OrderResult { get; set; }
        public ReturnHead Order { get; set; }
        public VendorResult Vendor { get; set; }
        private ProductResult _product;
        public ProductResult Product { get { return _product; } set { SetProperty(ref _product, value); } }

        public ObservableCollection<MeasurementUnit> MeasurementUnits { get; set; }
        public MeasurementUnit MeasurementSelected { get; set; }
        string _productNo = string.Empty;
        public string ProductNo { get { return _productNo; } set { SetProperty(ref _productNo, value); } }

        private string _vendorNo;
        public string VendorNo { get => _vendorNo; set { SetProperty(ref _vendorNo, value); } }

        decimal _quantity;
        public decimal Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }
        private ReturnItem _orderItem;
        public ReturnItem OrderItem { get => _orderItem; set { SetProperty(ref _orderItem, value); } }

        string _storeNo = string.Empty;
        public string StoreNo { get { return _storeNo; } set { SetProperty(ref _storeNo, value); } }

        string _storageNo = string.Empty;
        public string StorageNo { get { return _storageNo; } set { SetProperty(ref _storageNo, value); } }

        public DateTime CurrentDate { get; set; }
        public readonly OleRepository Repo;
        public ObservableCollection<Storage> Storages { get; set; }
        private Storage _storageSelected;
        public Storage StorageSelected
        {
            get { return _storageSelected; }
            set
            {
                SetProperty(ref _storageSelected, value);
                if (value.Id != null)
                {
                    StorageNo = value.Id;
                }
            }
        }
        private StorageResult _storageresult;
        public StorageResult ObjStorageResult
        {
            get { return _storageresult; }
            set { SetProperty(ref _storageresult, value); }
        }
        public ReturnItem ObjItem { get; set; }

        private bool _hasVendor = false;
        public bool HasVendor { get { return _hasVendor; } set { SetProperty(ref _hasVendor, value); } }

        private bool _goBack = false;
        public bool GoBack { get { return _goBack; } set { SetProperty(ref _goBack, value); } }

        public Command BackCommand { get; }
        string _vendorName = string.Empty;
        public string VendorName { get { return _vendorName; } set { SetProperty(ref _vendorName, value); } }
        private bool _showContent = true;
        public bool ShowContent { get { return _showContent; } set { SetProperty(ref _showContent, value); } }

        private readonly OleDataContext _context;
        private bool _InEdition = false;
        public bool InEdition { get { return _InEdition; } set { SetProperty(ref _InEdition, value); } }
        string _lookupBarCode = "*";
        public string LookupBarCode { get { return _lookupBarCode; } set { SetProperty(ref _lookupBarCode, value); } }
        private bool _canSave = false;
        public bool CanSave { get { return _canSave; } set { SetProperty(ref _canSave, value); } }
        string _productId = string.Empty;
        public string ProductId { get { return _productId; } set { SetProperty(ref _productId, value); } }
        public ReturnViewModel(OleRepository _repo, OleDataContext context)

        {
            _context = context;
            ObjItem = new ReturnItem();
            StoreNo = Preferences.Get("storeNo", "Default Value");
            ObjStorageResult = new StorageResult();
            // StorageSelected = new Storage();
            Repo = _repo;
            //OrderItem = new ReturnItem();
            Order = new ReturnHead();
            General = new PanelOption() { PanelVisible = true, BarColor = Color.FromRgba("#cc3300") };
            Productos = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
            Detalle = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
            Resumen = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
            ;

            Details = new ObservableCollection<DetailDto>();
            ReturnDetails = new ObservableCollection<ReturnItem>();

            GeneralCommand = new Command(() => GeneralOpcion());
            ProductosCommand = new Command(() => ProductosOpcion());
            DetalleCommand = new Command(() => DetalleOpcion());
            ResumenCommand = new Command(() => ResumenOpcion());

            GetVendorCommand = new Command(() => VendorByNo());
            GetProductCommand = new Command(() => ProductByNo());
            AddProductCommand = new Command(() => AddProduct());
            SaveReturnCommand = new Command(() => SaveReturn());

            RemoveItemCommand = new Command<ReturnItem>(RemoveReturnItem);

            MeasurementUnits = new ObservableCollection<MeasurementUnit>();

            Storages = new ObservableCollection<Storage>();
            LoadStorages();
            CurrentDate = DateTime.Now;
            BackCommand = new Command(() => BackSync());
            ShowContent = true;
            CanSave = false;

        }
        public async void LoadItemsAsync()
        {
            try
            {
                var items = new List<ReturnLine>();
                int linenumber = 10;
                ReturnDetails.Clear();
                items = _context.GetReturnLinesByOrderNo(VendorNo);
                if (items is not null && items.Any())
                {

                    foreach (var line in items)
                    {

                        ReturnDetails.Add(new ReturnItem { ProductBarCode = line.ProductBarCode, ProductId = line.ProductId, Qty = line.Quantity, Um = line.Um, StorageId = StorageNo, LineNo = linenumber });
                        linenumber += 10;
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
                var items = new List<ReturnLine>();
                int linenumber = 10;
                ReturnDetails.Clear();
                items = _context.GetReturnLinesByOrderNo(VendorNo, barCode);
                if (items is not null && items.Any())
                {

                    foreach (var line in items)
                    {

                        ReturnDetails.Add(new ReturnItem { ProductBarCode = line.ProductBarCode, ProductId = line.ProductId, Qty = line.Quantity, Um = line.Um, StorageId = StorageNo, LineNo = linenumber });
                        linenumber += 10;
                    }
                }
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Errorn", ex.Message, "Aceptar");
            }


        }
        private async void BackSync()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Devolucion", "Esta seguro de Salir?", "Si", "No");
            if (answer)
            {
                await Shell.Current.Navigation.PopAsync();
            }
        }
        private async void RemoveReturnItem(ReturnItem item)
        {

            try
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Devolucion", "Desea Remover este producto de la Devolucion?", "Yes", "No");
                if (answer)
                {
                    var qitem = ReturnDetails.FirstOrDefault(x => x.ProductId == item.ProductId || x.ProductBarCode == item.ProductBarCode);
                    ReturnDetails.Remove(qitem);
                    //Details.Remove(dto);
                    _context.ExecuteSql($"DELETE FROM ReturnLine Where VendorNo = '{VendorNo}' AND ProductId = '{item.ProductId}'");
                    LoadItemsAsync();
                }
            }
            catch (Exception ex)
            {


                await Application.Current.MainPage.DisplayAlert("Errorn", ex.Message, "Aceptar");
            }
        }

        private async void LoadStorages()
        {
            ObjStorageResult = new StorageResult();
            ObjStorageResult = await Repo.StorageByNo(StoreNo);
            if (ObjStorageResult.Data != null)
            {
                ObjStorageResult.Data.ToList().ForEach(kv =>
                {
                    Storages.Add(kv);
                });

            }

        }

        private async void SaveReturn()
        {
            try
            {
                if (Vendor.Data == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Guardar Devolucion", "Proveedor No Valido", "Aceptar");
                    return;
                }
                if (ReturnDetails == null || (ReturnDetails != null && ReturnDetails.Count == 0))
                {
                    await Application.Current.MainPage.DisplayAlert("Guardar Devolucion", "No has agregado detalle", "Aceptar");
                    return;
                }
                bool answer = await Application.Current.MainPage.DisplayAlert("Devolucion", "Desea guardar la Devolucion?", "Yes", "No");
                if (answer)
                {

                    Order.Items = ReturnDetails.ToArray();
                    Order.Comment = "";
                    Order.VendorId = Vendor.Data.Id;

                    IsBusy = true;
                    ShowContent = false;
                    var Result = await Repo.SaveReturn(Order);
                    //Thread.Sleep(5000);
                    IsBusy = false;
                    ShowContent = true;
                    if (Result.IsSuccess)
                    {
                        // ShowSucces("Transaccion Procesada Correctamente");
                        var dialogParam = new Dialog() { Icon = "checked2x", Description = Result.Message, Title = "Devolucion Mercancia", Label = "Volver al Inicio" };
                        var dialogAlertViewModel = new DialogAlertViewModel(dialogParam, goToBeginning: true);
                        
                        await Shell.Current.Navigation.PushModalAsync(new DialogAlert(dialogAlertViewModel));
                        await Task.Delay(5000);

                        if (dialogAlertViewModel.UserHasInteracted)
                            await Shell.Current.Navigation.PopToRootAsync();
                    }
                    else
                    {
                        // ShowError(Result.MessagesFromErp[0].Message);
                        var dialogParam = new Dialog() { Icon = "cross2x", Description = Result.Message, Title = "Devolucion Mercancia", Label = "Volver al Inicio" };
                        await Shell.Current.Navigation.PushModalAsync(new DialogAlert(new DialogAlertViewModel(dialogParam)));
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private async void AddProduct()
        {
            try
            {
                //ProductByNo();
                if (Product == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Agregar Line", "Debe buscar un producto", "Aceptar");
                    return;
                }
                if (StorageSelected == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Agregar Line", "No a seleccionado un storage", "Aceptar");
                    return;
                }
                if (string.IsNullOrEmpty(ProductNo))
                {
                    await Application.Current.MainPage.DisplayAlert("Agregar Line", "El producto no esta en blanco", "Aceptar");
                    return;
                }
                if (Product.Product.Id != ProductId)
                {
                    await Application.Current.MainPage.DisplayAlert("Agregar Line", "El Producto digitado es diferente al que fue consultado, eso significa que los cambiaste y no lo buscate bestia", "Aceptar");
                    return;
                }
                if (Quantity == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Agregar Line", "Debe digitar la cantidad de devolver", "Aceptar");
                    return;
                }
                int line = (ReturnDetails.Count == 0 ? 1 : ReturnDetails.Max(xc => xc.LineNo) + 1);

                if (!_context.ValidExistReturnLine(VendorNo, ProductId))
                {
                    _context.CreateReturnLine(new ReturnLine { VendorNo = VendorNo, ProductBarCode = ProductNo, ProductId = Product.Product.Id, ProductName = Product.Product.Name, Quantity = Quantity, Um = Product.Product.BaseUm });
                }
                else
                {

                    var qline = _context.GetReturnLine(VendorNo, ProductId);

                    if (qline != null)
                    {

                        bool answer = await Application.Current.MainPage.DisplayAlert("Devolucion", $"El producto ya Existe  desea Sumar o Sustituir)?", "Sustituir", "Sumar");
                        if (answer)
                        {
                            //Details.ToList().ForEach((i) => { if (i.ProductBarCode == ProductNo) { i.QtyRecibida += TotalQty; i.QtyPending = i.Quantity - i.QtyRecibida; } });

                            _context.ExecuteSql($"UPDATE ReturnLine SET Quantity = {Quantity} Where VendorNo = '{VendorNo}' AND ProductId = '{ProductId}'");
                        }
                        else
                        {
                            _context.ExecuteSql($"UPDATE ReturnLine SET Quantity = Quantity +{Quantity} Where VendorNo = '{VendorNo}' AND ProductId = '{ProductId}'");


                        }

                    }

                }
                ProductNo = "";
                ProductId = "";
                Quantity = 0;
                Product = new ProductResult();
                InEdition = false;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task ProductByNo()
        {
            try
            {
                Product = new ProductResult();
                Product = await Repo.ProductByBarCode(ProductNo);
                if (Product.Product == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Producto", "Producto no Existe", "Aceptar");
                    InEdition = false;
                    return;
                }
                Product.Product.MeasurementUnits.ToList().ForEach((un) => { MeasurementUnits.Add(un); });
                ProductId = Product.Product.Id;
                InEdition = true;
            }
            catch (Exception)
            {
                InEdition = false;
                throw;
            }


        }

        public async Task VendorByNo()
        {
            try
            {
                LoadItemsAsync();
                if (ReturnDetails.Count() > 0)
                {
                    bool answer = await Application.Current.MainPage.DisplayAlert("Recepcion", $"Esta Devolucion ya fue iniciada Desea Reanudar o Limpiar  e iniciar desde Cero?", "Reanudar", "Limpiar");
                    if (!answer)
                    {
                        _context.DeleteReturnLineByVendorNo(VendorNo);

                        ReturnDetails.Clear();

                    }
                }

                Vendor = new VendorResult();
                Vendor = await Repo.VendorById(VendorNo);
                if (Vendor.Data == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Proveedor", "Proveedor no Existe", "Aceptar");
                    return;
                }
                VendorName = Vendor.Data.Name;

                HasVendor = true;
            }
            catch (Exception ex)
            {

                throw;
            }


        }
        public async Task GetVendorByNo()
        {
            Vendor = new VendorResult();
            Vendor = await Repo.VendorById(VendorNo);
            if (Vendor.Data == null)
            {
                //await Application.Current.MainPage.DisplayAlert("Proveedor", "Proveedor no Existe", "Aceptar");
                return;
            }
            VendorName = Vendor.Data.Name;

            HasVendor = true;

        }

        private void ResumenOpcion()
        {
            if (ReturnDetails.Count() > 0)
            {
                CanSave = true;
            }
            ShowPanel("R");
        }

        private void DetalleOpcion()
        {
            if (!HasVendor)
            {
                return;
            }
            ShowPanel("D");

            LoadItemsAsync();
            CanSave = false;
        }

        private void ProductosOpcion()
        {
            if (!HasVendor)
            {
                return;
            }
            CanSave = false;
            ShowPanel("P");
        }

        private void GeneralOpcion()
        {
            CanSave = false;
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
