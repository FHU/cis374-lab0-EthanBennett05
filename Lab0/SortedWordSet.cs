namespace Lab0;

public sealed class SortedWordSet : IWordSet
{
    private SortedSet<string> words = [];

    public int Count => words.Count;
    public bool Add(string word)
    {
        var normalizedword = Normalize(word);
        if (normalizedword.Length == 0)
            return false;
        foreach (var w in words)
        {
            if (word.CompareTo(w) == 0)
                return false;
        }
        words.Add(normalizedword);

        return true;
    }

    public bool Remove(string word)
    {
        var normalizedword = Normalize(word);

        foreach (var w in words)
        {
            if (w.CompareTo(word) == 0)
            { 
                words.Remove(normalizedword);
                return true;
            }
        }
        return false;
    }

    public bool Contains(string word)
    {
        if (word.Length == 0 || words.Count == 0)
            return false;

        var normalizedword = Normalize(word);

        return words.Contains(normalizedword);
    }

    public string? Next(string word)
    {
        var normalizedword = Normalize(word);
        if (normalizedword.Length == 0 || words.Count == 0)
            return null;

        //var wordsInRange = words.GetViewBetween("a", "m");

        foreach (var candidate in words.GetViewBetween(normalizedword, MAX_STRING))
        {
            if (candidate.CompareTo(normalizedword) > 0)
                return candidate;
        }
        return null;
    }

    public string? Prev(string word)
    {
        string? best = null;
        var normalizedword = Normalize(word);
        if (normalizedword.Length == 0 || words.Count == 0)
            return null;


        //var wordsInRange = words.GetViewBetween("a", "m");
        var min_string = "\u0000\u0000\u0000";

        foreach (var candidate in words.GetViewBetween(min_string, normalizedword))
        {
            if (candidate.CompareTo(normalizedword) < 0)
                best = candidate;
            else break;
        }
        return best;
    }


    public IEnumerable<string> Prefix(string prefix, int k)
    {
        if (k <= 0 || words.Count == 0)
            return new List<string>();

        var results = new List<string>();

        var normalizedPrefix = Normalize(prefix);
        string lo = normalizedPrefix;
        string hi = normalizedPrefix + "{";

        foreach (var word in words)
        {
            if (word.StartsWith(normalizedPrefix))
                results.Add(word);
            if (results.Count >= k) break;
        }
        results.Sort();

        return results;
    }

    public IEnumerable<string> Range(string lo, string hi, int k)
    {
        var range = new List<string>();

        foreach (var word in words)
        {
            if (word.CompareTo(lo) < 0) continue;
            if (word.CompareTo(hi) > 0) break;

            range.Add(word);
            if (range.Count >= k)
                break;
        }

        return range;

    }

    private string Normalize(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return string.Empty;

        return word.Trim().ToLowerInvariant();
    }

    private const string MAX_STRING = "\uFFFF\uFFFF\uFFFF";
}