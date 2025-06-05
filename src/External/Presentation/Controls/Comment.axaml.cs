using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Imaging;
using Domain.Entities;
using Presentation.Utils;
using System.Reactive.Linq;

namespace Presentation.Controls;

public partial class Comment : UserControl
{
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<Comment, string>(nameof(Text));

    public static readonly StyledProperty<string> CreationDateProperty =
        AvaloniaProperty.Register<Comment, string>(nameof(CreationDate));
    
    public static readonly StyledProperty<User> UserProperty =
        AvaloniaProperty.Register<Comment, User>(nameof(User));
    
    public Comment()
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
}

