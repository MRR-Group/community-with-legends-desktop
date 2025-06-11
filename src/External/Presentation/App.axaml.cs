using System;
using System.Linq;
using Application.UseCases;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;
using Avalonia.SimpleRouter;
using Domain.Entities;
using Flurl;
using Flurl.Http;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
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
        var session = new CookieSession("https://cwl.legnica.pl/api");
        
        services.AddSingleton<HistoryRouter<ViewModelBase>>(s => new HistoryRouter<ViewModelBase>(t => (ViewModelBase)s.GetRequiredService(t)));
        services.AddSingleton<CookieSession> (s => session);

        services.AddSingleton<PermissionRepository> (s => new PermissionRepository());
        services.AddSingleton<UserRepository> (s => new UserRepository(s.GetService<CookieSession>()!));
        services.AddSingleton<AdminRepository> (s => new AdminRepository(s.GetService<CookieSession>()!));
        services.AddSingleton<ReportRepository> (s => new ReportRepository(s.GetService<CookieSession>()!));
        services.AddSingleton<GameRepository> (s => new GameRepository(s.GetService<CookieSession>()!));
        services.AddSingleton<LogsRepository> (s => new LogsRepository(s.GetService<CookieSession>()!));
        services.AddSingleton<StatisticsRepository> (s => new StatisticsRepository(s.GetService<CookieSession>()!));
        services.AddSingleton<AuthService> (s => new AuthService(s.GetService<CookieSession>()!, s.GetService<UserRepository>()!, s.GetService<PermissionRepository>()!));
        services.AddSingleton<AdminService> (s => new AdminService(s.GetService<CookieSession>()!, s.GetService<AdminRepository>()!));
        services.AddSingleton<ReportService> (s => new ReportService(s.GetService<CookieSession>()!));
        services.AddSingleton<ProfileService> (s => new ProfileService(s.GetService<CookieSession>()!));
        services.AddSingleton<TFAService> (s => new TFAService(s.GetService<CookieSession>()!));
        services.AddSingleton<PostService> (s => new PostService(s.GetService<CookieSession>()!));
        services.AddSingleton<CommentService> (s => new CommentService(s.GetService<CookieSession>()!));
        services.AddSingleton<HardwareService> (s => new HardwareService(s.GetService<CookieSession>()!));
        services.AddSingleton<GameService> (s => new GameService(s.GetService<CookieSession>()!));
        
        services.AddSingleton<BanService> (s => new BanService(s.GetService<CookieSession>()!));
        services.AddSingleton<RoleService> (s => new RoleService(s.GetService<CookieSession>()!));
        services.AddSingleton<AnonymizeService> (s => new AnonymizeService(s.GetService<CookieSession>()!));
        services.AddSingleton<RegisterInteractor>(s => new RegisterInteractor(s.GetService<AuthService>()!));
        services.AddSingleton<LogInInteractor>(s => new LogInInteractor(s.GetService<AuthService>()!));
        services.AddSingleton<LogOutInteractor>(s => new LogOutInteractor(s.GetService<AuthService>()!));
        services.AddSingleton<BanUserInteractor>(s => new BanUserInteractor(s.GetService<BanService>()!));
        services.AddSingleton<UnbanUserInteractor>(s => new UnbanUserInteractor(s.GetService<BanService>()!));
        services.AddSingleton<AnonymizeUserInteractor>(s => new AnonymizeUserInteractor(s.GetService<AnonymizeService>()!));
        services.AddSingleton<GiveModeratorRoleInteractor>(s => new GiveModeratorRoleInteractor(s.GetService<RoleService>()!));
        services.AddSingleton<RevokeModeratorRoleInteractor>(s => new RevokeModeratorRoleInteractor(s.GetService<RoleService>()!));
        services.AddSingleton<RevokeAdministratorRoleInteractor>(s => new RevokeAdministratorRoleInteractor(s.GetService<AdminService>()!));
        services.AddSingleton<CreateAdministratorUserInteractor>(s => new CreateAdministratorUserInteractor(s.GetService<AdminService>()!));
        services.AddSingleton<DeleteAdministratorInteractor>(s => new DeleteAdministratorInteractor(s.GetService<AdminService>()!));
        services.AddSingleton<DeleteReportableInteractor>(s => new DeleteReportableInteractor(s.GetService<PostService>()!, s.GetService<CommentService>()!));
        services.AddSingleton<RestoreDeleteReportableInteractor>(s => new RestoreDeleteReportableInteractor(s.GetService<PostService>()!, s.GetService<CommentService>()!));
        services.AddSingleton<CloseReportInteractor>(s => new CloseReportInteractor(s.GetService<ReportService>()!));
        services.AddSingleton<ReopenReportInteractor>(s => new ReopenReportInteractor(s.GetService<ReportService>()!));
        services.AddSingleton<DeleteAvatarInteractor>(s => new DeleteAvatarInteractor(s.GetService<ProfileService>()!));
        services.AddSingleton<RenameUserInteractor>(s => new RenameUserInteractor(s.GetService<ProfileService>()!));
        services.AddSingleton<ValidateTFAInteractor>(s => new ValidateTFAInteractor(s.GetService<TFAService>()!));
        services.AddSingleton<DeleteUserHardwareInteractor>(s => new DeleteUserHardwareInteractor(s.GetService<HardwareService>()!));
        services.AddSingleton<FetchGamesInteractor>(s => new FetchGamesInteractor(s.GetService<GameService>()!));

        services.AddSingleton<MainViewModel>();
        services.AddTransient<LoginPageViewModel>();
        services.AddTransient<TFAPageViewModel>();
        services.AddTransient<RegisterPageViewModel>();
        services.AddTransient<AdminsPageViewModel>();
        services.AddTransient<UsersPageViewModel>();
        services.AddTransient<ReportsPageViewModel>();
        services.AddTransient<GamesPageViewModel>();
        services.AddTransient<StatisticsPageViewModel>();
        services.AddTransient<LogsPageViewModel>();
        services.AddTransient<SettingsPageViewModel>();

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