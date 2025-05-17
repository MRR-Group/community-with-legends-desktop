using System.Threading.Tasks;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using Infrastructure.Repositories;
using Presentation.Controls;
using Ursa.Controls;

namespace Presentation.ViewModels;

public partial class AdminsPageViewModel : DataPageViewModel<User>
{
    private UserRepository _userRepository;

    public AdminsPageViewModel(
        HistoryRouter<ViewModelBase> router,
        LogOutInteractor logOutInteractor,
        UserRepository userRepository
    ) : base(router, logOutInteractor)
    {
        _userRepository = userRepository;
        RefreshData();
    }
    
    protected override async Task RefreshData()
    {
        var data = await _userRepository.All();
        
        Data.Clear();
        
        foreach (var user in data)
        {
            Data.Add(user);
        }
    }
}