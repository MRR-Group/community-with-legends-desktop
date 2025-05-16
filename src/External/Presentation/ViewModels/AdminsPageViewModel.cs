using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using Presentation.Controls;
using Ursa.Controls;

namespace Presentation.ViewModels;

public partial class AdminsPageViewModel : ViewModelBase
{
    public string Test { get; set; } = "Admins ";

    public AdminsPageViewModel(HistoryRouter<ViewModelBase> router) : base(router)
    {
        
    }
    
    [RelayCommand]
    private void HandleMenuClick(MainMenuItem item)
    {
        NavigateTo(item.Link);
    }
}