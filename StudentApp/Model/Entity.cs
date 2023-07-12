using System.Text.Json.Serialization;

namespace WebAPI.Domain.Models;

public abstract class Entity
{
    public string? id { get; set; }

    [JsonPropertyName("createdDateTime")]
    public DateTime? CreatedDateTime { get; set; }


    [JsonPropertyName("partitionKey")]
    public abstract string? PartitionKey { get;}
}