using Domain.Enums;
using Domain.ValueObjects;
using Infrastructure.Repositories;
using Xunit;

namespace Tests.Repositories;

public class PermissionRepositoryTests
{
    [Fact]
    public void Can_ShouldReturnTrue_WhenPermissionIsLoadedAndAllowed()
    {
        var permissions = new Permissions([Permission.BanUsers, Permission.AnonymizeUsers]);
        var repo = new PermissionRepository();
        repo.Load(permissions);
        Assert.True(repo.Can(Permission.BanUsers));
        Assert.True(repo.Can(Permission.AnonymizeUsers));
    }

    [Fact]
    public void Can_ShouldReturnFalse_WhenPermissionIsNotLoadedOrNotAllowed()
    {
        var permissions = new Permissions([Permission.BanUsers]);
        var repo = new PermissionRepository();
        repo.Load(permissions);
        Assert.False(repo.Can(Permission.AnonymizeUsers));
    }

    [Fact]
    public void Can_ShouldReturnFalse_WhenPermissionsNotLoaded()
    {
        var repo = new PermissionRepository();
        Assert.False(repo.Can(Permission.BanUsers));
    }

    [Fact]
    public void Cannot_ShouldReturnTrue_WhenPermissionIsNotLoadedOrNotAllowed()
    {
        var permissions = new Permissions([Permission.BanUsers]);
        var repo = new PermissionRepository();
        repo.Load(permissions);
        Assert.True(repo.Cannot(Permission.AnonymizeUsers));
    }

    [Fact]
    public void Cannot_ShouldReturnTrue_WhenPermissionsNotLoaded()
    {
        var repo = new PermissionRepository();
        Assert.True(repo.Cannot(Permission.BanUsers));
    }

    [Fact]
    public void Unload_ShouldClearPermissions()
    {
        var permissions = new Permissions([Permission.BanUsers]);
        var repo = new PermissionRepository();
        repo.Load(permissions);
        repo.Unload();
        Assert.False(repo.Can(Permission.BanUsers));
        Assert.True(repo.Cannot(Permission.BanUsers));
    }
}

