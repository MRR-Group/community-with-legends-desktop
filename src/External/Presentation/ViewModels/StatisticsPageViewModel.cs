using System.Threading.Tasks;
using Application.UseCases;
using Avalonia.SimpleRouter;
using Domain.Entities;

namespace Presentation.ViewModels;

public partial class StatisticsPageViewModel : AuthenticatedPageViewModel
{
    public StatisticsPageViewModel(HistoryRouter<ViewModelBase> router, LogOutInteractor logOutInteractor) : base(router, logOutInteractor)
    {
    }
}