using System;
using NUnit.Framework;

namespace Task3
{
    public static class Task3
    {
        /*
         * Прежде чем приступать к выполнению заданий, допишите к ним тесты.
         */

        /*
         * Задание 3.1. Для данного вещественного x найти значение следующей функции f, принимающей значения целого типа:
         * 	        0,	если x < 0,
         * f(x) = 	1,	если x принадлежит [0, 1), [2, 3), …,
                   −1,	если x принадлежит [1, 2), [3, 4), … .
         */
        internal static double F(double x)
        {
            if (x < 0) {
                return 0;
            }

            if (x % 2 < 1) {
                return 1;
            }

            return -1;
        }

        /*
         * Задание 3.2. Дан номер года (положительное целое число). Определить количество дней в этом году,
         * учитывая, что обычный год насчитывает 365 дней, а високосный — 366 дней. Високосным считается год,
         * делящийся на 4, за исключением тех годов, которые делятся на 100 и не делятся на 400
         * (например, годы 300, 1300 и 1900 не являются високосными, а 1200 и 2000 — являются).
         */
        internal static int NumberOfDays(int year)
        {
            // https://en.wikipedia.org/wiki/Gregorian_calendar
            if (year < 1582) {
                throw new ArgumentOutOfRangeException(nameof(year), "Gregorian calendar was introduced in 1582");
            }

            if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0) {
                return 366;
            }

            return 365;
        }

        private static char NextOrientation(char c)
        {
            return c switch {
                'С' => 'З',
                'З' => 'Ю',
                'Ю' => 'В',
                'В' => 'С',
                _ => throw new ArgumentOutOfRangeException(nameof(c), "Invalid direction"),
            };
        }

        private static char Rotate(char orientation, int cmd)
        {
            return cmd switch {
                0 => orientation,
                1 => NextOrientation(orientation),
                2 => NextOrientation(NextOrientation(orientation)),
                -1 => NextOrientation(NextOrientation(NextOrientation(orientation))),
                _ => throw new ArgumentOutOfRangeException(nameof(cmd), "Invalid command"),
            };
        }

        /*
         * Задание 3.3. Локатор ориентирован на одну из сторон света («С» — север, «З» — запад,
         * «Ю» — юг, «В» — восток) и может принимать три цифровые команды поворота:
         * 1 — поворот налево, −1 — поворот направо, 2 — поворот на 180°.
         * Дан символ C — исходная ориентация локатора и целые числа N1 и N2 — две посланные команды.
         * Вернуть ориентацию локатора после выполнения этих команд.
         */
        internal static char Rotate2(char orientation, int cmd1, int cmd2)
        {
            return Rotate(Rotate(orientation, cmd1), cmd2);
        }

        /*
         * Задание 3.4. Дано целое число в диапазоне 20–69, определяющее возраст (в годах).
         * Вернуть строку-описание указанного возраста, обеспечив правильное согласование
         * числа со словом «год», например: 20 — «двадцать лет», 32 — «тридцать два года»,
         * 41 — «сорок один год».
         *
         * Пожалуйста, научитесь делать такие вещи очень аккуратно. Программное обеспечение
         * переполнено некорректными с точки зрения русского языка решениями.
         */
        internal static string AgeDescription(int age)
        {
            if (age is < 20 or > 69) {
                throw new ArgumentOutOfRangeException(nameof(age), "Age must be in range 20..69");
            }

            var mostSignificantDigit = age / 10;
            var leastSignificantDigit = age % 10;

            var wordOne = mostSignificantDigit switch {
                2 => "двадцать",
                3 => "тридцать",
                4 => "сорок",
                5 => "пятьдесят",
                6 => "шестьдесят",
                _ => throw new AssertionException("Impossible"),
            };

            var wordTwo = leastSignificantDigit switch {
                0 => null,
                1 => "один",
                2 => "два",
                3 => "три",
                4 => "четыре",
                5 => "пять",
                6 => "шесть",
                7 => "семь",
                8 => "восемь",
                9 => "девять",
                _ => throw new AssertionException("Impossible"),
            };

            var suffix = leastSignificantDigit switch {
                1 => "год",
                2 or 3 or 4 => "года",
                _ => "лет",
            };

            return wordTwo == null ? $"{wordOne} {suffix}" : $"{wordOne} {wordTwo} {suffix}";
        }

        public static void Main(string[] args)
        {
            throw new NotImplementedException(
                "Вызовите здесь все перечисленные в классе функции, как это сделано в предыдущих заданиях");
        }
    }
}
