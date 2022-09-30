using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task2.Task2;

namespace Task2;

public class Tests
{
    private const string TmpFileName = "../../../data/temp.txt";
    private const string Utf8File = "../../../data/text-utf8.txt";
    private const string Utf32File = "../../../data/text-utf-32.txt";

    [Test]
    public void Main1Test()
    {
        File.Copy(Utf8File, TmpFileName, true);
        Main(new[] { TmpFileName, "utf-8", "utf-32" });
        That(File.ReadAllBytes(TmpFileName),
            Is.EqualTo(File.ReadAllBytes(Utf32File)));
    }

    [Test]
    public void Main2Test()
    {
        File.Copy(Utf32File, TmpFileName, true);

        Main(new[] { TmpFileName, "utf-32", "utf-8" });
        That(File.ReadAllBytes(TmpFileName),
            Is.EqualTo(File.ReadAllBytes(Utf8File)));
    }
}
