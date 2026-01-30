namespace Lab0;

public sealed class SortedWordSet : IWordSet
{
    private SortedSet<string> words = [];

    public int Count => words.Count;
    public bool Add(string word)
    {
        var normalizedword = Normalize(word);
        words.Add(normalizedword);
        if (normalizedword.Length == 0)
            return false;

        return words.Add(word);
    }

     public bool Remove(string word)
    {
        var normalizedword = Normalize(word);
        words.Add(normalizedword);
        if (normalizedword.Length == 0)
            return false;

        return words.Remove(word);
    }

    public bool Contains(string word)
    {
        var normalizedword = Normalize(word);
        words.Add(normalizedword);
        if (normalizedword.Length == 0)
            return false;

        return words.Contains(word);
    }

    public string? Next(string word)
    {
        var normalizedword = Normalize(word);
        if (normalizedword.Length == 0 || words.Count == 0)
            return null;

        //var wordsInRange = words.GetViewBetween("a", "m");

        foreach(var candidate in words.GetViewBetween(normalizedword, MAX_STRING))
        {
            if (candidate.CompareTo(normalizedword) > 0)
                return candidate;
        }
        return null;
    }

        public string? Prev(string word)
    {
        var normalizedword = Normalize(word);
        if (normalizedword.Length == 0 || words.Count == 0)
            return null;

        //var wordsInRange = words.GetViewBetween("a", "m");

        foreach(var candidate in words.GetViewBetween(MIN_STRING, normalizedword))
        {
            if (candidate.CompareTo(normalizedword) > 0)
                return candidate;
        }
        return null;
    }


    public IEnumerable<string> Prefix(string prefix, int k)
    {
        if (k<=0 || words.Count == 0)
            return new List<string>();
        
        var results = new List<string>();

        var normalizedPrefix = Normalize(prefix);
        string lo = normalizedPrefix;
        string hi = normalizedPrefix+"{";

        int count = 0;

        foreach( var candidate in  words.GetViewBetween(lo, hi))
        {
            results.Add(candidate);
            count++;
            if (count >= k)
                return results;
        }

        return results;
    }

    public IEnumerable<string> Range(string lo, string hi, int k)
    {
        var range = new List<string>();

        foreach (var word in words)
        {
            if (word.CompareTo(hi) > 0)
                break;

            
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
    private const string MIN_STRING = "\u0000\u0000\u0000";

}