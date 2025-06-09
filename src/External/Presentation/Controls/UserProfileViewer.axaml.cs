using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Domain.Entities;
using Presentation.Utils;

namespace Presentation.Controls;

public partial class UserProfileViewer : UserControl
{
    public static readonly StyledProperty<string> CreationDateProperty =
        AvaloniaProperty.Register<UserProfileViewer, string>(nameof(CreationDate));
    
    public static readonly StyledProperty<User> UserProperty =
        AvaloniaProperty.Register<UserProfileViewer, User>(nameof(User));
    
    public static readonly StyledProperty<Hardware[]> HardwareListProperty =
        AvaloniaProperty.Register<UserProfileViewer, Hardware[]>(nameof(User));
    
    public UserProfileViewer()
    {
        InitializeComponent();
    }
    
    public string CreationDate
    {
        get => this.GetValue(CreationDateProperty);
        set => this.SetValue(CreationDateProperty, value);
    }
    
    public Hardware[] HardwareList
    {
        get => this.GetValue(HardwareListProperty);
        set => this.SetValue(HardwareListProperty, value);
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

