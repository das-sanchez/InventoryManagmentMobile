﻿using InventoryManagmentMobile.Repositories;
using InventoryManagmentMobile.ViewModels;
using InventoryManagmentMobile.Views;

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
        builder.Services.AddSingleton<ReceptionPage>();
        builder.Services.AddSingleton<ReturnsPage>();
        builder.Services.AddSingleton<TransferPage>();
        builder.Services.AddSingleton<RecepcionStorePage>();
        builder.Services.AddSingleton<ReceptionCDPage>();

        //View Models
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<ReceptionMenuViewModel>();
        builder.Services.AddSingleton<ReceptionViewModel>();
        builder.Services.AddSingleton<ReturnViewModel>();
        builder.Services.AddSingleton<TransferViewModel>();
        builder.Services.AddSingleton<ReceptionStoreViewModel>();
        builder.Services.AddSingleton<ReceptionCDViewModel>();

        builder.Services.AddSingleton<OrderRepository>();
        builder.Services.AddSingleton<OleRepository>();


        return builder.Build();



    }
}
