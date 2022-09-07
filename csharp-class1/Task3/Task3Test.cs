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
        throw new NotImplementedException("Необходимо добавить больше тестов");
    }

    [Test]
    public void Rotate2Test()
    {
        That(Rotate2('С', 1, -1), Is.EqualTo('С'));
        throw new NotImplementedException("Необходимо добавить больше тестов");
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
