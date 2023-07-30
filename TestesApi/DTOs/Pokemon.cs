using System.Text.Json.Serialization;

namespace TestesApi.DTOs;

public class Pokemon
{
    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("weight")]
    public int Weight { get; set; }
}