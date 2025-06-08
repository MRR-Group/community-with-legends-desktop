using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Infrastructure.DTOs;

public record ReportDto
{
    [property: JsonPropertyName("id")]
    public uint Id { get; set; }
    
    [property: JsonPropertyName("reason")]
    public string Reason { get; set; }
    
    [property: JsonPropertyName("reportable_type")]
    public string ReportableType { get; set; }
    
    [property: JsonPropertyName("reported_at")]
    public string ReportedAt { get; set; }
    
    [property: JsonPropertyName("resolved_at")]
    public string? ResolvedAt { get; set; }
    
    [property: JsonPropertyName("status")]
    public string[] Status { get; set; }
    
    [property: JsonPropertyName("reported_by")]
    public UserDto ReportedBy { get; set; }
    
    [property: JsonPropertyName("reportable")]
    public JsonElement Reportable { get; set; }

    public Report? ToEntity()
    {
        var reportable = ConvertReportable();

        if (reportable == null)
        {
            return null;
        }

        var status = ConvertStatus();
        var resolvedAt = ResolvedAt != null ? new Date(ResolvedAt) : null;
        
        return new Report(Id, Reason, reportable, new Date(ReportedAt), ReportedBy.ToEntity(), status, resolvedAt);
    }

    private Reportable? ConvertReportable()
    {
        if (ReportableType == "Comment")
        {
            return Reportable.Deserialize<CommentDto>()!.ToEntity();
        }
        
        if (ReportableType == "Post")
        {
            return Reportable.Deserialize<PostDto>()!.ToEntity();
        }

        return null;
    }
    
    private ReportStatus[] ConvertStatus()
    {
        return Status.Select(FirstCharToUpper).Select(Enum.Parse<ReportStatus>).ToArray();
    }
    
    private string FirstCharToUpper(string input)
    {
        return string.IsNullOrEmpty(input) ? string.Empty : $"{char.ToUpper(input[0])}{input[1..]}";
    }
}