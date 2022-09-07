using System;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task3.Task3;

public class Tests
{
    [Test]
    public void FTest()
    {
        const double eps = 1e-6;

        That(F(-eps), Is.EqualTo(0.0).Within(1e-5));
        That(F(-1.0), Is.EqualTo(0.0).Within(1e-5));
        That(F(-1.5), Is.EqualTo(0.0).Within(1e-5));
        That(F(-2.56), Is.EqualTo(0.0).Within(1e-5));
        That(F(-1337), Is.EqualTo(0.0).Within(1e-5));

        That(F(0.0), Is.EqualTo(1.0).Within(1e-5));

        That(F(0.5), Is.EqualTo(1.0).Within(1e-5));
        That(F(eps), Is.EqualTo(1.0).Within(1e-5));
        That(F(1 - eps), Is.EqualTo(1.0).Within(1e-5));

        That(F(1.0), Is.EqualTo(-1.0).Within(1e-5));
        That(F(1.5), Is.EqualTo(-1.0).Within(1e-5));
        That(F(1.0 + eps), Is.EqualTo(-1.0).Within(1e-5));
        That(F(2.0 - eps), Is.EqualTo(-1.0).Within(1e-5));


        That(F(7.5), Is.EqualTo(-1.0).Within(1e-5));
        That(F(23.367), Is.EqualTo(-1.0).Within(1e-5));
        That(F(49.957), Is.EqualTo(-1.0).Within(1e-5));

        That(F(8.5), Is.EqualTo(1.0).Within(1e-5));
        That(F(24.367), Is.EqualTo(1.0).Within(1e-5));
        That(F(50.957), Is.EqualTo(1.0).Within(1e-5));

        That(F(54347), Is.EqualTo(-1.0).Within(1e-5));
        That(F(1337), Is.EqualTo(-1.0).Within(1e-5));

        That(F(1338), Is.EqualTo(1.0).Within(1e-5));
        That(F(543414), Is.EqualTo(1.0).Within(1e-5));
    }

    [Test]
    public void NumberOfDaysTest()
    {
        That(NumberOfDays(2021), Is.EqualTo(365));
        That(NumberOfDays(2022), Is.EqualTo(365));
        That(NumberOfDays(2023), Is.EqualTo(365));
        That(NumberOfDays(2024), Is.EqualTo(366));
        That(NumberOfDays(13454), Is.EqualTo(365));

        That(NumberOfDays(2000), Is.EqualTo(366));
        That(NumberOfDays(2100), Is.EqualTo(365));
        That(NumberOfDays(2300), Is.EqualTo(365));
        That(NumberOfDays(2400), Is.EqualTo(366));
        That(NumberOfDays(3000), Is.EqualTo(365));

        // ДО 1581 года календарь не был григорианским
        Throws<ArgumentOutOfRangeException>(() => NumberOfDays(-1650));
        Throws<ArgumentOutOfRangeException>(() => NumberOfDays(0));
        Throws<ArgumentOutOfRangeException>(() => NumberOfDays(1581));
    }

    [Test]
    public void Rotate2Test()
    {
        That(Rotate2('С', 1, -1), Is.EqualTo('С'));
        That(Rotate2('С', 1, 1), Is.EqualTo('Ю'));
        That(Rotate2('Ю', 1, 1), Is.EqualTo('С'));
        That(Rotate2('В', 2, 2), Is.EqualTo('В'));
        That(Rotate2('С', -1, -1), Is.EqualTo('Ю'));
        That(Rotate2('С', 2, -1), Is.EqualTo('З'));
        That(Rotate2('З', -1, 2), Is.EqualTo('Ю'));
        That(Rotate2('Ю', 1, 2), Is.EqualTo('З'));
    }

    [Test]
    public void AgeDescriptionTest()
    {
        That(AgeDescription(42), Is.EqualTo("сорок два года"));
        That(AgeDescription(69), Is.EqualTo("шестьдесят девять лет"));
        That(AgeDescription(20), Is.EqualTo("двадцать лет"));
        That(AgeDescription(38), Is.EqualTo("тридцать восемь лет"));
        That(AgeDescription(22), Is.EqualTo("двадцать два года"));
        That(AgeDescription(51), Is.EqualTo("пятьдесят один год"));
        That(AgeDescription(63), Is.EqualTo("шестьдесят три года"));
        That(AgeDescription(44), Is.EqualTo("сорок четыре года"));
        That(AgeDescription(35), Is.EqualTo("тридцать пять лет"));
        That(AgeDescription(27), Is.EqualTo("двадцать семь лет"));

        Throws<ArgumentOutOfRangeException>(() => AgeDescription(-1));
        Throws<ArgumentOutOfRangeException>(() => AgeDescription(0));
        Throws<ArgumentOutOfRangeException>(() => AgeDescription(19));
        Throws<ArgumentOutOfRangeException>(() => AgeDescription(70));
        Throws<ArgumentOutOfRangeException>(() => AgeDescription(1337));
    }

    [Test]
    public void MainTest()
    {
        That(Main(Array.Empty<string>()), Is.EqualTo(-1));
        That(Main(new[] { "some-arg", "other-arg" }), Is.EqualTo(-1));
        That(Main(new[] { "unknown-arg" }), Is.EqualTo(-1));

        // Проверять что CLI выводит в консоль я не буду.
        // Это не функционал API, это функционал UI, и его unit-тестирование 
        // не только усложнит код тестов и не даст никакого прироста в качестве тестов,
        // но и усложнит поддержку тестов в будущем.
    }
}
