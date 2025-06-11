using System.Text.Json.Serialization;

namespace Infrastructure.DTOs;

public record Statistics
{
    [property: JsonPropertyName("posts")]
    public int Posts { get; set; }
    
    [property: JsonPropertyName("comments")]
    public int Comments { get; set; }
    
    [property: JsonPropertyName("users")]
    public int Users { get; set; }
    
    [property: JsonPropertyName("user_growth_all_time")]
    public Dictionary<string, int> UsersGrowth { get; set; }
    
    [property: JsonPropertyName("posts_growth_all_time")]
    public Dictionary<string, int> PostsGrowth { get; set; }
    
    [property: JsonPropertyName("comments_growth_all_time")]
    public Dictionary<string, int> CommentsGrowth { get; set; }

    [property: JsonPropertyName("disk_usage_percentage")]
    public int DiskUsage { get; set; }
    
    [property: JsonPropertyName("banned_users")]
    public int BannedUsers { get; set; }
    
    [property: JsonPropertyName("logs")]
    public int Logs { get; set; }
}
