using LibraryNetwork;

internal class Program
{
    private static void Main(string[] args)
    {
        Book b = new Book(name: null, note: null);
        Console.WriteLine(b.Name);
        Console.WriteLine(b.Note);
    }
}