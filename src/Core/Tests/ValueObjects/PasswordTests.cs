using Domain.Exceptions;
using Domain.ValueObjects;
using Xunit;

namespace Tests.ValueObjects;

public class PasswordTests
{
    [Fact]
    public void Constructor_ShouldSetValue_WhenValidPassword()
    {
        var password = new Password("Abcdef12");
        Assert.Equal("Abcdef12", password.Value);
    }

    [Theory]
    [InlineData("abcdefg")] 
    [InlineData("abcdefgh")]
    [InlineData("ABCDEFGH")]
    [InlineData("1234567")] 
    public void Constructor_ShouldThrowInvalidPasswordException_WhenInvalid(string invalidPassword)
    {
        Assert.Throws<InvalidPasswordException>(() => new Password(invalidPassword));
    }

    [Fact]
    public void ImplicitConversion_ToString_ReturnsPasswordValue()
    {
        var password = new Password("Abcdef12");
        string str = password;
        Assert.Equal("Abcdef12", str);
    }

    [Fact]
    public void ImplicitConversion_FromString_CreatesPassword()
    {
        Password password = "Abcdef12";
        Assert.Equal("Abcdef12", password.Value);
    }
}
