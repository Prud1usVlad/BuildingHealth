using Microsoft.Extensions.Logging;
using System.Text.Json;
using BuildingHealth.Mobile.Services.Interfaces;
using BuildingHealth.Mobile.Services;
using BuildingHealth.Mobile.Pages;
using BuildingHealth.Mobile.ViewModels;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace BuildingHealth.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp(true)
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterHttpClient()
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews();
                

#if DEBUG
		    builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<ProjectsViewModel>();
            mauiAppBuilder.Services.AddTransient<ProjectDetailsViewModel>();
            mauiAppBuilder.Services.AddTransient<StatisticsViewModel>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<AppShell>();
            mauiAppBuilder.Services.AddTransient<MainPage>();
            mauiAppBuilder.Services.AddTransient<Login>();
            mauiAppBuilder.Services.AddTransient<Projects>();
            mauiAppBuilder.Services.AddTransient<ProjectDetails>();
            mauiAppBuilder.Services.AddTransient<Statistics>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterHttpClient(this MauiAppBuilder mauiAppBuilder)
        {
            Uri apiAddress = new Uri("http://192.168.1.239:5254/api/");

            mauiAppBuilder.Services.AddTransient<HttpClient>(p =>
            {
                var client = new HttpClient
                {
                    BaseAddress = apiAddress,
                };

                client.DefaultRequestHeaders.Add("Accept", "application/json");

                return client;
            });

            mauiAppBuilder.Services.AddTransient<JsonSerializerOptions>(p =>
            {
                return new JsonSerializerOptions { PropertyNameCaseInsensitive =  true };
            });

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<IAuthService, AuthService>();
            mauiAppBuilder.Services.AddTransient<IProjectService, ProjectService>();
            mauiAppBuilder.Services.AddTransient<ICommentService, CommentService>();

            return mauiAppBuilder;
        }

    }
}