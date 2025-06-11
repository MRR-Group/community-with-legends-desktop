using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.UseCases;
using Avalonia.SimpleRouter;
using Domain.Entities;
using Infrastructure.DTOs;
using Infrastructure.Repositories;

namespace Presentation.ViewModels;

public partial class LogsPageViewModel : DataPageViewModel<Log>
{
    public LogsPageViewModel(
        HistoryRouter<ViewModelBase> router,
        LogsRepository logsRepository,
        LogOutInteractor logOutInteractor
    ) : base(router, logsRepository, logOutInteractor)
    {
    }
}