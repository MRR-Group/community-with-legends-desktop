using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CommunityWithLegends.ViewModels;

namespace CommunityWithLegends;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? viewModel)
    {
        if (viewModel is null)
        {
            return null;
        }

        var name = viewModel.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type == null)
        {
            return new TextBlock { Text = "Not Found: " + name };
        }

        var control = (Control)Activator.CreateInstance(type)!;
        control.DataContext = viewModel;

        return control;
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
