namespace Baksoft.AoC;

public class Day9
{
    private List<List<bool>> grid;

    private List<Knot> knots;
    public void Part1(string input)
    {
        grid = new List<List<bool>> { new List<bool>{false} };
        IEnumerable<Move> moves = input.Trim().Split('\n').Select(s => new Move() { Dir = s[0], Steps = int.Parse(s[2..])});
        knots = new List<Knot>(Enumerable.Repeat(0, 2).Select(_ => new Knot()));
        grid[0][0] = true;

        foreach (Move m in moves)
        {
            performMove2(m);
        }

        Console.WriteLine(grid.SelectMany(col => col).Count(p => p));
    }

    public void Part2(string input)
    {
        grid = new List<List<bool>> { new List<bool>{false} };
        IEnumerable<Move> moves = input.Trim().Split('\n').Select(s => new Move() { Dir = s[0], Steps = int.Parse(s[2..])});
        knots = new List<Knot>(Enumerable.Repeat(0, 10).Select(_ => new Knot()));
        grid[knots[0].X][knots[0].Y] = true;

        foreach (Move m in moves)
        {
            performMove2(m);
        }

        Console.WriteLine(grid.SelectMany(col => col).Count(p => p));
    }

    private void performMove2(Move m)
    {
        int diff = 0;
        int i1 = knots[0].X;
        int i2 = knots[0].Y;
        switch (m.Dir)
        {
            case 'L':
                if (i1 - m.Steps < 0)
                {
                    diff = Math.Abs(i1 - m.Steps);
                    foreach (Knot knot in knots)
                    {
                        knot.X+= diff;
                    }
                    for (int k = 0; k < diff; k++)
                    {
                        grid = grid.Prepend(grid[0].Select(_ => false).ToList()).ToList();
                    }
                }

                break;
            case 'U':
                if (i2 + m.Steps >= grid[0].Count)
                {
                    diff = i2 + m.Steps + 1 - grid[0].Count;
                    for (int k = 0; k < diff; k++)
                    {
                        foreach (List<bool> col in grid)
                        {
                            col.Add(false);
                        }
                    }
                }
                break;
            case 'R':
                if (i1 + m.Steps >= grid.Count)
                {
                    diff = i1 + m.Steps + 1 - grid.Count;
                    for (int k = 0; k < diff; k++)
                    {
                        grid.Add(grid[0].Select(_ => false).ToList());
                    }
                }
                break;
            case 'D':
                if (i2 - m.Steps < 0)
                {
                    diff = Math.Abs(i2 - m.Steps);
                    foreach (Knot knot in knots)
                    {
                        knot.Y+= diff;
                    }
                    for (int k = 0; k < diff; k++)
                    {
                        grid = grid.Select(col => col.Prepend(false).ToList()).ToList();
                    }
                }
                break;
        }

        for (int k = 0; k < m.Steps; k++)
        {
            Step(m.Dir);
        }
    }

    private void SnakeStep(Knot h, Knot t)
    {
        int cx = t.X;
        int cy = t.Y;

        if (Math.Abs(cx - h.X) == 2 && Math.Abs(cy - h.Y) == 2)
        {
            t.X += (h.X-cx) / 2;
            t.Y += (h.Y-cy) / 2;
            return;
        }
        if (Math.Abs(cx - h.X) == 2)
        {
            t.Y = h.Y;
            t.X += (h.X-cx) / 2;
            return;
        }

        if (Math.Abs(cy - h.Y) != 2) return;
        t.X = h.X;
        t.Y += (h.Y-cy) / 2;
    }

    private void Step(char dir)
    {
        switch (dir)
        {
            case 'L':
                knots[0].X--;
                break;
            case 'U':
                knots[0].Y++;
                break;
            case 'R':
                knots[0].X++;
                break;
            case 'D':
                knots[0].Y--;
                break;
        }

        for (int k = 1; k < knots.Count; k++)
        {
            SnakeStep(knots[k-1], knots[k]);
        }

        grid[knots.Last().X][knots.Last().Y] = true;
    }
}

public class Knot
{
    public int X;
    public int Y;
}

public class Move
{
    public char Dir { get; set; }
    public int Steps { get; set; }
}
