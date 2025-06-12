using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Infrastructure.DTOs;

public record PostDto : Dto<Post>
{
    
    [property: JsonPropertyName("id")]
    public uint Id { get; set; }
    
    [property: JsonPropertyName("content")]
    public string Content { get; set; }
    
    [property: JsonPropertyName("created_at")]
    public string CreationDate { get; set; }
    
    [property: JsonPropertyName("user")]
    public UserDto User { get; set; }
    
    [property: JsonPropertyName("game")]
    public GameDto? Game { get; set; }
    
    [property: JsonPropertyName("tags")]
    public TagDto[] Tags { get; set; } = [];
    
    [property: JsonPropertyName("asset")]
    public AssetDto? Asset { get; set; }
    
    [property: JsonPropertyName("reactions")]
    public int Reactions { get; set; }
    
    [property: JsonPropertyName("comments")]
    public CommentDto[] Comments { get; set; } = [];

    public override Post ToEntity()
    {
        var tags = Tags.Select(tag => tag.ToEntity()).ToArray();
        var comments = Comments.Select(comment =>
        {
            var c = comment;
            
            return comment.ToEntity();
        }).ToArray();
        
        return new Post(
            Id, 
            Content, 
            new Date(CreationDate), 
            User.ToEntity(), 
            Game?.ToEntity(), 
            tags, 
            Asset?.ToEntity(), 
            Reactions, 
            comments
        );
    }
}