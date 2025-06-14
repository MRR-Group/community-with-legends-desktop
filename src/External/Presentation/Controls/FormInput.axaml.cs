using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;

namespace Presentation.Controls;

public partial class FormInput : UserControl
{
    public static readonly StyledProperty<string> TitleProperty = 
        AvaloniaProperty.Register<FormInput, string>(nameof(Title));

    public static readonly StyledProperty<string> WatermarkProperty = 
        AvaloniaProperty.Register<FormInput, string>(nameof(Watermark));

    public static readonly StyledProperty<string> UnderTextProperty = 
        AvaloniaProperty.Register<FormInput, string>(nameof(UnderText));

    public static readonly StyledProperty<string> LinkTextProperty = 
        AvaloniaProperty.Register<FormInput, string>(nameof(LinkText));

    public static readonly StyledProperty<bool> PasswordProperty = 
        AvaloniaProperty.Register<FormInput, bool>(nameof(Password));

    public static readonly StyledProperty<bool> HideUnderTextProperty = 
        AvaloniaProperty.Register<FormInput, bool>(nameof(Password));
    
    public static readonly StyledProperty<string> TextProperty = 
        AvaloniaProperty.Register<FormInput, string>(nameof(Text));
    
    public static readonly StyledProperty<string?> ErrorProperty =
        AvaloniaProperty.Register<FormInput, string?>(nameof(Error));
        
    public static readonly RoutedEvent<RoutedEventArgs> LinkClickEvent = 
        RoutedEvent.Register<FormInput, RoutedEventArgs>(nameof(LinkClick), RoutingStrategies.Direct);
    
    public FormInput()
    { 
        this.InitializeComponent();
    }

    public event EventHandler<RoutedEventArgs> LinkClick
    {
        add => this.AddHandler(LinkClickEvent, value);
        remove => this.RemoveHandler(LinkClickEvent, value);
    }
    
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

    public bool HideUnderText
    {
        get => this.GetValue(HideUnderTextProperty);
        set => this.SetValue(HideUnderTextProperty, value);
    }
    
    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string? Error
    {
        get => GetValue(ErrorProperty);
        set => SetValue(ErrorProperty, value);
    }
    
    private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        RoutedEventArgs args = new RoutedEventArgs(LinkClickEvent);
        this.RaiseEvent(args);
    }
    
    public bool ShouldDisplayUnderText
    {
        get => Error is null && !HideUnderText;
    }
}
