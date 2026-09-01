using System.Text.Json.Serialization;

namespace ECommerceApp.API.Common.Responses;

public class ProblemApiResponse
{
    public bool Success { get; init; } = false;
    public string Type { get; init; } = default!;
    public string Title { get; init; } = default!;
    public int Status { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, string[]>? Errors { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Details { get; set; } = default!;
    public string TraceId { get; set; } = default!;
    public string Instance { get; set; } = default!;
}

