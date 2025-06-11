using Domain.Enums;
using Domain.ValueObjects;

namespace Tests;

public class RolesTests
{
    [Fact]
    public void Constructor_ShouldSetValue()
    {
        var roles = new Roles([Role.Administrator, Role.Moderator]);
        Assert.Contains(Role.Administrator, roles.Value);
        Assert.Contains(Role.Moderator, roles.Value);
    }

    [Fact]
    public void Is_ShouldReturnTrue_WhenAllRolesPresent()
    {
        var roles = new Roles([Role.Administrator, Role.Moderator]);
        Assert.True(roles.Is(Role.Administrator));
        Assert.True(roles.Is(Role.Administrator, Role.Moderator));
    }

    [Fact]
    public void Is_ShouldReturnFalse_WhenNotAllRolesPresent()
    {
        var roles = new Roles([Role.Administrator]);
        Assert.False(roles.Is(Role.Administrator, Role.Moderator));
    }

    [Fact]
    public void IsNot_ShouldReturnTrue_WhenNoneOfRolesPresent()
    {
        var roles = new Roles([Role.Administrator]);
        Assert.True(roles.IsNot(Role.Moderator));
        Assert.True(roles.IsNot(Role.Moderator, Role.User));
    }

    [Fact]
    public void IsNot_ShouldReturnFalse_WhenAnyRolePresent()
    {
        var roles = new Roles([Role.Administrator, Role.Moderator]);
        Assert.False(roles.IsNot(Role.Moderator));
        Assert.False(roles.IsNot(Role.Administrator, Role.User));
    }

    [Fact]
    public void ImplicitConversion_ToRoleArray_Works()
    {
        var roles = new Roles([Role.Administrator]);
        Role[] array = roles;
        Assert.Contains(Role.Administrator, array);
    }

    [Fact]
    public void ImplicitConversion_FromRoleArray_Works()
    {
        Role[] array = [Role.Administrator];
        Roles roles = array;
        Assert.Contains(Role.Administrator, roles.Value);
    }
}
