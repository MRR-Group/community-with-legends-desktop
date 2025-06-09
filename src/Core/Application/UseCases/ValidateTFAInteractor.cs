using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.UseCases;

public class ValidateTFAInteractor
{
    private ITFAService _tfaService;

    public ValidateTFAInteractor(ITFAService tfaService)
    {
        _tfaService = tfaService;
    }

    public async Task Validate(string token)
    { 
        await _tfaService.Validate(token);
    }
}