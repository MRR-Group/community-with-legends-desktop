using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Infrastructure.DTOs;

public record CommentDto : Dto<Comment>
{
    [property: JsonPropertyName("id")]
    public uint Id { get; set; }
    
    [property: JsonPropertyName("content")]
    public string Content { get; set; }
    
    [property: JsonPropertyName("created_at")]
    public string CreationDate { get; set; }
    
    [property: JsonPropertyName("user")]
    public UserDto User { get; set; }

    public override Comment ToEntity()
    {
        return new Comment(Id, Content, new Date(CreationDate), User.ToEntity());
    }
}