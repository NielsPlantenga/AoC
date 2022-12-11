namespace Baksoft.AoC;

public class Day11
{
    public void Part1(string input)
    {
        List<Monkey> monkeys = input.Trim().Split("\n\n").Select(s =>
        {
            string[] lines = s.Split('\n');
            bool badMonkey = int.TryParse(lines[2].Split(' ').Last(), out int factor);
            return new Monkey()
            {
                Items = lines[1].Split(':')[1].Split(',').Select(n => UInt64.Parse(n.Trim())).ToList(),
                Factor = factor,
                Worrisome = lines[2].Contains('*'),
                Worrisomst = !badMonkey,
                Test = int.Parse(lines[3].Split(' ').Last()),
                TrueMonkey = int.Parse(lines[4].Split(' ').Last()),
                FalseMonkey = int.Parse(lines[5].Split(' ').Last())
            };
        }).ToList();

        for (int i = 0; i < 10000; i++)
        {
            foreach (Monkey monkey in monkeys)
            {
                monkey.Turn(monkeys);
            }
        }

        Monkey[] max = monkeys.OrderByDescending(m => m.Throws).Take(2).ToArray();
        Console.WriteLine((ulong)max[0].Throws * (ulong)max[1].Throws);
    }

    //Part 2 uses a different method of Monkey
    public void Part2(string input)
    {
    }
}

public class Monkey
{
    private int divisor = 9699690;
    public List<UInt64> Items { get; set; }
    public int Factor { get; set; }
    public bool Worrisome { get; set; }
    public bool Worrisomst { get; set; }
    public int Test { get; set; }
    public int TrueMonkey { get; set; }
    public int FalseMonkey { get; set; }
    public int Throws { get; set; }

    private UInt64 Inspect(UInt64 item)
    {
        if (Worrisomst) return (item * item)/3;
        return (Worrisome ? item * (ulong)Factor : item + (ulong)Factor)/3;
    }

    private UInt64 Inspect2(UInt64 item)
    {
        item = item % (ulong)divisor;
        if (Worrisomst) return (item * item);
        return (Worrisome ? item * (ulong)Factor : item + (ulong)Factor);
    }

    private void Throw(List<Monkey> monkeys, UInt64 item)
    {
        if(item % (ulong)Test ==0) monkeys[TrueMonkey].Items.Add(item);
        else monkeys[FalseMonkey].Items.Add(item);
        Throws++;
    }

    public void Turn(List<Monkey> monkeys)
    {
        foreach (UInt64 item in Items.Select(Inspect2))
        {
            Throw(monkeys, item);
        }

        Items.Clear();
    }
}
