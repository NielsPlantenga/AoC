using System.Data;

namespace Baksoft.AoC;

public static class Day5
{
    public static void Part1(string input)
    {
        string[] ins = input.Split("\n\n");
        string[] stackings = ins[0].Split('\n');
        Stack<char>[] stacks = new Stack<char>[9].Select(s => new Stack<char>()).ToArray();
        var moves = ins[1].Trim().Split('\n').Select(m => m.Split(' '))
            .Select(ms => new[] { int.Parse(ms[1]), int.Parse(ms[3])-1, int.Parse(ms[5])-1 }).ToList();

        for (int i = stackings.Length-2; i >= 0; i--)
        {
            for (int j = 1; j < stackings[0].Length; j+=4)
            {
                if(stackings[i][j] != ' ') stacks[j/4].Push(stackings[i][j]);
            }
        }

        foreach (int[] move in moves)
        {
            stacks[move[1]].Move(stacks[move[2]], move[0]);
        }

        Console.WriteLine(stacks.Select(s => s.Pop()).ToArray());
    }

    public static void Part2(string input)
    {
        string[] ins = input.Split("\n\n");
        string[] stackings = ins[0].Split('\n');
        Stack<char>[] stacks = new Stack<char>[9].Select(s => new Stack<char>()).ToArray();
        var moves = ins[1].Trim().Split('\n').Select(m => m.Split(' '))
            .Select(ms => new[] { int.Parse(ms[1]), int.Parse(ms[3])-1, int.Parse(ms[5])-1 }).ToList();

        for (int i = stackings.Length-2; i >= 0; i--)
        {
            for (int j = 1; j < stackings[0].Length; j+=4)
            {
                if(stackings[i][j] != ' ') stacks[j/4].Push(stackings[i][j]);
            }
        }

        foreach (int[] move in moves)
        {
            stacks[move[1]].MoveMultiple(stacks[move[2]], move[0]);
        }

        Console.WriteLine(stacks.Select(s => s.Pop()).ToArray());
    }

    private static void Move(this Stack<char> from, Stack<char> to, int count)
    {
        for (int i = 0; i < count; i++)
        {
            to.Push(from.Pop());
        }
    }

    private static void MoveMultiple(this Stack<char> from, Stack<char> to, int count)
    {
        Stack<char> crane = new Stack<char>();
        for (int i = 0; i < count; i++)
        {
            crane.Push(from.Pop());
        }

        foreach (char c in crane)
        {
            to.Push(c);
        }
    }
}
