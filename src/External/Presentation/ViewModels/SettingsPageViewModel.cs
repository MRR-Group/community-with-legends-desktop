using System.Threading.Tasks;
using Application.UseCases;
using Avalonia.SimpleRouter;
using Domain.Entities;

namespace Presentation.ViewModels;

public partial class SettingsPageViewModel : DataPageViewModel<User>
{
    public SettingsPageViewModel(
        HistoryRouter<ViewModelBase> router,
        LogOutInteractor logOutInteractor
    ) : base(router, logOutInteractor)
    {
        RefreshData();
    }
    
    protected override async Task RefreshData()
    {
    }
}