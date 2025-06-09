using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Domain.Enums;
using Domain.Primitives;

namespace Presentation.Controls;

public partial class ReportableViewer : UserControl
{
    public static readonly StyledProperty<Reportable> ReportableProperty =
        AvaloniaProperty.Register<AssetViewer, Reportable>(nameof(AssetType));
    
    public ReportableViewer()
    {
        InitializeComponent();
    }
    
    public Reportable Reportable
    {
        get => this.GetValue(ReportableProperty);
        set => this.SetValue(ReportableProperty, value);
    }
    
    public bool IsPost
    {
        get => Reportable is Domain.Entities.Post;
    }
    
    public bool IsComment
    {
        get => Reportable is Domain.Entities.Comment;
    }
    
    public bool IsUserProfile
    {
        get => Reportable is Domain.Entities.UserProfile;
    }
    
    public Domain.Entities.Post Post
    {
        get => Reportable as Domain.Entities.Post;
    }
    
    public Domain.Entities.UserProfile UserProfile
    {
        get => Reportable as Domain.Entities.UserProfile;
    }
    
    public string[] Tags
    {
        get => (Post?.Tags ?? []).Select(tag => tag.Name).ToArray();
    }
}

