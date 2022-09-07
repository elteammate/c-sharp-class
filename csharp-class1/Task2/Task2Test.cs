using NUnit.Framework;
using System;
using static NUnit.Framework.Assert;
using static Task2.Task2;

public class Tests
{
    [Test]
    public void Min3Test1()
    {
        That(Min3(2, 0, 3), Is.EqualTo(0));
    }

    [Test]
    public void Min3Test2()
    {
        That(Min3(1, 1, 1), Is.EqualTo(1));
    }

    [Test]
    public void Min3Test3()
    {
        That(Min3(-420, -69, 1337), Is.EqualTo(-420));
    }

    [Test]
    public void Min3Test4()
    {
        That(Min3(3, 2, 1), Is.EqualTo(1));
    }

    [Test]
    public void Max3Test1()
    {
        That(Max3(2, 0, 3), Is.EqualTo(3));
    }

    [Test]
    public void Max3Test2()
    {
        That(Max3(-420, -69, -1337), Is.EqualTo(-69));
    }

    [Test]
    public void Max3Test3()
    {
        That(Max3(1, 1, 1), Is.EqualTo(1));
    }

    [Test]
    public void Max3Test4()
    {
        That(Max3(0, -1, -2), Is.EqualTo(0));
    }

    [Test]
    public void Deg2RadTest1()
    {
        That(Deg2Rad(180.0), Is.EqualTo(Math.PI).Within(1e-5));
        That(Deg2Rad(2 * 360 + 180.0), Is.EqualTo(5 * Math.PI).Within(1e-5));
    }

    [Test]
    public void Rad2DegTest1()
    {
        That(Rad2Deg(Math.PI), Is.EqualTo(180.0).Within(1e-5));
        That(Rad2Deg(5 * Math.PI), Is.EqualTo(5 * 180.0).Within(1e-5));
    }

    [Test]
    public void MoreRadDegTests()
    {
        That(Rad2Deg(0.0), Is.EqualTo(0.0).Within(1e-5));
        That(Rad2Deg(0.0), Is.EqualTo(0.0).Within(1e-5));

        That(Deg2Rad(360.0), Is.EqualTo(Math.PI * 2).Within(1e-5));
        That(Rad2Deg(Math.PI * 2), Is.EqualTo(360.0).Within(1e-5));

        That(Deg2Rad(30), Is.EqualTo(Math.PI * (1.0 / 6.0)).Within(1e-5));
        That(Rad2Deg(Math.PI * (1.0 / 6.0)), Is.EqualTo(30).Within(1e-5));

        // https://www.google.com/search?q=4.08617164889+rad+to+deg
        const double phi1Deg = 234.12038984753;
        const double phi1Rad = 4.08617164889;

        // https://www.google.com/search?q=654.1348971662345+rad+to+deg
        const double phi2Deg = 37479.168839942321938;
        const double phi2Rad = 654.1348971662345;

        That(Deg2Rad(phi1Deg), Is.EqualTo(phi1Rad).Within(1e-5));
        That(Rad2Deg(phi1Rad), Is.EqualTo(phi1Deg).Within(1e-5));

        That(Deg2Rad(-phi2Deg), Is.EqualTo(-phi2Rad).Within(1e-5));
        That(Rad2Deg(-phi2Rad), Is.EqualTo(-phi2Deg).Within(1e-5));
    }
}
