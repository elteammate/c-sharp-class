using System;
using System.Numerics;

namespace Task5
{
    public static class Task5
    {
        /*
         * Задание 5.1. Запустите функцию `main`, нажав зелёный треугольник слева от её имени. Это действие создаст
         * конфигурацию, которую позже можно отредактировать. В этом задании для ввода данных используются
         * либо параметры командной строки, либо консольный ввод. При запуске через Rider параметры командной строки
         * задаются в окне "Run/Edit configurations..." ("Program arguments:").
         */
        public static void Main(string[] args)
        {
            DemoInput(args);
            ComputeFib(args);
        }

        /*
         * Задание 5.2. Разберите код функции `demoInput`.
         */
        private static void DemoInput(string[] args)
        {
            string name;
            if (args.Length > 0)
                name = string.Join(" ", args);
            else
            {
                Console.WriteLine("Введите имя:");
                name = Console.ReadLine()!;
            }

            Console.WriteLine($"Привет, {name}");
        }

        /*
         * Задание 5.3. Напишите программу, которая принимает на вход целое неотрицательное число
         * (либо параметром командной строки, либо с клавиатуры) и печатает число Фибоначчи:
         * https://ru.wikipedia.org/wiki/%D0%A7%D0%B8%D1%81%D0%BB%D0%B0_%D0%A4%D0%B8%D0%B1%D0%BE%D0%BD%D0%B0%D1%87%D1%87%D0%B8
         * с соответствующим номером. Мы считаем, что нулевым числом Фибоначчи является число 0, а первым — число 1.
         *
         * Для представления чисел Фибоначчи следует использовать длинные целые:
         * - https://docs.microsoft.com/ru-ru/dotnet/api/system.numerics.biginteger?view=net-6.0
         */
        private static BigInteger Fib(int n)
        {
            if (n == 0) return 0;

            var previous = new BigInteger(0);
            var last = new BigInteger(1);
            
            for (var i = 1; i < n; i++)
            {
                var newLast = previous + last;
                previous = last;
                last = newLast;
            }

            return last;
        }

        internal static void ComputeFib(string[] args)
        {
            // Я знаю, что это написано не так, как в DemoInput, но я считаю, что это читаемее
            var n = args.Length switch
            {
                1 => int.Parse(args[0]),
                0 => int.Parse(Console.ReadLine()!),
                _ => throw new ArgumentException("Wrong number of arguments")
            };

            Console.WriteLine(Fib(n));
        }
    }
}
