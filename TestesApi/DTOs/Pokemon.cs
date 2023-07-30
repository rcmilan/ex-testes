using System.Text.Json.Serialization;

namespace TestesApi.DTOs;

public record Pokemon(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("height")] int Height,
    [property: JsonPropertyName("weight")] int Weight
);