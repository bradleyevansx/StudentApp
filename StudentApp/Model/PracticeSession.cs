using System.Text.Json.Serialization;

namespace WebAPI.Domain.Models;

public class PracticeSession : Entity
{
    public string? userId { get; set; }
    
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    [JsonPropertyName("length")]
    public TimeSpan? Length { get; set; }
    
    [JsonPropertyName("date")]
    public DateTime? Date { get; set; }
    
    [JsonPropertyName("focusPoints")]
    public List<FocusPoint>? FocusPoints { get; set; } = new();
    
    public override string? PartitionKey => userId;
}