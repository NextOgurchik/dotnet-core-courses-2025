using System.Drawing;
using Console = Colorful.Console;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteWithGradient("Введите первое число: ", Color.Yellow, Color.Fuchsia, 14);
        var x = int.Parse(Console.ReadLine());

        Console.WriteWithGradient("Введите второе число: ", Color.Yellow, Color.Fuchsia, 14);
        var y = int.Parse(Console.ReadLine());

        var calc = new Calc.Lib.Calculator();

        Console.WriteWithGradient("Результат сложения: " + calc.Add(x,y), Color.Yellow, Color.Fuchsia, 14);
    }
}