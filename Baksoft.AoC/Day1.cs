namespace Baksoft.AoC;

public static class Day1
{
    public static void Part1(string input)
    {
        Console.WriteLine(input.Split("\n\n").Select(elf => elf.Trim().Split('\n').Select(int.Parse).Sum()).Max());
    }

    public static void Part2(string input)
    {
        Console.WriteLine(input.Split("\n\n").Select(elf => elf.Trim().Split('\n').Select(int.Parse).Sum())
            .OrderByDescending(elf => elf).Take(3).Sum());
    }
}
