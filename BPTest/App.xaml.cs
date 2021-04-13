using Xamarin.Forms;
using BPTest.Services;
using Autofac.Core;
using Autofac;
using SensorHub.Backend;
using System.Net.Http;
using BPTest.Services.Interfaces;
using BPTest.Shared.Constants;
using BPTest.ViewModels;
using System;
using BPTest.Repositories.Interfaces;
using BPTest.Repositories;
using BPTest.Shared.Models.Authentication;
using BPTest.Views;
using BPTest.Utils;

namespace BPTest
{
    public partial class App : Application
    {
        public static IContainer DependencyService { get; private set; }
        public App(ContainerBuilder containerBuilder)
        {
            InitializeComponent();

            MainPage = new AppShell();

            Action<string> settingsChangeAction = null;
            var httpClient = new HttpClient();
            var client = new SwaggerClient(httpClient, ref settingsChangeAction);
            var settingsService = new SettingsService(settingsChangeAction);
            var authToken = settingsService.Get<AuthorizationToken>(SettingsKeys.AuthorizationTokenSettingsKey, null).GetAwaiter().GetResult();
            if (authToken != null)
                client.UpdateAuthToken(authToken.Token);
            containerBuilder.RegisterInstance(client).As<ISwaggerClient>().SingleInstance();
            containerBuilder.RegisterInstance(settingsService).AsImplementedInterfaces().SingleInstance();
            containerBuilder.RegisterType<LoginService>().AsImplementedInterfaces().SingleInstance();
            containerBuilder.RegisterType<DeviceService>().AsImplementedInterfaces().SingleInstance();
            containerBuilder.RegisterType<SettingsViewModel>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<LoginViewModel>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<DevicesViewModel>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<DeviceReadingViewModel>().AsSelf();
            containerBuilder.RegisterType<DeviceRepository>().AsImplementedInterfaces().SingleInstance();
            containerBuilder.RegisterType<DeviceBuilderFactory>().AsImplementedInterfaces().SingleInstance();

            DependencyService = containerBuilder.Build();
            Routing.RegisterRoute("devices/details", typeof(DeviceReadingPage));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
