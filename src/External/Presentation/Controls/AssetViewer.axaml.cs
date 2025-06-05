using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Domain.Enums;
using Presentation.Utils;

namespace Presentation.Controls;

public partial class AssetViewer : UserControl
{
    public static readonly StyledProperty<AssetType> AssetTypeProperty =
        AvaloniaProperty.Register<AssetViewer, AssetType>(nameof(AssetType));
    
    public static readonly StyledProperty<Uri> SourceProperty =
        AvaloniaProperty.Register<AssetViewer, Uri>(nameof(Source));

    public AssetViewer()
    {
        InitializeComponent();
    }
    
    public AssetType AssetType
    {
        get => this.GetValue(AssetTypeProperty);
        set => this.SetValue(AssetTypeProperty, value);
    }

    public bool IsVideo
    {
        get => AssetType == AssetType.Video;
    }

    public Uri Source
    {
        set => this.SetValue(SourceProperty, value);
        get => this.GetValue(SourceProperty);
    }
    
    public Task<Bitmap?> Image
    {
        get
        {
            var url = Source;            
            
            if (IsVideo)
            {
                url = YouTubeHelper.GetYTPreview(url);
            }

            return ImageHelper.LoadFromWeb(url);
        }
    }
    
    void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (!IsVideo)
        {
            return;
        }

        var launcher = TopLevel.GetTopLevel(this)?.Launcher;
        launcher?.LaunchUriAsync(Source);
    }
}

