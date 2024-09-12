using System.Text.RegularExpressions;

namespace Filters;

internal sealed class CensorService(string[] bannedPhrases)
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
}
