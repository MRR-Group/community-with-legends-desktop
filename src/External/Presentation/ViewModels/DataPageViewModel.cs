using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using Domain.Primitives;
using Infrastructure.DTOs;
using Infrastructure.Repositories;

namespace Presentation.ViewModels;

public abstract partial class DataPageViewModel<T> : AuthenticatedPageViewModel where T : Entity
{
    public ObservableCollection<T> Data { get; protected set;  }
    protected IRepository<T> _repository;
    protected int _page = 1;
    protected bool _lastPage = false;
    protected bool _isLoading = false;

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
    
    protected async Task RefreshData()
    {
        try
        {
            await SendAction(async () => ReloadData(await fetchData(_page)));
        }
        catch (Exception e)
        {
            var ee = e;
        }
    }

    [RelayCommand]
    public void HandleScroll()
    { 
        InfinityScroll();
    }

    protected async Task InfinityScroll()
    {
        if (_isLoading || _lastPage)
        {
            return;
        }

        _isLoading = true;

        try
        {
            _page += 1;

            var data = await fetchData(_page);

            if (data.Length == 0)
            {
                _lastPage = true;
                return;
            }

            ReloadData(data);
        }
        finally
        {
            _isLoading = false;
        }
    }

    protected async Task<T[]> fetchData(int page = 1)
    {
        return await _repository.All(_page);
    }

    protected Task RefreshItem(T item)
    {
        return SendAction(async () =>
        {
            var data = await _repository.ById(item.Id);
            var index = Data.IndexOf(item);

            if (index >= 0) 
            {
                Data[index] = data;
            }
            else
            {
                AddItem(data);
            }
        });
    }
    
    protected void ReloadData(T[] data)
    {
        var existingIds = Data.Select(item => item.Id).ToHashSet();

        foreach (var item in data)
        {
            if (!existingIds.Contains(item.Id))
            {
                AddItem(item);
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