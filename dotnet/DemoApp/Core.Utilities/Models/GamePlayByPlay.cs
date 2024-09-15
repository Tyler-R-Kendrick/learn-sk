namespace Core.Utilities.Models
{
    public record PlayByPlay(List<Play> AllPlays);

    public record Play(PlayResult Result);

    public record PlayResult(
        string Description    
    );
}
