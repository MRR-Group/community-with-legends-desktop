using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Presentation.Messages;
using Presentation.ViewModels;

namespace Presentation.Controls;

public partial class MainMenu : UserControl
{
    public ObservableCollection<MainMenuItem> Items { get; set; }
    
    public static readonly StyledProperty<RelayCommand<MainMenuItem>> ItemClickedProperty =
        AvaloniaProperty.Register<MainMenu, RelayCommand<MainMenuItem>>(nameof(ItemClicked));

    public static readonly StyledProperty<RelayCommand> LogOutProperty =
        AvaloniaProperty.Register<MainMenu, RelayCommand>(nameof(LogOut));
    
    public static readonly StyledProperty<string> SelectedProp = 
        AvaloniaProperty.Register<MainMenu, string>(nameof(Selected));
    
    public MainMenu()
    {
        Items = [
            new MainMenuItem
            {
                Id = "admin", 
                Link = typeof(AdminsPageViewModel),
                Icon = "/Assets/Icons/Admin.svg",
            },
            new MainMenuItem 
            {
                Id = "users",
                Link = typeof(UsersPageViewModel),
                Icon = "/Assets/Icons/User.svg",
            },
            new MainMenuItem 
            {
                Id = "reports",
                Link = typeof(ReportsPageViewModel),
                Icon = "/Assets/Icons/Flag.svg",
            },
            new MainMenuItem 
            {
                Id = "game",
                Link = typeof(GamesPageViewModel),
                Icon = "/Assets/Icons/Gamepad.svg",
            },
            new MainMenuItem 
            {
                Id = "statistics",
                Link = typeof(StatisticsPageViewModel),
                Icon = "/Assets/Icons/Chart.svg",
            },
            new MainMenuItem 
            {
                Id = "logs",
                Link = typeof(LogsPageViewModel),
                Icon = "/Assets/Icons/File.svg",
            },
            new MainMenuItem 
            {
                Id = "settings",
                Link = typeof(SettingsPageViewModel),
                Icon = "/Assets/Icons/Gear.svg",
            },
        ];
        
        InitializeComponent();
        
        WeakReferenceMessenger.Default.Register<LanguageChangedMessage>(this, (_, msg) =>
        {
            RefreshTranslations();
        });
    }
    
    public RelayCommand<MainMenuItem> ItemClicked
    {
        get => GetValue(ItemClickedProperty);
        set => SetValue(ItemClickedProperty, value);
    }
    
    public RelayCommand LogOut
    {
        get => GetValue(LogOutProperty);
        set => SetValue(LogOutProperty, value);
    }
    
    public string Selected
    {
        get => GetValue(SelectedProp);
        set {
            SetValue(SelectedProp, value);
            
            foreach (var item in Items)
            {
                item.IsSelected = item.Id == value;
            }
        }
    }
    
    public void RefreshTranslations()
    {
        foreach (var item in Items)
        {
            item.RefreshText();
        }
    }
    
    void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is not TextBlock button)
        {
            return;
        }
        
        if (button.DataContext is not MainMenuItem item)
        {
            return;
        }
        
        ItemClicked?.Execute(item);
    }
    
    void LogoutButton_Click(object? sender, RoutedEventArgs e)
    {
        LogOut?.Execute(null);
    }
}

