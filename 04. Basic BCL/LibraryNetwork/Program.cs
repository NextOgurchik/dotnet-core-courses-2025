using LibraryNetwork;
using LibraryNetwork.Models;
using LibraryNetwork.Models.Entety;
internal class Program
{
    private static void Main(string[] args)
    {
        OrderBy? x = null;
        Console.WriteLine(X(x));
    }
    public static int X(OrderBy? orderby)
    {
        return orderby switch
        {
            OrderBy.Asc => 1,
            OrderBy.Desc => 2,
            _ => 3
        };
    }
}