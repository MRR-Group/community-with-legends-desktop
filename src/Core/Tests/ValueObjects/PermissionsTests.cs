using Domain.Enums;
using Domain.ValueObjects;

namespace Tests.ValueObjects;

public class PermissionsTests
{
    [Fact]
    public void Constructor_ShouldSetValue()
    {
        var perms = new Permissions([Permission.BanUsers, Permission.CreatePost]);
        Assert.Contains(Permission.BanUsers, perms.Value);
        Assert.Contains(Permission.CreatePost, perms.Value);
    }

    [Fact]
    public void HasNone_ShouldReturnTrue_WhenNoPermissions()
    {
        var perms = new Permissions([]);
        Assert.True(perms.HasNone());
    }

    [Fact]
    public void HasNone_ShouldReturnFalse_WhenHasPermissions()
    {
        var perms = new Permissions([Permission.BanUsers]);
        Assert.False(perms.HasNone());
    }

    [Fact]
    public void Can_ShouldReturnTrue_WhenPermissionExists()
    {
        var perms = new Permissions([Permission.BanUsers, Permission.CreatePost]);
        Assert.True(perms.Can(Permission.BanUsers));
    }

    [Fact]
    public void Can_ShouldReturnFalse_WhenPermissionDoesNotExist()
    {
        var perms = new Permissions([Permission.CreatePost]);
        Assert.False(perms.Can(Permission.BanUsers));
    }

    [Fact]
    public void Cannot_ShouldReturnTrue_WhenPermissionDoesNotExist()
    {
        var perms = new Permissions([Permission.CreatePost]);
        Assert.True(perms.Cannot(Permission.BanUsers));
    }

    [Fact]
    public void Cannot_ShouldReturnFalse_WhenPermissionExists()
    {
        var perms = new Permissions([Permission.BanUsers]);
        Assert.False(perms.Cannot(Permission.BanUsers));
    }

    [Fact]
    public void ImplicitConversion_ToPermissionArray_Works()
    {
        var perms = new Permissions([Permission.BanUsers]);
        Permission[] array = perms;
        Assert.Contains(Permission.BanUsers, array);
    }

    [Fact]
    public void ImplicitConversion_FromPermissionArray_Works()
    {
        Permission[] array = [Permission.BanUsers];
        Permissions perms = array;
        Assert.Contains(Permission.BanUsers, perms.Value);
    }
}
