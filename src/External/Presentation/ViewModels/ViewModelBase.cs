using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.ViewModels;

public class ViewModelBase : ObservableObject
{
    protected HistoryRouter<ViewModelBase> _router;

    public ViewModelBase(HistoryRouter<ViewModelBase> router)
    {
        _router = router;
    }
}
