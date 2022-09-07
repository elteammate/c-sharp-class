using System;
using System.ComponentModel;

namespace Task4
{
    public static class Task4
    {
        /*
         * В решениях следующих заданий предполагается использование циклов.
         */

        /*
         * Задание 4.1. Пользуясь циклом for, посимвольно напечатайте рамку размера width x height,
         * состоящую из символов frameChar. Можно считать, что width>=2, height>=2.
         * Например, вызов printFrame(5,3,'+') должен напечатать следующее:

        +++++
        +   +
        +++++
         */
        internal static void PrintFrame(int width, int height, char frameChar = '*')
        {
            if (width < 2 || height < 2) {
                throw new ArgumentException("Width and height must be greater than 2");
            }

            for (var y = 0; y < height; y++) {
                for (var x = 0; x < width; x++) {
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1) {
                        Console.Write(frameChar);
                    } else {
                        Console.Write(' ');
                    }
                }

                Console.WriteLine();
            }
        }

        /*
         * Задание 4.2. Выполните предыдущее задание, пользуясь циклом while.
         */
        internal static void PrintFrame2(int width, int height, char frameChar = '*')
        {
            if (width < 2 || height < 2) {
                throw new ArgumentException("Width and height must be greater than 2");
            }

            var x = 0;
            var y = 0;

            while (y < height) {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1) {
                    Console.Write(frameChar);
                } else {
                    Console.Write(' ');
                }

                x++;
                if (x >= width) {
                    x = 0;
                    y++;
                    Console.WriteLine();
                }
            }
        }


        /*
         * Задание 4.3. Даны целые положительные числа A и B. Найти их наибольший общий делитель (НОД),
         * используя алгоритм Евклида:
         * НОД(A, B) = НОД(B, A mod B),    если B ≠ 0;        НОД(A, 0) = A,
         * где «mod» обозначает операцию взятия остатка от деления.
         */
        internal static long Gcd(long a, long b)
        {
            if (a == 0 && b == 0) {
                throw new ArgumentException("A and B cannot be zero at the same time");
            }

            a = Math.Abs(a);
            b = Math.Abs(b);

            while (b != 0) {
                var temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        /*
         * Задание 4.4. Дано вещественное число X и целое число N (> 0). Найти значение выражения
         * 1 + X + X^2/(2!) + … + X^N/(N!), где N! = 1·2·…·N.
         * Полученное число является приближенным значением функции exp в точке X.
         */
        internal static double ExpTaylor(double x, int n)
        {
            var approximation = 0.0;
            var term = 1.0;

            for (var i = 1; i <= n; i++) {
                approximation += term;
                term *= x / i;
            }

            return approximation;
        }

        public static int Main(string[] args)
        {
            if (args.Length != 1) {
                Console.WriteLine("Usage: <program> <command>");
                return -1;
            }

            var command = args[0];

            switch (command) {
                case "printFrame": {
                    var w = int.Parse(Console.ReadLine()!);
                    var h = int.Parse(Console.ReadLine()!);
                    var c = char.Parse(Console.ReadLine()!);
                    PrintFrame(w, h, c);
                    break;
                }

                case "printFrame2": {
                    var w = int.Parse(Console.ReadLine()!);
                    var h = int.Parse(Console.ReadLine()!);
                    var c = char.Parse(Console.ReadLine()!);
                    PrintFrame2(w, h, c);
                    break;
                }

                case "gcd": {
                    var a = long.Parse(Console.ReadLine()!);
                    var b = long.Parse(Console.ReadLine()!);
                    Console.WriteLine(Gcd(a, b));
                    break;
                }

                case "expTaylor": {
                    var x = double.Parse(Console.ReadLine()!);
                    var n = int.Parse(Console.ReadLine()!);
                    Console.WriteLine(ExpTaylor(x, n));
                    break;
                }

                default: return -1;
            }

            return 0;
        }
    }
}
