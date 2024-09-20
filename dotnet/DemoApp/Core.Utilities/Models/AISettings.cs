namespace Core.Utilities.Models;

public record AISettings(
    OpenAI OpenAI
);

public record OpenAI(
    string Endpoint,
    string ModelName,
    string Key
);
