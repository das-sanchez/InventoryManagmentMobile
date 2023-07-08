
using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.Models.Dtos;
using InventoryManagmentMobile.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.ViewModels
{
    public class ReceptionViewModel : BaseViewModel
    {
        public Command GeneralCommand { get; }
        public Command ProductosCommand { get; }
        public Command DetalleCommand { get; }
        public Command ResumenCommand { get; }

        public Command AddItemCommand { get; }
        public Command RemoveItemCommand { get; }
        public Command AddResumenCommand { get; }
        public Command RemoveResumenCommand { get; }
        public Command SaveReceptionCommand { get; }

        public ObservableCollection<DetailDto> Details { get; set; }
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

        private OrderResult _order;
        public OrderResult Order { get => _order; set { SetProperty(ref _order, value); } }


        private OrderItem _orderitem;
        public OrderItem OrderItem { get => _orderitem; set { SetProperty(ref _orderitem, value); } }

        private ReceptionItem _item;
        public ReceptionItem Item { get => _item; set { SetProperty(ref _item, value); } }

        private ReceptionItem _itemSelected;
        public ReceptionItem ItemSelected { get => _itemSelected; set { SetProperty(ref _itemSelected, value); } }

        private ItemSummary _itemSummarySelected;
        public ItemSummary ItemSummrySelected { get => _itemSummarySelected; set { SetProperty(ref _itemSummarySelected, value); } }


        decimal _quantity = 0;
        public decimal Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }
        decimal _totalQty = 0;
        public decimal TotalQty
        {
            get { return _totalQty; }
            set { SetProperty(ref _totalQty, value); }
        }
        decimal _qtyUni = 0;
        public decimal QtyUnit
        {
            get { return _qtyUni; }
            set { SetProperty(ref _qtyUni, value); }
        }
        public int Factor { get; set; } = 0;
        public ProductResult Product { get; set; }
        public bool IsBonus { get; set; } = false;

        public DateTime ExpirationDate { get; set; }
        public ObservableCollection<MeasurementUnit> MeasurementUnits { get; set; }
        public MeasurementUnit MeasurementSelected { get; set; }
        public ReceptionViewModel(OleRepository _repo)
        {
            repo = _repo;
            General = new PanelOption() { PanelVisible = true, BarColor = Color.FromRgba("#cc3300") };
            Productos = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
            Detalle = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };
            Resumen = new PanelOption() { PanelVisible = false, BarColor = Color.FromRgba("#006600") };

            Items = new List<OrderItem>();

            Details = new ObservableCollection<DetailDto>();
            Details.Add(new DetailDto() { ProductId = 1, ProductName = "Latas de Atun en Aceite Vegetal de 220gr", QtyPending = 10, Quantity = 150, QtyRecibida = 150, Stock = 15 });
            Details.Add(new DetailDto() { ProductId = 1, ProductName = "Latas de Tilapia en Aceite Vegetal de 220gr", QtyPending = 10, Quantity = 150, QtyRecibida = 150, Stock = 15 });
            Details.Add(new DetailDto() { ProductId = 1, ProductName = "Latas de Baguet en Aceite Vegetal de 220gr", QtyPending = 10, Quantity = 150, QtyRecibida = 150, Stock = 15 });
            Details.Add(new DetailDto() { ProductId = 1, ProductName = "Latas de Trucha en Aceite Vegetal de 220gr", QtyPending = 10, Quantity = 150, QtyRecibida = 150, Stock = 15 });


            GeneralCommand = new Command(() => GeneralOpcion());
            ProductosCommand = new Command(() => ProductosOpcion());
            DetalleCommand = new Command(() => DetalleOpcion());
            ResumenCommand = new Command(() => ResumenOpcion());
            OrderByIdCommand = new Command(async () => await OrderByIdAction());
            ProductByNoCommand = new Command(() => ProductByNo());
            AddItemCommand = new Command(() => AddItem());
            RemoveItemCommand = new Command(() => RemoveItem());
            AddResumenCommand = new Command(() => AddItemSummary());
            RemoveResumenCommand = new Command(() => RemoveItemSummary());
            SaveReceptionCommand = new Command(() => SaveReception());

            OrderItem = new OrderItem();
            ItemsSummary = new ObservableCollection<ItemSummary>();
            ReceptionItems = new ObservableCollection<ReceptionItem>();
            Item = new ReceptionItem();
            ItemSelected = new ReceptionItem();
            ItemSummrySelected = new ItemSummary();
            MeasurementUnits = new ObservableCollection<MeasurementUnit>();
        }

        private void SaveReception()
        {
            throw new NotImplementedException();
        }

        private void RemoveItemSummary()
        {
            throw new NotImplementedException();
        }

        private void AddItemSummary()
        {
            throw new NotImplementedException();
        }

        private void RemoveItem()
        {
            throw new NotImplementedException();
        }

        private void AddItem()
        {
            if (!ReceptionItems.Any(xc => xc.ProductBarCode == ProductNo))
            {
                ReceptionItems.Add(new ReceptionItem() { ProductBarCode = ProductNo, ProductId = Product.Product.Id, Factor = Factor, LineNo = OrderItem.LineNo, LotNo = NoLote, Qty = Quantity, Um = OrderItem.Um, Bono = IsBonus, LotExpirationDate = ExpirationDate });
            }
        }

        private async void ProductByNo()
        {
            try
            {
                //OrderItem = new OrderItem();
                if (string.IsNullOrEmpty(ProductNo)) return;
                OrderItem = Items.FirstOrDefault(xc => xc.ProductBarCode.Trim().Equals(ProductNo.Trim()));
                ////var qOrderItem = new OrderItem();
                //Items.ForEach((p) =>
                //{
                //    if (p.ProductBarCode.Trim() == ProductNo || p.ProductNo.Trim() == ProductNo)
                //    {
                //        OrderItem = p;
                //        ;
                //    }
                //});
                Product = new ProductResult();
                Product = await repo.ProductByBarCode(ProductNo);

                Product.Product.MeasurementUnits.ToList().ForEach((un) => { MeasurementUnits.Add(un); });
                var un = MeasurementUnits.FirstOrDefault(xc => xc.BaseUm == OrderItem.Um);
                Factor = un.Factor;
                Unidad = $"{OrderItem.Um} ({un.Factor.ToString()})";
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task OrderByIdAction()
        {
            Order = new OrderResult();
            Order = await repo.OrderByOrderNo(OrderNo);
            Items = Order.Data.Items.ToList();
        }

        public Command OrderByIdCommand { get; }
        public Command ProductByNoCommand { get; }

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
