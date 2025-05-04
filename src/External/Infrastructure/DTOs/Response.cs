using System.Text.Json.Serialization;
namespace Infrastructure.DTOs;

public record Response<T>
{
    public T Data { get; set; }
}
