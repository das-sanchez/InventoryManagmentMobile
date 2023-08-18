using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.Repositories;
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
        public ObservableCollection<DetailDto> Details { get; set; }
        public ObservableCollection<TransDetail> TrDetails { get; set; }

        public ObservableCollection<ReturnItem> ReturnDetails { get; set; }
        public OrderResult OrderResult { get; set; }
        public ReturnHead Order { get; set; }
        public VendorResult Vendor { get; set; }
        public ProductResult Product { get; set; }

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
        private readonly OleRepository Repo;
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
        public ReturnViewModel(OleRepository _repo)

        {
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

            TrDetails = new ObservableCollection<TransDetail>();
            TrDetails.Add(new TransDetail() { ProductId = 1, ProductName = "Latas de Atun en Aceite Vegetal de 220gr", Quantity = 10, Factor = 12, QtyTotal = 120 });
            TrDetails.Add(new TransDetail() { ProductId = 1, ProductName = "Latas de Tilapia en Aceite Vegetal de 220gr", Quantity = 10, Factor = 12, QtyTotal = 120 });
            TrDetails.Add(new TransDetail() { ProductId = 1, ProductName = "Latas de Baguet en Aceite Vegetal de 220gr", Quantity = 15, Factor = 10, QtyTotal = 150 });
            TrDetails.Add(new TransDetail() { ProductId = 1, ProductName = "Latas de Trucha en Aceite Vegetal de 220gr", Quantity = 6, Factor = 12, QtyTotal = 60 });

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

            MeasurementUnits = new ObservableCollection<MeasurementUnit>();

            Storages = new ObservableCollection<Storage>();
            LoadStorages();
            CurrentDate = DateTime.Now;
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
            bool answer = await Application.Current.MainPage.DisplayAlert("Devolucion", "Desea guardar la Devolucion?", "Yes", "No");
            if (answer)
            {
                if (ReturnDetails == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Guardar Devolucoin", "No a Agregado detalle", "Aceptar");
                    return;
                }
                Order.Items = ReturnDetails.ToArray();
                Order.Comment = "";
                Order.VendorId = Vendor.Data.Id;

                var Result = await Repo.SaveReturn(Order);
            }
            else
            {
                return;
            }
        }

        private async void AddProduct()
        {
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
            if (Quantity == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Agregar Line", "Debe digitar la cantidad de devolver", "Aceptar");
                return;
            }
            int line = (ReturnDetails.Count == 0 ? 1 : ReturnDetails.Max(xc => xc.LineNo) + 1);


            ObjItem.ProductId = Product.Product.Id; ObjItem.ProductBarCode = Product.Product.BarCode; ObjItem.LineNo = line; ObjItem.Qty = Quantity; ObjItem.Um = Product.Product.BaseUm;
            ObjItem.StorageId = StorageSelected.Id;
            ReturnDetails.Add(ObjItem);

            ProductNo = "";
            Quantity = 0;
        }

        private async void ProductByNo()
        {
            Product = new ProductResult();
            Product = await Repo.ProductByBarCode(ProductNo);
            if (Product.Product == null)
            {
                await Application.Current.MainPage.DisplayAlert("Producto", "Producto no Existe", "Aceptar");
                return;
            }
            Product.Product.MeasurementUnits.ToList().ForEach((un) => { MeasurementUnits.Add(un); });

        }

        private async void VendorByNo()
        {
            Vendor = new VendorResult();
            if (Vendor.Data == null)
            {
                await Application.Current.MainPage.DisplayAlert("Proveedor", "Proveedor no Existe", "Aceptar");
                return;
            }
            Vendor = await Repo.VendorById(VendorNo);

        }

        private void ResumenOpcion()
        {
            ShowPanel("R");
        }

        private void DetalleOpcion()
        {

            ShowPanel("D");
        }

        private void ProductosOpcion()
        {
            if (Vendor.Data == null)
            {
                return;
            }

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
