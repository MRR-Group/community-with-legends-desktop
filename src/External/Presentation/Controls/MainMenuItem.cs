using System;
using System.Globalization;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using Echoes;
using Presentation.ViewModels;
namespace Presentation.Controls;

public record MainMenuItem
{
    public string Id { get; set; }
    public string Icon { get; set; }

    public bool IsSelected { get; set; }
    public Type Link { get; set; }
    
    public String Text
    {
        get => TranslationProvider.ReadTranslation(
            assembly: typeof(Strings).Assembly,
            file: "Translations\\Strings.toml",
            key: $"{Id}_page_title",
            culture: TranslationProvider.Culture
        ) ?? Id;
    }

    public string IconCss
    {
        get => IsSelected ? "path { stroke: #8E2CFE; }" : "path { stroke: #FDFEFE; }";
    }
}