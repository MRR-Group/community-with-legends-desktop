using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Presentation.Controls;

public partial class Tag : UserControl
{
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<Tag, string>(nameof(Text));
    
    public Tag()
    {
        InitializeComponent();
    }
    
    public string Text
    {
        get => this.GetValue(TextProperty);
        set => this.SetValue(TextProperty, value);
    }
}

