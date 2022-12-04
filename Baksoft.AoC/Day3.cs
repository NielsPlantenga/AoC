namespace Baksoft.AoC;

public static class Day3
{
    public static void Part1(string input)
    {
        Console.WriteLine(
            input.Trim()
                .Split('\n')
                .Select(r => new[] {r[..(r.Length/2)], r[(r.Length/2)..]})
                .Select(r => r[0].Intersect(r[1]).First())
                .Sum(c => c < 'a' ? c - 'A' + 27 : c - 'a' + 1)
        );
    }

    public static void Part2(string input)
    {
        string[] sacks = input.Trim().Split('\n');
        int x = 0;
        for (int i = 0; i < sacks.Length; i += 3)
        {
            char c = sacks[i].Intersect(sacks[i + 1]).Intersect(sacks[i + 2]).First();
            x += c < 'a' ? c - 'A' + 27 : c - 'a' + 1;
        }
        Console.WriteLine(x);
    }
}
