namespace Solution7
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
