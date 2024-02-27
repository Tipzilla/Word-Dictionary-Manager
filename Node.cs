public class Node
{
    public string Word { get; private set; }
    public int Length { get; private set; }

    // Constructor
    public Node(string word)
    {
        Word = word;
        Length = word.Length;
    }

    public override string ToString()
    {
        // Return a string representation of the Node
        return $"Word: {Word}, Length: {Length}";
    }
}
