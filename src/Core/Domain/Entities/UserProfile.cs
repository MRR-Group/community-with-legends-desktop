using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

public class UserProfile: Reportable
{
    public Hardware[] HardwareList { get; private set; } 
    
    public UserProfile(uint id, Date creationDate, User user, Hardware[] hardwareList) : base(id, creationDate, user, "")
    {
        HardwareList = hardwareList;
    }
}

