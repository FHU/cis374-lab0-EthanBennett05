namespace Lab0;

class Program
{
    static void Main(string[] args)
    {
        var wordSet = new SortedWordSet();
        wordSet.Add("hey");
        System.Console.WriteLine(wordSet.Remove("hey"));
        System.Console.WriteLine(wordSet.Contains("hey"));
        // Assert.IsTrue(wordSet.Remove("hey"));
        // Assert.IsFalse(wordSet.Contains("hey"));
    }
}
