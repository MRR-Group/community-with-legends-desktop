using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using Presentation.Controls;
using Presentation.ViewModels;

namespace Presentation.Views;

public partial class UsersPageView : ViewBase
{
    public UsersPageView()
    {
        InitializeComponent();
    }
}