using Domain.Enums;
using Domain.ValueObjects;
namespace Infrastructure.Repositories;

public class PermissionRepository
{
    private Permissions? _permissions = null;

    public PermissionRepository() { }

    public void Load(Permissions permissions)
    {
        _permissions = permissions;
    }
    
    public void Unload()
    {
        _permissions = null;
    }
    
    public bool Can(Permission permission) => _permissions?.Can(permission) ?? false;
    public bool Cannot(Permission permission) => _permissions?.Cannot(permission) ?? true;
}
