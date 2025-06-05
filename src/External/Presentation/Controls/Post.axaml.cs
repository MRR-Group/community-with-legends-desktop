using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Imaging;
using Domain.Entities;
using Presentation.Utils;
using System.Reactive.Linq;
using Domain.Enums;

namespace Presentation.Controls;

public partial class Post : UserControl
{
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<Post, string>(nameof(Text));

    public static readonly StyledProperty<string> CreationDateProperty =
        AvaloniaProperty.Register<Post, string>(nameof(CreationDate));
    
    public static readonly StyledProperty<User> UserProperty =
        AvaloniaProperty.Register<Post, User>(nameof(User));
    
    public static readonly StyledProperty<string[]> TagsProperty =
        AvaloniaProperty.Register<Post, string[]>(nameof(Tags));
    
    public static readonly StyledProperty<int> LikesProperty =
        AvaloniaProperty.Register<Post, int>(nameof(Likes));
    
    public static readonly StyledProperty<Asset?> AssetProperty =
        AvaloniaProperty.Register<Post, Asset?>(nameof(Asset));
    
    public Post()
    {
        InitializeComponent();
    }
    
    public string Text
    {
        get => this.GetValue(TextProperty);
        set => this.SetValue(TextProperty, value);
    }
    
    public string CreationDate
    {
        get => this.GetValue(CreationDateProperty);
        set => this.SetValue(CreationDateProperty, value);
    }
    
    public User User
    {
        get => this.GetValue(UserProperty);
        set => this.SetValue(UserProperty, value);
    }

    public Task<Bitmap?> Avatar
    {
        get => ImageHelper.LoadFromWeb(GetValue(UserProperty).Avatar);
    }
    
    public bool ContainsAsset
    {
        get => Asset != null;
    }
    
    public string[] Tags
    {
        get => this.GetValue(TagsProperty);
        set => this.SetValue(TagsProperty, value);
    }
    
    public string Likes
    {
        get => $"x {this.GetValue(LikesProperty)}";
        set => this.SetValue(LikesProperty, value);
    }

    public Asset? Asset
    {
        get => this.GetValue(AssetProperty);
        set => this.SetValue(AssetProperty, value);
    }
}

