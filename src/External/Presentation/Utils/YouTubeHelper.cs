using System;
using System.Linq;
using System.Web;
using Presentation.Exceptions;
namespace Presentation.Utils;

public static class YouTubeHelper
{
    public static string GetYouTubeVideoId(Uri uri)
    {
        if (uri == null)
        {
            throw new IncorrectYoutubeUrlException();
        }

        if (IsYouTuBeShortUrl(uri))
        {
            return GetIdFromShortUrl(uri);
        }

        if (IsWatchUrl(uri))
        {
            return GetIdFromWatchUrl(uri);
        }

        if (IsEmbedUrl(uri))
        {
            return GetIdFromEmbedUrl(uri);
        }

        throw new IncorrectYoutubeUrlException();
    }

    private static bool IsYouTuBeShortUrl(Uri uri)
    {
        return uri.Host.Contains("youtu.be");
    }

    private static string GetIdFromShortUrl(Uri uri)
    {
        return uri.Segments.Last().TrimEnd('/');
    }

    private static bool IsWatchUrl(Uri uri)
    {
        return uri.Host.Contains("youtube.com") && uri.AbsolutePath.Equals("/watch", StringComparison.OrdinalIgnoreCase);
    }

    private static string GetIdFromWatchUrl(Uri uri)
    {
        var query = HttpUtility.ParseQueryString(uri.Query);
        var value = query["v"];        
        
        if (value == null)
        {
            throw new IncorrectYoutubeUrlException();
        }

        return value;
    }

    private static bool IsEmbedUrl(Uri uri)
    {
        return uri.Host.Contains("youtube.com") && uri.AbsolutePath.StartsWith("/embed/", StringComparison.OrdinalIgnoreCase);
    }
    
    private static string GetIdFromEmbedUrl(Uri uri)
    {
        const int embedIdSegmentIndex = 2;

        if (uri.Segments.Length <= embedIdSegmentIndex)
        {
            throw new IncorrectYoutubeUrlException();
        }
        
        var videoId = uri.Segments[embedIdSegmentIndex].TrimEnd('/');

        if (string.IsNullOrEmpty(videoId))
        {
            throw new IncorrectYoutubeUrlException();
        }

        return videoId;
    }
    
    public static Uri GetYTPreview(Uri link)
    {
        try
        {
            var id = GetYouTubeVideoId(link);
            
            return new Uri($"https://i.ytimg.com/vi/{id}/sddefault.jpg");
        }
        catch (IncorrectYoutubeUrlException _)
        {
            return new Uri("https://w7.pngwing.com/pngs/164/715/png-transparent-youtube-copyright-face-thumbnail.png");
        }
    }
}
