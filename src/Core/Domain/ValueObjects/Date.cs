namespace Domain.ValueObjects;

public record Date
{
    private DateTime _date;
    
    public Date(string isoDate)
    {
        _date = DateTime.Parse(isoDate, null, System.Globalization.DateTimeStyles.AdjustToUniversal);
    }
    
    public override string ToString()
    {
        return _date.ToString("yyyy/MM/dd HH:mm");
    }
}
