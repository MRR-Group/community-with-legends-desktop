using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using Presentation.ViewModels;
namespace Presentation.Controls;

public record MainMenuItem
{
    public string Text { get; set; }
    public string Icon { get; set; }

    public bool IsSelected { get; set; }

    public Type Link { get; set; }

    public string IconCss
    {
        get => IsSelected ? "path { stroke: #8E2CFE; }" : "path { stroke: #FDFEFE; }";
    }
}