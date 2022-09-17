using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace Task2;

public static class Task2
{
    /*
     * В этих заданиях * рекомендуется всюду использовать класс StringBuilder.
     * Документация: https://docs.microsoft.com/ru-ru/dotnet/api/system.text.stringbuilder?view=net-6.0
     */

    /*
     * Задание 2.1. Дана непустая строка S и целое число N (> 0). Вернуть строку, содержащую символы
     * строки S, между которыми вставлено по N символов «*» (звездочка).
     */
    internal static string FillWithAsterisks(string s, int n)
    {
        var builder = new StringBuilder(s, (n + 1) * s.Length);

        for (var i = 1; i < s.Length; i++)
        {
            builder.Insert(i + n * (i - 1), new string('*', n));
        }

        return builder.ToString();
    }
    
    
    internal static string FillWithAsterisks2(string s, int n)
    {
        var builder = new StringBuilder((n + 1) * s.Length);

        for (var i = 0; i < s.Length - 1; i++)
        {
            builder.Append(s[i]);
            builder.Append('*', n);
        }
        
        builder.Append(s[^1]);

        return builder.ToString();
    }

    /*
     * Задание 2.2. Сформировать таблицу квадратов чисел от 1 до заданного числа N.
     * Например, для N=4 должна получиться следующая строка:

    1  1
    2  4
    3  9
    4 16

     * Обратите внимание на выравнивание: числа в первом столбце выравниваются по левому краю,
     * а числа во втором -- по правому, причём между числами должен оставаться как минимум один
     * пробел. В решении можно использовать функции Pad*.
     */
    internal static string TabulateSquares(int n)
    {
        var columnOneWidth = n.ToString().Length;
        var columnTwoWidth = (n * n).ToString().Length;
        var builder = new StringBuilder(n * (columnOneWidth + columnTwoWidth + 2));

        for (var i = 1; i <= n; i++)
        {
            builder.Append(i.ToString().PadRight(columnOneWidth));
            builder.Append(' ');
            builder.Append((i * i).ToString().PadLeft(columnTwoWidth));
            builder.Append('\n');
        }

        // Remove last \n
        builder.Remove(builder.Length - 1, 1);
        return builder.ToString();
    }

    public static void Main(string[] args)
    {
        Console.WriteLine(FillWithAsterisks("abc", 2));
        Console.WriteLine(TabulateSquares(4));

        
        BenchmarkRunner.Run<Benchmark>();
    }
}

public class Config : ManualConfig
{
    public Config()
    {
        WithOptions(ConfigOptions.DisableOptimizationsValidator);
    }
}

[RPlotExporter]
[Config(typeof(Config))]
public class Benchmark
{
    [Params(1, 2, 5, 10, 100, 1000)]
    public int N;
    
    [Params(100, 1000, 10_000, 100_000)]
    public int M;

    [Benchmark]
    public string Insert() => Task2.FillWithAsterisks(new string('a', M), N);
    [Benchmark]
    public string Append() => Task2.FillWithAsterisks2(new string('a', M), N);
}
