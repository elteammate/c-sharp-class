using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task1.Task1;

namespace Task1;

public class Tests
{
    [Test]
    public void IPv4AddrTest()
    {
        That(new IpV4Addr("127.0.0.1").IntValue, Is.EqualTo(2130706433u));
        That(new IpV4Addr("0.0.0.1").IntValue, Is.EqualTo(1u));
        That(new IpV4Addr("1.1.1.1").IntValue, Is.EqualTo(16843009u));
        That(new IpV4Addr("255.255.255.255").IntValue, Is.EqualTo(4294967295u));
    }

    [Test]
    public void CompareIPv4AddrTest()
    {
        That(new IpV4Addr("127.0.0.1"), Is.LessThan(new IpV4Addr("127.0.0.2")));
        That(new IpV4Addr("127.0.0.0"), Is.GreaterThan(new IpV4Addr("126.255.255.255")));
    }

    [Test]
    public void ParseArgsTest()
    {
        That(
            ParseArgs(new[]
                    { "../../../data/query.ips", "../../../data/1.iprs", "../../../data/2.iprs" })!
                .IpsFile,
            Is.EqualTo("../../../data/query.ips")
        );
        That(
            ParseArgs(new[]
                    { "../../../data/query.ips", "../../../data/1.iprs", "../../../data/2.iprs" })!
                .IprsFiles,
            Is.EqualTo(new List<string> { "../../../data/1.iprs", "../../../data/2.iprs" })
        );
        That(ParseArgs(new[] { "../../../data/query.ips" }), Is.Null);
        That(ParseArgs(Array.Empty<string>()), Is.Null);
    }

    [Test]
    public void LoadQueryTest()
    {
        That(LoadQuery("../../../data/query.ips").ToList(), Has.Count.EqualTo(5));
    }

    [Test]
    public void LoadRangesTest()
    {
        var ranges = LoadRanges(new List<string>
            { "../../../data/1.iprs", "../../../data/2.iprs" });
        foreach (var (r1, r2) in ranges.Zip(ranges.Skip(1)))
        {
            That(r1.IpTo, Is.LessThan(r2.IpTo));
            That(r1.IpFrom, Is.LessThan(r2.IpFrom));
        }
    }

    [Test]
    public void LoadRangesEmptyTest()
    {
        var ranges = LoadRanges(new List<string>());
        That(FindRange(ranges, new IpV4Addr("60.161.226.166")), Is.Null);
    }

    [Test]
    public void LoadFindRangeTest()
    {
        var ranges = LoadRanges(new List<string> { "../../../data/1.iprs" });
        That(FindRange(ranges, new IpV4Addr("60.161.226.166")), Is.Not.Null);
        That(FindRange(ranges, new IpV4Addr("127.0.0.1")), Is.Null);
    }
}
