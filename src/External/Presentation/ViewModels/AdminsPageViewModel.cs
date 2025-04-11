using Avalonia.SimpleRouter;

namespace Presentation.ViewModels;

public partial class AdminsPageViewModel(HistoryRouter<ViewModelBase> router) : ViewModelBase
{
    public string Test { get; set; } = "Admins ";

    private HistoryRouter<ViewModelBase> _router = router;
}