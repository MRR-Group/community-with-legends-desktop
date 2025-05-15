using System;
using System.Linq;
using Application.UseCases;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;
using Avalonia.SimpleRouter;
using Flurl;
using Flurl.Http;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Presentation.ViewModels;
using Presentation.Views;
using Microsoft.Extensions.DependencyInjection;
using Ursa.Controls;

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
        var session = new CookieSession("https://cwl.purgal.xyz/api/");
        
        services.AddSingleton<HistoryRouter<ViewModelBase>>(s => new HistoryRouter<ViewModelBase>(t => (ViewModelBase)s.GetRequiredService(t)));
        services.AddSingleton<CookieSession> (s => session);

        services.AddSingleton<PermissionRepository> (s => new PermissionRepository());
        services.AddSingleton<UserRepository> (s => new UserRepository(s.GetService<CookieSession>()!));
        services.AddSingleton<AuthService> (s => new AuthService(s.GetService<CookieSession>()!, s.GetService<UserRepository>()!));
        services.AddSingleton<BanService> (s => new BanService(s.GetService<CookieSession>()!));
        services.AddSingleton<RoleService> (s => new RoleService(s.GetService<CookieSession>()!));
        services.AddSingleton<AnonymizeService> (s => new AnonymizeService(s.GetService<CookieSession>()!));
        services.AddSingleton<RegisterInteractor>(s => new RegisterInteractor(s.GetService<AuthService>()!));
        services.AddSingleton<LogInInteractor>(s => new LogInInteractor(s.GetService<AuthService>()!));
        services.AddSingleton<BanUserInteractor>(s => new BanUserInteractor(s.GetService<BanService>()!));
        services.AddSingleton<UnbanUserInteractor>(s => new UnbanUserInteractor(s.GetService<BanService>()!));
        services.AddSingleton<AnonymizeUserInteractor>(s => new AnonymizeUserInteractor(s.GetService<AnonymizeService>()!));
        services.AddSingleton<GiveModeratorRoleInteractor>(s => new GiveModeratorRoleInteractor(s.GetService<RoleService>()!));
        services.AddSingleton<RevokeModeratorRoleInteractor>(s => new RevokeModeratorRoleInteractor(s.GetService<RoleService>()!));
        services.AddSingleton<RevokeAdministratorRoleInteractor>(s => new RevokeAdministratorRoleInteractor(s.GetService<RoleService>()!));

        services.AddSingleton<MainViewModel>();
        services.AddTransient<LoginPageViewModel>();
        services.AddTransient<RegisterPageViewModel>();
        services.AddTransient<AdminsPageViewModel>();
        services.AddTransient<UsersPageViewModel>();
        
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