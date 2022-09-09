namespace Task1;

public static class Task1
{
    /*
     * Задание 1.1. Дана строка. Верните строку, содержащую текст "Длина: NN",
     * где NN — длина заданной строки. Например, если задана строка "hello",
     * то результатом должна быть строка "Длина: 5".
     */
    internal static int StringLength(string s) => s.Length;

    /*
     * Задание 1.2. Дана непустая строка. Вернуть коды ее первого и последнего символов.
     * Рекомендуется найти специальные функции для вычисления соответствующих символов и их кодов.
     */
    internal static Tuple<int?, int?> FirstLastCodes(string s) => new(Code(First(s)), Code(Last(s)));

    private static char? First(string s) => s.Length > 0 ? s[0] : null;
    private static char? Last(string s) => s.Length > 0 ? s[^1] : null;
    private static int? Code(char? c) => c;


    /*
     * Задание 1.3. Дана строка. Подсчитать количество содержащихся в ней цифр.
     * В решении необходимо воспользоваться циклом for.
     */
    internal static int CountDigits(string s)
    {
        var count = 0;
        foreach (var t in s) {
            if (char.IsDigit(t)) {
                count++;
            }
        }

        return count;
    }

    /*
     * Задание 1.4. Дана строка. Подсчитать количество содержащихся в ней цифр.
     * В решении необходимо воспользоваться методом Count:
     * https://docs.microsoft.com/ru-ru/dotnet/api/system.linq.enumerable.count?view=net-6.0#system-linq-enumerable-count-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean)))
     * и функцией Char.IsDigit:
     * https://docs.microsoft.com/ru-ru/dotnet/api/system.char.isdigit?view=net-6.0
     */
    internal static int CountDigits2(string s) => s.Count(char.IsDigit);

    /*
     * Задание 1.5. Дана строка, изображающая арифметическое выражение вида «<цифра>±<цифра>±…±<цифра>»,
     * где на месте знака операции «±» находится символ «+» или «−» (например, «4+7−2−8»). Вывести значение
     * данного выражения (целое число).
     */
    internal static int CalcDigits(string expr)
    {
        expr = expr.Insert(0, "+");
        var result = 0;

        for (var i = 0; i < expr.Length; i += 2) {
            result += (expr[i + 1] - '0') * (expr[i] == '+' ? 1 : -1);
        }

        return result;
    }

    /*
     * Задание 1.6. Даны строки S, S1 и S2. Заменить в строке S первое вхождение строки S1 на строку S2.
     */
    internal static string ReplaceWithString(string s, string s1, string s2)
    {
        for (var i = 0; i <= s.Length - s1.Length; i++) {
            var isMatch = true;
            for (var j = 0; j < s1.Length; j++) {
                if (s[i + j] != s1[j]) {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch) {
                return s[..i] + s2 + s[(i + s1.Length)..];
            }
        }

        return s;
    }


    public static void Main(string[] args)
    {
        Console.WriteLine(StringLength("Hello"));
        Console.WriteLine(FirstLastCodes("Hello"));
        Console.WriteLine(CountDigits("Hello123"));
        Console.WriteLine(CountDigits2("Hello123"));
        Console.WriteLine(CalcDigits("4+7-2-8"));
        Console.WriteLine(ReplaceWithString("Hello world", "world", "everyone"));
    }
}
