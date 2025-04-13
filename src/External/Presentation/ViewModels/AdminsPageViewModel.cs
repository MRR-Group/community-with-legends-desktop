using Avalonia.SimpleRouter;

namespace Presentation.ViewModels;

public partial class AdminsPageViewModel : ViewModelBase
{
    public string Test { get; set; } = "Admins ";

    public AdminsPageViewModel(HistoryRouter<ViewModelBase> router) : base(router)
    {
        
    }
}