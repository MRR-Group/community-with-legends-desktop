namespace Infrastructure.DTOs;

public abstract record Dto<T>
{
    public abstract T ToEntity();
}
