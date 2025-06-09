using Domain.Enums;
using Domain.Primitives;
namespace Domain.Entities;

public class Asset : Entity
{
    public AssetType Type { get; private set; }
    public Uri Link { get; private set; }

    public Asset(uint id, AssetType type, Uri link) : base(id)
    {
        Type = type;
        Link = link;
    }
}
