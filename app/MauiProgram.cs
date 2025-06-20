﻿using Microsoft.Extensions.Logging;
using NutritionApp.Services;
using NutritionApp.ViewModels;
using NutritionApp.Views;
using Microsoft.Maui.Controls;


namespace NutritionApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
               .ConfigureFonts(fonts =>
               {
                   fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                   // Додайте цей рядок
                   fonts.AddFont("FluentSystemIcons-Regular.ttf", "FluentIcons");

               });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            // Реєструємо сервіс та сторінки
            builder.Services.AddSingleton<ApiService>();

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddTransient<RegisterPage>(); // Створюється кожен раз заново
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddTransient<EditProfilePage>();
            builder.Services.AddTransient<EditProfileViewModel>();

            builder.Services.AddTransient<MealPlanPage>();
            builder.Services.AddTransient<MealPlanViewModel>();


            builder.Services.AddTransient<HistoryPage>();
            builder.Services.AddTransient<HistoryViewModel>();
            builder.Services.AddTransient<HistoryDetailPage>();



            return builder.Build();
        }
    }
}