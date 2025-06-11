using System;
using System.Reactive.Disposables;
namespace Presentation.Utils;

public static class DisposableExtensions
{
    public static void DisposeWith(this IDisposable disposable, CompositeDisposable composite)
    {
        composite.Add(disposable);
    }
}
