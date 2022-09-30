using System.Text;

namespace Task2;

public static class Task2
{
    private record ProgramArguments(string FileName, Encoding InputEncoding, Encoding OutputEncoding);

    private static ProgramArguments ParseArguments(IReadOnlyList<string> args)
    {
        if (args.Count != 3)
            throw new ArgumentException(
                "Usage: <program> <file> <input-encoding> <output-encoding>"
            );

        var allEncodings = Encoding.GetEncodings().Select(e => e.Name).ToHashSet();

        if (!allEncodings.Contains(args[1]))
            throw new ArgumentException($"Unknown input encoding: {args[1]}");

        if (!allEncodings.Contains(args[2]))
            throw new ArgumentException($"Unknown input encoding: {args[2]}");

        var input = Encoding.GetEncoding(args[1]);
        var output = Encoding.GetEncoding(args[2]);

        return new ProgramArguments(args[0], input, output);
    }

    private static void Convert(ProgramArguments programArguments)
    {
        var initial = File.ReadAllBytes(programArguments.FileName);

        var converted = Encoding.Convert(
            programArguments.InputEncoding,
            programArguments.OutputEncoding,
            initial
        );

        File.WriteAllBytes(programArguments.FileName, converted);
    }

    public static void Main(string[] args)
    {
        var arguments = ParseArguments(args);
        Convert(arguments);
    }
}
