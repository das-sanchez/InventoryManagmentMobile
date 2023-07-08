using InventoryManagmentMobile.Views;

namespace InventoryManagmentMobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(ReceptionPage), typeof(ReceptionPage));
        Routing.RegisterRoute($"//ReceptionMenuPage/ReceptionPage", typeof(ReceptionPage));
        Routing.RegisterRoute($"//ReceptionMenuPage/ReceptionCDPage", typeof(ReceptionCDPage));
        Routing.RegisterRoute($"//ReceptionMenuPage/RecepcionStorePage", typeof(RecepcionStorePage));
        Routing.RegisterRoute(nameof(ReceptionCDPage), typeof(ReceptionCDPage));
        Routing.RegisterRoute(nameof(RecepcionStorePage), typeof(RecepcionStorePage));
        Routing.RegisterRoute(nameof(ReceptionMenuPage), typeof(ReceptionMenuPage));
        Routing.RegisterRoute(nameof(ReturnsPage), typeof(ReturnsPage));
        Routing.RegisterRoute(nameof(TransferPage), typeof(TransferPage));
    }
}
