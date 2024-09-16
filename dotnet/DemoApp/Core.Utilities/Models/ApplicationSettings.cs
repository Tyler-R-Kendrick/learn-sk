namespace Core.Utilities.Models
{
    public record ApplicationSettings(
        OpenAI OpenAI
    );

    public record OpenAI(
        string Endpoint,
        string ModelName,
        string Key
    );
}
