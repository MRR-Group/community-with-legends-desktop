using Domain.Primitives;
using Domain.ValueObjects;
namespace Domain.Entities;

public class Post : Reportable
{
    public Game? Game { get; private set; }
    public Tag[] Tags { get; set; }
    public Asset? Asset { get; private set; }
    public int Reactions { get; private set; }
    public Comment[] Comments { get; private set; }

    public Post(uint id, string content, Date creationDate, User user, Game? game, Tag[] tags, Asset? asset, int reactions, Comment[] comments) : base(id, creationDate, user, content)
    {
        Game = game;
        Tags = tags;
        Asset = asset;
        Reactions = reactions;
        Comments = comments;
    }
}
