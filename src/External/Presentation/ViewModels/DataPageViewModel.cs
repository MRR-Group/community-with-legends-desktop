using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.UseCases;
using Avalonia.SimpleRouter;
using Domain.Primitives;
using Infrastructure.DTOs;
using Infrastructure.Repositories;

namespace Presentation.ViewModels;

public abstract class DataPageViewModel<T> : AuthenticatedPageViewModel where T : Entity
{
    public ObservableCollection<T> Data { get; protected set;  }
    protected IRepository<T> _repository;
    
    public DataPageViewModel(
        HistoryRouter<ViewModelBase> router, 
        IRepository<T> repository,
        LogOutInteractor logOutInteractor
    ) : base(router, logOutInteractor)
    {
        _repository = repository;
        Data = [];

        RefreshData();
    }
    
    protected Task RefreshData()
    { 
        return SendAction(async () => ReloadData(await _repository.All()));
    }
    
    protected Task RefreshItem(T item)
    {
        return SendAction(async () =>
        {
            var data = await _repository.ById(item.Id);
            RemoveItem(item);
            AddItem(data);
        });
    }

    protected void ReloadData(T[] data)
    {
        var existingIds = Data.Select(item => item.Id).ToHashSet();

        foreach (var item in data)
        {
            if (!existingIds.Contains(item.Id))
            {
                Data.Add(item);
            }
        }
    }
    
    protected void AddItem(T item)
    { 
        Data.Add(item);
    }
    
    protected void RemoveItem(T item)
    {
        Data.Remove(item);
    }
    
    protected async Task SendAction(T? target, Func<T, Task> sendAction)
    {
        if (target != null)
        {
            await SendAction(async () => await sendAction.Invoke(target));
        }
    }
}