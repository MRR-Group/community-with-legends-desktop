using System;
using System.Linq;
using System.Threading.Tasks;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;

namespace Presentation.ViewModels;

public partial class GamesPageViewModel : DataPageViewModel<Game>
{
    protected GameRepository _gameRepository;
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
    ) : base(router, logOutInteractor)
    {
        _gameRepository = gameRepository;
        _gameService = gameService;
        _getFetchGamesInteractor = fetchGamesInteractor;
        
        RefreshData();
        UpdateProgress();
    }
    
    protected override async Task RefreshData()
    {
        var data = await _gameRepository.All();
        var existingIds = Data.Select(g => g.Id).ToHashSet();

        foreach (var game in data)
        {
            if (!existingIds.Contains(game.Id))
            {
                Data.Add(game);
            }
        }
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