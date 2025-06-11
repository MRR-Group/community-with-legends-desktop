using System;
using System.Linq;
using System.Threading.Tasks;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using Infrastructure.DTOs;
using Infrastructure.Repositories;
using Infrastructure.Services;

namespace Presentation.ViewModels;

public partial class GamesPageViewModel : DataPageViewModel<Game, GameDto>
{
    protected GameService _gameService;
    protected FetchGamesInteractor _getFetchGamesInteractor;

    [ObservableProperty] 
    private int _progress = 0;
    
    [ObservableProperty] 
    private bool _isFetching = true;

    public GamesPageViewModel(
        HistoryRouter<ViewModelBase> router,
        GameRepository gameRepository,
        GameService gameService,
        FetchGamesInteractor fetchGamesInteractor,
        LogOutInteractor logOutInteractor
    ) : base(router, gameRepository, logOutInteractor)
    {
        _gameService = gameService;
        _getFetchGamesInteractor = fetchGamesInteractor;
        
        UpdateProgress();
    }
    
    [RelayCommand]
    public void FetchGames()
    {
        SendAction(async () =>
        {
            IsFetching = true;
            Progress = 0;
            
            await _getFetchGamesInteractor.Fetch();
            
            await UpdateProgress();
            await RefreshData();
        });
    }

    protected async Task UpdateProgress()
    {
        Progress = await _gameService.Progress();

        if (Progress >= 100)
        {
            IsFetching = false;
            return;
        }

        await Task.Delay(1000);
        await UpdateProgress();
    }
}