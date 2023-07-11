using InventoryManagmentMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.ViewModels
{
    public class ReceptionStoreViewModel : BaseViewModel
    {
        public Command GeneralCommand { get; }
        public Command ProductosCommand { get; }
        public Command DetalleCommand { get; }
        public Command ResumenCommand { get; }
        public ObservableCollection<DetailDto> Details { get; set; }
        public ObservableCollection<TransDetail> TrDetails { get; set; }
        public ReceptionStoreViewModel()
        {
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


            GeneralCommand = new Command(() => GeneralOpcion());
            ProductosCommand = new Command(() => ProductosOpcion());
            DetalleCommand = new Command(() => DetalleOpcion());
            ResumenCommand = new Command(() => ResumenOpcion());
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
