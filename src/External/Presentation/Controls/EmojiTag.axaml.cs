using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Presentation.Controls;

public partial class EmojiTag : UserControl
{
    public static readonly StyledProperty<string> EmojiProperty =
        AvaloniaProperty.Register<EmojiTag, string>(nameof(Emoji));
    
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<EmojiTag, string>(nameof(Text));
    
    public EmojiTag()
    {
        InitializeComponent();
    }

    public string Emoji
    {
        get => this.GetValue(EmojiProperty);
        set => this.SetValue(EmojiProperty, value);
    }
    
    public string Text
    {
        get => this.GetValue(TextProperty);
        set => this.SetValue(TextProperty, value);
    }
}

