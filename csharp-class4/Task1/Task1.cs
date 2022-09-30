using System.Collections.Immutable;

namespace Task1;

using IPRangesDatabase = ImmutableArray<Task1.IpRange>;

public static class Task1
{
    internal record IpV4Addr(string StrValue) : IComparable<IpV4Addr>
    {
        internal readonly uint IntValue = IpStr2Int(StrValue);

        // Благодаря этому методу мы можем сравнивать два значения IPv4Addr
        private static uint IpStr2Int(string strValue)
        {
            var chunks = strValue.Split('.').Select(uint.Parse).ToArray();
            return (chunks[0] << 24) + (chunks[1] << 16) + (chunks[2] << 8) + chunks[3];
        }

        public int CompareTo(IpV4Addr? other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return IntValue.CompareTo(other.IntValue);
        }

        public static bool operator <=(IpV4Addr a, IpV4Addr b) => a.CompareTo(b) <= 0;

        public static bool operator >=(IpV4Addr a, IpV4Addr b) => a.CompareTo(b) >= 0;

        public static bool operator <(IpV4Addr a, IpV4Addr b) => a.CompareTo(b) < 0;

        public static bool operator >(IpV4Addr a, IpV4Addr b) => a.CompareTo(b) > 0;

        public override string ToString() => StrValue;
    }

    internal record IpRange(IpV4Addr IpFrom, IpV4Addr IpTo)
    {
        public override string ToString() => $"{IpFrom},{IpTo}";

        public bool Inside(IpV4Addr addr) => IpFrom <= addr && addr <= IpTo;
    }

    internal record IpLookupArgs(string IpsFile, List<string> IprsFiles);

    internal static IpLookupArgs? ParseArgs(string[] args)
    {
        if (args.Length < 2) return null;
        var ipsFile = args[0];
        var iprsFile = args[1..].ToList();
        return new IpLookupArgs(ipsFile, iprsFile);
    }

    internal static List<IpV4Addr> LoadQuery(string filename) =>
        File.ReadAllLines(filename).Select(addr => new IpV4Addr(addr)).ToList();

    internal static IPRangesDatabase LoadRanges(List<string> filenames)
    {
        // Reading all lines from all files
        var givenList = (
            from filename in filenames
            from line in File.ReadAllLines(filename)
            select line.Split(',')
            into ips
            let ipFrom = new IpV4Addr(ips[0])
            let ipTo = new IpV4Addr(ips[1])
            select new IpRange(ipFrom, ipTo)
        ).ToList();

        // Sorting by increasing order of the first IP address
        // and then by decreasing order of the second IP address
        givenList.Sort((a, b) =>
        {
            var first = a.IpFrom.CompareTo(b.IpFrom);
            return first != 0 ? first : b.IpTo.CompareTo(a.IpTo);
        });

        var size = givenList.Count;

        // Getting all ranges that are included in other ranges
        var keep = new bool[size];
        var right = new IpV4Addr("0.0.0.0");
        for (var i = 0; i < size; i++)
        {
            if (givenList[i].IpTo < right) continue;
            keep[i] = true;
            right = givenList[i].IpTo;
        }

        // Returning only ranges that are not included in other ranges
        return givenList.Where((_, i) => keep[i]).ToImmutableArray();
    }

    internal static IpRange? FindRange(IPRangesDatabase ranges, IpV4Addr query)
    {
        // Perform binary search on ranges database
        var left = 0;
        var right = ranges.Length;
        while (left < right)
        {
            var mid = (left + right) / 2;
            if (ranges[mid].IpFrom > query)
                right = mid;
            else if (ranges[mid].IpTo < query)
                left = mid + 1;
            else
                return ranges[mid];
        }

        return null;
    }

    public static void Main(string[] args)
    {
        var ipLookupArgs = ParseArgs(args);
        if (ipLookupArgs == null) return;

        var queries = LoadQuery(ipLookupArgs.IpsFile);
        var ranges = LoadRanges(ipLookupArgs.IprsFiles);
        foreach (var ip in queries)
        {
            var findRange = FindRange(ranges, ip);
            Console.WriteLine(findRange != null ? $"{ip}: YES({findRange})" : $"{ip}: NO");
        }
    }
}
