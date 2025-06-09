using Domain.Primitives;
namespace Domain.Entities;

public class Hardware : Entity
{
    public string Title { get; private set; }
    public string Value { get; private set; }

    public Hardware(uint id, string title, string vale) : base(id)
    {
        Title = title;
        Value = vale;
    }
}
