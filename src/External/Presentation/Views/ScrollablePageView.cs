using System;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Domain.Primitives;
using Presentation.ViewModels;
using Presentation.Utils;

namespace Presentation.Views;

public class ScrollablePageView<TViewModel, TItem> : UserControl where TViewModel : DataPageViewModel<TItem> where TItem : Entity 
{
    protected CompositeDisposable _disposables = new();
    protected ScrollViewer ScrollHost;

    protected void InitScroll(ScrollViewer scrollViewer)
    {
        ScrollHost = scrollViewer;

        ScrollHost.GetObservable(ScrollViewer.OffsetProperty)
            .Subscribe(offset =>
            {
                double scrollY = offset.Y;
                double maxScroll = ScrollHost.Extent.Height - ScrollHost.Viewport.Height;
                double reloadPoint = maxScroll * 0.9;
                
                if (scrollY >= reloadPoint)
                {
                    if (DataContext is TViewModel viewModel)
                    {
                        viewModel.HandleScrollCommand.Execute(null);
                    }
                }
            })
            .DisposeWith(_disposables);
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        _disposables.Dispose();
    }
}
