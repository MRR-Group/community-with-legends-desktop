using Application.Abstractions;
using Application.Exceptions;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases;

public class FetchGamesInteractor
{
    private IGameService _gameService;

    public FetchGamesInteractor(IGameService gameService)
    {
        _gameService = gameService;
    }

    public async Task<int> Fetch()
    {
        return await _gameService.Fetch();
    }
}