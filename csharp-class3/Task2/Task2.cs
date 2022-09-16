using OneVariableFunction = System.Func<double, double>;
using FunctionName = System.String;

namespace Task2
{
    public class Task2
    {

        /*
         * В этом задании необходимо написать программу, способную табулировать сразу несколько
         * функций одной вещественной переменной на одном заданном отрезке.
         */

        // Сформируйте набор как минимум из десяти вещественных функций одной переменной
        internal static readonly Dictionary<string, OneVariableFunction> AvailableFunctions =
            new()
            {
                { "square", x => x * x },
                { "sin", Math.Sin },
                { "cos", Math.Cos },
                { "exp", Math.Exp },
                { "sqrt", Math.Sqrt },
                { "abs", Math.Abs },
                { "log", Math.Log },
                { "tan", Math.Tan },
                { "inv", x => 1 / x },
                { "invsqrt", x => 1 / Math.Sqrt(x) },
                { "sign", x => Math.Sign(x) },
            };

        // Тип данных для представления входных данных
        internal record InputData(double FromX, double ToX, int NumberOfPoints, List<OneVariableFunction> FunctionNames);

        // Чтение входных данных из параметров командной строки
        private static InputData? PrepareData(string[] args)
        {
            if (args.Length < 4)
                return null;

            if (!double.TryParse(args[0], out var fromX)) return null;
            if (!double.TryParse(args[1], out var toX)) return null;
            if (!int.TryParse(args[2], out var number)) return null;
            
            var functions = new List<OneVariableFunction>();
            for (var i = 3; i < args.Length; i++)
            {
                if (!AvailableFunctions.ContainsKey(args[i]))
                    return null;
                functions.Add(AvailableFunctions[args[i]]);
            }

            return new InputData(fromX, toX, number, functions);
        }

        // Тип данных для представления таблицы значений функций
        // с заголовками столбцов и строками (первый столбец --- значение x,
        // остальные столбцы --- значения функций). Одно из полей --- количество знаков
        // после десятичной точки.
        internal record FunctionTable(int Precision, List<string> ColumnNames, List<List<double>> Values)
        {
            // Код, возвращающий строковое представление таблицы (с использованием StringBuilder)
            // Столбец x выравнивается по левому краю, все остальные столбцы по правому.
            // Для форматирования можно использовать функцию String.Format.
            public override string ToString()
            {
                List<List<string>> formattedValues = new();
                foreach (var row in Values)
                {
                    var formattedRow = new List<string>();
                    foreach (var value in row)
                    {
                        formattedRow.Add(value.ToString($"F{Precision}"));
                    }
                    formattedValues.Add(formattedRow);
                }
            }
        }

        /*
         * Возвращает таблицу значений заданных функций на заданном отрезке [fromX, toX]
         * с заданным количеством точек.
         */
        internal static FunctionTable Tabulate(InputData input)
        {
            throw new NotImplementedException();
        }
        
        public static void Main(string[] args)
        {
            var input = PrepareData(args);

            if (input == null)
                Console.WriteLine("Incorrect input data");
            else
                Console.WriteLine(Tabulate(input));
        }
    }
}
