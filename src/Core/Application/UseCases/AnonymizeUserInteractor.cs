using Application.Abstractions;
using Domain.Entities;

namespace Application.UseCases;

public class AnonymizeUserInteractor
{
    private IAnonymizeService _anonymizeService;

    public AnonymizeUserInteractor(IAnonymizeService anonymizeService)
    {
        _anonymizeService = anonymizeService;
    }

    public async Task<bool> Anonymize(User user)
    {
        return await _anonymizeService.Anonymize(user);
    }
}