using Domain.Exceptions;
using Domain.ValueObjects;
using Xunit;

namespace Tests.ValueObjects;

public class EmailTests
{
    [Fact]
    public void Constructor_ShouldSetValue_WhenValidEmail()
    {
        var email = new Email("test@example.com");
        Assert.Equal("test@example.com", email.Value);
    }

    [Theory]
    [InlineData("invalidemail.com")]
    [InlineData("invalidemail")]
    [InlineData("ia")]
    [InlineData("")]
    public void Constructor_ShouldThrowInvalidEmailException_WhenEmailIsInvalid(string invalidEmail)
    {
        Assert.Throws<InvalidEmailException>(() => new Email(invalidEmail));
    }

    [Fact]
    public void ImplicitConversion_ToString_ReturnsEmailValue()
    {
        var email = new Email("test@example.com");
        string str = email;
        Assert.Equal("test@example.com", str);
    }

    [Fact]
    public void ImplicitConversion_FromString_CreatesEmail()
    {
        Email email = "user@example.com";
        Assert.Equal("user@example.com", email.Value);
    }
}
