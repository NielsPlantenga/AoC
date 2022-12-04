namespace Baksoft.AoC;

public static class Day4
{
    public static void Part1(string input)
    {
        Console.WriteLine(
            input.Trim()
                .Split('\n')
                .Select(pair => pair.Split(',').Select(a => a.Split('-').Select(int.Parse).ToArray()).ToArray())
                .Count(pair => pair[0][0] <= pair[1][0] && pair[0][1] >= pair[1][1] || pair[0][0] >= pair[1][0] && pair[0][1] <= pair[1][1])
        );
    }

    public static void Part2(string input)
    {
        Console.WriteLine(
            input.Trim()
                .Split('\n')
                .Select(pair => pair.Split(',').Select(a => a.Split('-').Select(int.Parse).ToArray()).ToArray())
                .Count(pair => !(pair[0][0] < pair[1][0] && pair[0][1] < pair[1][0] || pair[0][0] > pair[1][1] && pair[0][1] > pair[1][1]))
        );
    }
}
