using System.Text.RegularExpressions;

namespace Filters;

public sealed class CensorService(params string[] bannedPhrases)
{
    private const string
        CensoredText = "[Censored]",
        PatternTemplate = @"\b({0})(s?)\b";
    private readonly IEnumerable<Regex> Regexes = bannedPhrases
        .Select(x => string.Format(PatternTemplate, x))
        .Select(x => new Regex(x, RegexOptions.IgnoreCase));

    public string Transform(string input) => Regexes
        .Aggregate(input, (current, matcher)
            => matcher.Replace(current, CensoredText));

    public void Reject(string input)
    {
        foreach (var regex in Regexes)
        {
            if (regex.IsMatch(input))
            {
                throw new ArgumentException("Input contains banned phrase");
            }
        }
    }
}
