    using System.Threading.Tasks;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using Infrastructure.Repositories;
using Presentation.Controls;
using Ursa.Controls;

namespace Presentation.ViewModels;

public partial class ReportsPageViewModel : DataPageViewModel<User>
{
    public ReportsPageViewModel(
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