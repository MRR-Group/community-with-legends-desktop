using System;
using System.Linq;
using Application.UseCases;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;
using Avalonia.SimpleRouter;
using Flurl;
using Infrastructure.Services;
using Presentation.ViewModels;
using Presentation.Views;
using Microsoft.Extensions.DependencyInjection;

[assembly: XmlnsDefinition("https://github.com/avaloniaui", "Presentation.Controls")]

namespace Presentation;

public partial class App : Avalonia.Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        IServiceProvider services = ConfigureServices();
        var mainViewModel = services.GetRequiredService<MainViewModel>();

        if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        { 
            this.DisableAvaloniaDataAnnotationValidation();
            
            desktop.MainWindow = new MainView
            {
                DataContext = mainViewModel,
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        
        services.AddSingleton<HistoryRouter<ViewModelBase>>(s => new HistoryRouter<ViewModelBase>(t => (ViewModelBase)s.GetRequiredService(t)));
        services.AddSingleton<AuthService> (s => new AuthService(new Url("https://cwl.purgal.xyz/api/")));
        services.AddSingleton<SignUpInteractor>(s => new SignUpInteractor(s.GetService<AuthService>()!));
        
        services.AddSingleton<MainViewModel>();
        services.AddTransient<LoginPageViewModel>();
        services.AddTransient<RegisterPageViewModel>();
        services.AddTransient<AdminsPageViewModel>();
        
        return services.BuildServiceProvider();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}