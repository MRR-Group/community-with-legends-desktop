namespace Domain.ValueObjects;

public record Date
{
    private DateTime _date;
    
    public Date(uint timestamp)
    {
        _date = DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
    }
    
    public override string ToString()
    {
        return _date.ToString("yyyy/MM/dd HH:mm");
    }
}
