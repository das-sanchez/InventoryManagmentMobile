using InventoryManagmentMobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        string _orderNo = string.Empty;
        public string OrderNo { get => _orderNo; set { SetProperty(ref _orderNo, value); } }



        int _documentNumber = 0;
        public int DocumentNumber
        {
            get { return _documentNumber; }
            set { SetProperty(ref _documentNumber, value); }
        }

        int _providerNumber = 0;
        public int ProvidertNumber
        {
            get { return _providerNumber; }
            set { SetProperty(ref _providerNumber, value); }
        }
        string _orderlabel = string.Empty;
        public string OrderLabel { get { return _orderlabel; } set { SetProperty(ref _orderlabel, value); } }
        string _orderPlaceHolderlabel = string.Empty;
        public string OrderPlaceHolderLabel { get { return _orderPlaceHolderlabel; } set { SetProperty(ref _orderPlaceHolderlabel, value); } }

        PanelOption _panelGeneral;
        public PanelOption General
        {
            get { return _panelGeneral; }
            set { SetProperty(ref _panelGeneral, value); }
        }
        PanelOption _panelProductos;
        public PanelOption Productos
        {
            get { return _panelProductos; }
            set { SetProperty(ref _panelProductos, value); }
        }
        PanelOption _panelDetalle;
        public PanelOption Detalle
        {
            get { return _panelDetalle; }
            set { SetProperty(ref _panelDetalle, value); }
        }
        PanelOption _panelResumen;
        public PanelOption Resumen
        {
            get { return _panelResumen; }
            set { SetProperty(ref _panelResumen, value); }
        }
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
