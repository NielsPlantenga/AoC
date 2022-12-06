namespace Baksoft.AoC;

public static class Day6
{
    public static void Part1(string input)
    {
        string inp = input.Trim();

        for (int i = 0; i < inp.Length; i++)
        {
            if (inp.Substring(i, 4).Distinct().ToList().Count == 4)
            {
                Console.WriteLine(i+4);
                break;
            }
        }
    }

    public static void Part2(string input)
    {
        string inp = input.Trim();

        for (int i = 0; i < inp.Length; i++)
        {
            if (inp.Substring(i, 14).Distinct().ToList().Count == 14)
            {
                Console.WriteLine(i+14);
                break;
            }
        }
    }
}
