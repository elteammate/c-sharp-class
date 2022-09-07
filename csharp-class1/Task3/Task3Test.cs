using NUnit.Framework;
using System;
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
        throw new NotImplementedException("Необходимо добавить больше тестов");
    }

    [Test]
    public void MainTest()
    {
        Main(Array.Empty<string>());
    }
}
