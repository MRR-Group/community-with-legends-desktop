using System;
using System.Threading.Tasks;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using Infrastructure.Exceptions;
using Presentation.Events;
using Presentation.Utils;
using Ursa.Controls;

namespace Presentation.ViewModels;

public partial class FormPageViewModel : ViewModelBase
{
    [ObservableProperty]
    protected bool _processing;
    
    protected FormExceptions _exceptions = new ();

    public event EventHandler<FormExceptionEventArgs>? OnFormException;
    
    public FormPageViewModel(HistoryRouter<ViewModelBase> router) : base(router)
    {
        _processing = false;
    }

    protected virtual async Task SendForm(Func<Task> sendAction)
    {
        try
        {
            Processing = true;
            _exceptions.Clear();

            await sendAction.Invoke();
        }
        catch (FormValidationException e)
        {
            _exceptions.AddRange(e.Fields);
        }
        finally
        {
            UpdateFormEvents();
            Processing = false;
        }
    }

    protected void UpdateFormEvents()
    {
        OnFormException?.Invoke(this, new FormExceptionEventArgs { Exceptions = _exceptions.LimitToFirst() });
    }
}