using Avalonia;
using Avalonia.Controls;

namespace CommunityWithLegends.Controls;

public partial class FormInput : UserControl
{
    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<FormInput, string>(
        nameof(Title));

    public static readonly StyledProperty<string> WatermarkProperty = AvaloniaProperty.Register<FormInput, string>(
        nameof(Watermark));

    public static readonly StyledProperty<string> UnderTextProperty = AvaloniaProperty.Register<FormInput, string>(
        nameof(UnderText));

    public static readonly StyledProperty<string> LinkTextProperty = AvaloniaProperty.Register<FormInput, string>(
        nameof(LinkText));

    public static readonly StyledProperty<bool> PasswordProperty = AvaloniaProperty.Register<FormInput, bool>(
        nameof(Password));

    public FormInput() => this.InitializeComponent();

    public bool Password
    {
        get => this.GetValue(PasswordProperty);
        set => this.SetValue(PasswordProperty, value);
    }

    public string LinkText
    {
        get => this.GetValue(LinkTextProperty);
        set => this.SetValue(LinkTextProperty, value);
    }

    public string UnderText
    {
        get => this.GetValue(UnderTextProperty);
        set => this.SetValue(UnderTextProperty, value);
    }

    public string Watermark
    {
        get => this.GetValue(WatermarkProperty);
        set => this.SetValue(WatermarkProperty, value);
    }

    public string Title
    {
        get => this.GetValue(TitleProperty);
        set => this.SetValue(TitleProperty, value);
    }
}
