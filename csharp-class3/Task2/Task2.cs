using System.Text;
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
        internal record InputData(double FromX, double ToX, int NumberOfPoints, List<string> FunctionNames);

        // Чтение входных данных из параметров командной строки
        private static InputData? PrepareData(string[] args)
        {
            if (args.Length < 4)
                return null;

            if (!double.TryParse(args[0], out var fromX)) return null;
            if (!double.TryParse(args[1], out var toX)) return null;
            if (!int.TryParse(args[2], out var number)) return null;
            
            var functions = new List<string>();
            for (var i = 3; i < args.Length; i++)
            {
                if (!AvailableFunctions.ContainsKey(args[i]))
                    return null;
                functions.Add(args[i]);
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
                var columnCount = ColumnNames.Count;
                var formattedValues = new List<List<string>>();

                foreach (var line in Values)
                {
                    formattedValues.Add(new List<string>());
                    foreach (var value in line)
                        formattedValues[^1].Add(value.ToString($"F{Precision}"));
                }

                var maxLength = new List<int>();
                
                for (var column = 0; column < columnCount; column++)
                {
                    var valuesSpan = formattedValues.Max(x => x[column].Length);
                    var headerSpan = ColumnNames[column].Length;
                    maxLength.Add(Math.Max(valuesSpan, headerSpan));
                }

                var lineSpanEstimate = maxLength.Sum() + columnCount * 2 + 1;
                var builder = new StringBuilder(columnCount * lineSpanEstimate);

                void AppendLine(List<string> values)
                {
                    for (var column = 0; column < columnCount; column++)
                    {
                        var header = values[column];
                        var headerSpan = header.Length;
                        var span = maxLength[column];
                        var padding = span - headerSpan + (column == 0 ? 0 : 1);
                        builder.Append(' ', padding);
                        builder.Append(header);
                    }
                    
                    builder.AppendLine();
                }
                
                AppendLine(ColumnNames);
                foreach (var line in formattedValues)
                    AppendLine(line);

                builder.Remove(builder.Length - 1, 1);
                return builder.ToString();
            }
        }

        private static double NthValueInRange(double from, double to, int n, int total) =>
            from + (to - from) * n / (total - 1);

        /*
         * Возвращает таблицу значений заданных функций на заданном отрезке [fromX, toX]
         * с заданным количеством точек.
         */
        internal static FunctionTable Tabulate(InputData input)
        {
            var values = new List<List<double>>(input.FunctionNames.Count + 1);
            var columnNames = new List<string>(input.FunctionNames.Count + 1) { "x" };
            columnNames.AddRange(input.FunctionNames);
            
            for (var i = 0; i < input.NumberOfPoints; i++)
            {
                var x = NthValueInRange(input.FromX, input.ToX, i, input.NumberOfPoints);
                
                values.Add(new List<double>(input.FunctionNames.Count + 1) { x });
                foreach (var function in input.FunctionNames)
                {
                    try
                    {
                        values[i].Add(AvailableFunctions[function](x));
                    } catch (Exception)
                    {
                        values[i].Add(double.NaN);
                    }
                }
            }

            return new FunctionTable(3, columnNames, values);
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
