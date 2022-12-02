namespace Baksoft.AoC;

public static class Day2
{
    public static void Part1(string input)
    {
        Console.WriteLine(input.Trim().Split('\n').Select(round =>
        {
            string[] hands = round.Split(' ');
            int opp = (int) hands[0][0];
            int play = (int) hands[1][0];
            return (opp - (play - 23)) switch
            {
                -2 => play - 87,
                -1 => 6 + play -87,
                0 => 3 + play -87,
                1 => play -87,
                2 => 6 + play -87,
                _ => 0
            };
        }).Sum());
    }

    public static void Part2(string input)
    {
        Console.WriteLine(input.Trim().Split('\n').Select(round =>
        {
            string[] hands = round.Split(' ');
            int opp = (int) hands[0][0] - 64;
            int play = (int) hands[1][0] - 89;
            return (play) switch
            {
                -1 => opp-1 < 1 ? 3 : opp-1,
                0 => 3 + opp,
                1 => 6 + (opp+1 > 3 ? 1 : opp+1),
                _ => 0
            };
        }).Sum());
    }
}
