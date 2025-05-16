using System;
using Avalonia.Controls;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using Presentation.Views;
using Ursa.Controls;

namespace Presentation.ViewModels;

public class ViewModelBase : ObservableObject
{
    protected HistoryRouter<ViewModelBase> _router;
    private WindowNotificationManager? _notificationManager;
    
    public void NavigateTo(Type page)
    {
        var goTo = typeof(HistoryRouter<ViewModelBase>)
            .GetMethod(nameof(_router.GoTo))!
            .MakeGenericMethod(page);
        
        goTo.Invoke(_router, null);
    }

    public ViewModelBase(HistoryRouter<ViewModelBase> router)
    {
        _router = router;
    }

    protected void ShowNotification(string title, string message)
    {
        _notificationManager?.Show(new Notification(title, message));
    }

    public void LoadNotificationForView(ViewBase view)
    {
        var topLevel = TopLevel.GetTopLevel(view);

        if (WindowNotificationManager.TryGetNotificationManager(topLevel, out var manager))
        {
            _notificationManager = manager;
        }
        else
        {
            _notificationManager = new WindowNotificationManager(topLevel);
        }
    }
}
