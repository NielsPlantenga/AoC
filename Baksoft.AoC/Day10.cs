namespace Baksoft.AoC;

public class Day10
{
    public void Part1(string input)
    {
        bool executing = false;
        int x = 1;
        int signal = 0;
        int[] intervals = new[] { 20, 60, 100, 140, 180, 220 };
        Queue<string> prog = new Queue<string>(input.Split('\n'));
        for (int i = 1; i <= 220 ; i++)
        {
            if (intervals.Contains(i))
            {
                signal += i * x;
            }
            if (executing)
            {
                x += int.Parse(prog.Dequeue().Split(' ')[1]);
                executing = false;
                continue;
            }

            switch (prog.Peek())
            {
                case "noop":
                {
                    prog.Dequeue();
                    continue;
                }
                default:
                {
                    executing = true;
                    continue;
                }
            }
        }

        Console.WriteLine(signal);
    }

    public void Part2(string input)
    {
        bool executing = false;
        int x = 1;
        int pos = 0;
        // int[] intervals = new[] { 20, 60, 100, 140, 180, 220 };
        Queue<string> prog = new Queue<string>(input.Split('\n'));
        for (int i = 1; i <= 300 ; i++)
        {
            if (i%40==1)
            {
                Console.WriteLine();
                pos = 0;
            }

            if(pos >= x-1 && pos <= x+1) Console.Write('#');
            else Console.Write('.');

            pos++;

            if (executing)
            {
                x += int.Parse(prog.Dequeue().Split(' ')[1]);
                executing = false;
                continue;
            }

            switch (prog.Peek())
            {
                case "noop":
                {
                    prog.Dequeue();
                    continue;
                }
                default:
                {
                    executing = true;
                    continue;
                }
            }
        }
    }
}
