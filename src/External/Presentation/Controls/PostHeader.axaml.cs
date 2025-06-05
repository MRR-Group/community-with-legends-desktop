using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;

namespace Presentation.Controls;

public partial class PostHeader : UserControl
{
    public static readonly StyledProperty<string> UserNameProperty =
        AvaloniaProperty.Register<PostHeader, string>(nameof(UserName));

    public static readonly StyledProperty<string> CreationDateProperty =
        AvaloniaProperty.Register<PostHeader, string>(nameof(CreationDate));
    
    public static readonly StyledProperty<string> UnderTextProperty =
        AvaloniaProperty.Register<PostHeader, string>(nameof(UnderText));
    
    public static readonly StyledProperty<Task<Bitmap?>> AvatarProperty =
        AvaloniaProperty.Register<PostHeader, Task<Bitmap?>>(nameof(Avatar));
    
    public PostHeader()
    {
        InitializeComponent();
    }
    
    public string UserName
    {
        get => this.GetValue(UserNameProperty);
        set => this.SetValue(UserNameProperty, value);
    }
    
    public string CreationDate
    {
        get => this.GetValue(CreationDateProperty);
        set => this.SetValue(CreationDateProperty, value);
    }
    
    public string UnderText
    {
        get => this.GetValue(UnderTextProperty);
        set => this.SetValue(UnderTextProperty, value);
    }
    
    public Task<Bitmap?> Avatar
    {
        get => this.GetValue(AvatarProperty);
        set => this.SetValue(AvatarProperty, value);
    }

    public bool HasUnderText
    {
        get => UnderText.Length > 0;
    }
    
    public bool HasCreationDate
    {
        get => CreationDate.Length > 0;
    }
    
    public bool HasUserName
    {
        get => UserName.Length > 0;
    }
}

