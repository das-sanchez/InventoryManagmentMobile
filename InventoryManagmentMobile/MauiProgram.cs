using InventoryManagmentMobile.Database;
using InventoryManagmentMobile.Repositories;
using InventoryManagmentMobile.ViewModels;
using InventoryManagmentMobile.Views;
using Microsoft.Maui.Controls.Compatibility.Hosting;

namespace InventoryManagmentMobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()

            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Nunito-Regular.ttf", "NunitoRegular");
                fonts.AddFont("Nunito-Italic.ttf", "NunitoItalic");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");


            });
        //Views
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<ReceptionMenuPage>();
        builder.Services.AddTransient<ReceptionPage>();
        builder.Services.AddTransient<ReturnsPage>();
        builder.Services.AddSingleton<TransferPage>();
        builder.Services.AddSingleton<RecepcionStorePage>();
        builder.Services.AddTransient<ReceptionCDPage>();
        builder.Services.AddSingleton<DialogAlert>();


        //View Models
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<ReceptionMenuViewModel>();
        builder.Services.AddTransient<ReceptionViewModel>();
        builder.Services.AddTransient<DialogAlertViewModel>();
        builder.Services.AddSingleton<ReturnViewModel>();
        builder.Services.AddSingleton<TransferViewModel>();
        builder.Services.AddSingleton<ReceptionStoreViewModel>();
        builder.Services.AddTransient<ReceptionCDViewModel>();


        builder.Services.AddSingleton<OrderRepository>();
        builder.Services.AddSingleton<OleRepository>();
        builder.Services.AddSingleton<OleDataContext>();



        return builder.Build();



    }
}
