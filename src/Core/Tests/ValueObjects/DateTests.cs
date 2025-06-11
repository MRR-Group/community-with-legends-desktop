using System;
using Domain.ValueObjects;
using Xunit;

namespace Tests.ValueObjects;

public class DateTests
{
    [Fact]
    public void Constructor_ShouldParseIsoDateString_Correctly()
    {
        var isoString = "2025-06-11T10:15:28Z";

        var date = new Date(isoString);

        Assert.Equal("2025.06.11 10:15", date.ToString());
    }

    [Fact]
    public void ToString_ShouldReturnFormattedString()
    {
        var isoString = "2023-12-01T23:45:00Z";

        var date = new Date(isoString);

        Assert.Equal("2023.12.01 23:45", date.ToString());
    }

    [Fact]
    public void Constructor_ShouldThrowFormatException_ForInvalidString()
    {
        Assert.Throws<FormatException>(() => new Date("invalid-date"));
    }
}
