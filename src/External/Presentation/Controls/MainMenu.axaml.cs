using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
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
                Text = "Admins", 
                Link = typeof(AdminsPageViewModel),
                Icon = "/Assets/Icons/Admin.svg",
            },
            new MainMenuItem 
            {
                Text = "Users",
                Link = typeof(UsersPageViewModel),
                Icon = "/Assets/Icons/User.svg",
            },
            new MainMenuItem 
            {
                Text = "Reports",
                Link = typeof(AdminsPageViewModel),
                Icon = "/Assets/Icons/Flag.svg",
            },
            new MainMenuItem 
            {
                Text = "Game list",
                Link = typeof(AdminsPageViewModel),
                Icon = "/Assets/Icons/Gamepad.svg",
            },
            new MainMenuItem 
            {
                Text = "Statistics",
                Link = typeof(AdminsPageViewModel),
                Icon = "/Assets/Icons/Chart.svg",
            },
            new MainMenuItem 
            {
                Text = "Logs",
                Link = typeof(AdminsPageViewModel),
                Icon = "/Assets/Icons/File.svg",
            },
            new MainMenuItem 
            {
                Text = "Settings",
                Link = typeof(AdminsPageViewModel),
                Icon = "/Assets/Icons/Gear.svg",
            },
        ];
        
        InitializeComponent();
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
                item.IsSelected = item.Text == value;
            }
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

