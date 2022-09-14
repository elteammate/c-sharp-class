using System;
using System.IO;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task4.Task4;

public class Tests
{
    private readonly TextWriter _standardOut = Console.Out;
    private StringWriter _stringWriter = new StringWriter();

    [SetUp]
    public void Setup()
    {
        var stringWriter = new StringWriter();
        _stringWriter = stringWriter;
        Console.SetOut(_stringWriter);
    }

    [TearDown]
    public void TearDown()
    {
        Console.SetOut(_standardOut);
        _stringWriter.Close();
    }

    [Test]
    public void TestPrintFrame1_Test1()
    {
        PrintFrame(5, 3, '+');
        AssertOut("+++++\n+   +\n+++++");
    }

    [Test]
    public void TestPrintFrame1_Test2()
    {
        PrintFrame(5, 2, '&');
        AssertOut("&&&&&\n&&&&&");
    }

    [Test]
    public void TestPrintFrame1_Test3()
    {
        PrintFrame(2, 5, 'x');
        AssertOut("xx\nxx\nxx\nxx\nxx");
    }

    [Test]
    public void TestPrintFrame1_Test4()
    {
        PrintFrame(4, 4);
        AssertOut("****\n*  *\n*  *\n****");
    }

    [Test]
    public void TestPrintFrame2_Test1()
    {
        PrintFrame2(5, 3, '+');
        AssertOut("+++++\n+   +\n+++++");
    }

    [Test]
    public void TestPrintFrame2_Test2()
    {
        PrintFrame2(5, 2, '&');
        AssertOut("&&&&&\n&&&&&");
    }

    [Test]
    public void TestPrintFrame2_Test3()
    {
        PrintFrame2(2, 5, 'x');
        AssertOut("xx\nxx\nxx\nxx\nxx");
    }

    [Test]
    public void TestPrintFrame2_Test4()
    {
        PrintFrame2(4, 4);
        AssertOut("****\n*  *\n*  *\n****");
    }

    [Test]
    public void TestGcd()
    {
        That(Gcd(2, 3), Is.EqualTo(1));
        That(Gcd(10, 20), Is.EqualTo(10));
        That(Gcd(15, 20), Is.EqualTo(5));
        That(Gcd(413100, 283935), Is.EqualTo(15));

        That(Gcd(-2, -3), Is.EqualTo(1));
        That(Gcd(-10, 20), Is.EqualTo(10));
        That(Gcd(15, -20), Is.EqualTo(5));
        That(Gcd(-413100, -283935), Is.EqualTo(15));

        Throws<ArgumentException>(() => Gcd(0, 0));
    }

    [Test]
    public void TestExpTaylor()
    {
        That(ExpTaylor(0.0, 10), Is.EqualTo(1.0).Within(1e-2));
        That(ExpTaylor(1.0, 10), Is.EqualTo(Math.E).Within(1e-2));
        var x = new Random().NextDouble();
        That(ExpTaylor(x, 1000), Is.EqualTo(Math.Exp(x)).Within(1e-10));
    }

    private void AssertOut(string expected)
    {
        That(_stringWriter.ToString().TrimEnd(Environment.NewLine.ToCharArray()),
            Is.EqualTo(expected));
    }


    [Test]
    public void MainTest()
    {
        That(Main(Array.Empty<string>()), Is.EqualTo(-1));
        That(Main(new[] { "some-arg", "other-arg" }), Is.EqualTo(-1));
        That(Main(new[] { "unknown-arg" }), Is.EqualTo(-1));
    }
}
