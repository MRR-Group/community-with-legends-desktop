using System.Threading.Tasks;
using Application.UseCases;
using Avalonia.SimpleRouter;
using Domain.Entities;


namespace Presentation.ViewModels;

public partial class LogsPageViewModel : DataPageViewModel<User>
{
    public LogsPageViewModel(
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