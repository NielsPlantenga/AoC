namespace Baksoft.AoC;

public class Day14
{
    public void Part1(string input)
    {
        string[] ridges = input.Trim().Split('\n');
        int maxX = ridges.SelectMany(r => r.Split(" -> ").Select(p => int.Parse(p.Split(',')[0]))).Max();
        int maxY = ridges.SelectMany(r => r.Split(" -> ").Select(p => int.Parse(p.Split(',')[1]))).Max();
        // int minX = ridges.SelectMany(r => r.Split(" -> ").Select(p => int.Parse(p.Split(',')[0]))).Min();
        // int minY = ridges.SelectMany(r => r.Split(" -> ").Select(p => int.Parse(p.Split(',')[1]))).Min();
        char[,] grid = new char[maxX+1, maxY+1];

        foreach (string ridge in ridges)
        {
            (int, int)[] points = ridge.Split(" -> ").Select(p =>
            {
                string[] s = p.Split(',');
                return (int.Parse(s[0]), int.Parse(s[1]));
            }).ToArray();
            for (int i = 0; i < points.Length -1; i++)
            {
                DrawPath(points[i].Item1, points[i].Item2, points[i+1].Item1, points[i+1].Item2, grid);
            }
        }

        int units = 0;

        while (Sink(500, 0, grid))
        {
            units++;
        }

        Console.WriteLine(units);
    }

    private bool Sink(int x, int y, char[,] grid)
    {
        if (y == grid.GetLength(1)-1) return false;

        if (grid[x, y + 1] == '\0') return Sink(x, y + 1, grid);

        if (grid[x - 1, y + 1] == '\0') return Sink(x - 1, y + 1, grid);

        if (grid[x + 1, y + 1] == '\0') return Sink(x + 1, y + 1, grid);

        grid[x, y] = 'O';
        return true;
    }

    private void DrawPath(int sX, int sY, int eX, int eY, char[,] grid)
    {
        if (sX != eX)
        {
            if (eX < sX) (sX, eX) = (eX, sX);

            for (int i = sX; i <= eX; i++)
            {
                grid[i, sY] = '#';
            }

            return;
        }

        if (eY < sY) (sY, eY) = (eY, sY);

        for (int i = sY; i <= eY; i++)
        {
            grid[sX, i] = '#';
        }
    }

    public void Part2(string input)
    {
        string[] ridges = input.Trim().Split('\n');
        int maxX = ridges.SelectMany(r => r.Split(" -> ").Select(p => int.Parse(p.Split(',')[0]))).Max();
        int maxY = ridges.SelectMany(r => r.Split(" -> ").Select(p => int.Parse(p.Split(',')[1]))).Max();
        // int minX = ridges.SelectMany(r => r.Split(" -> ").Select(p => int.Parse(p.Split(',')[0]))).Min();
        // int minY = ridges.SelectMany(r => r.Split(" -> ").Select(p => int.Parse(p.Split(',')[1]))).Min();
        char[,] grid = new char[maxX*2+1, maxY+2];

        foreach (string ridge in ridges)
        {
            (int, int)[] points = ridge.Split(" -> ").Select(p =>
            {
                string[] s = p.Split(',');
                return (int.Parse(s[0]), int.Parse(s[1]));
            }).ToArray();
            for (int i = 0; i < points.Length -1; i++)
            {
                DrawPath(points[i].Item1, points[i].Item2, points[i+1].Item1, points[i+1].Item2, grid);
            }
        }

        int units = 0;

        while (grid[500,0] == '\0' || grid[499,1] == '\0' || grid[501,1] == '\0')
        {
            Sink2(500, 0, grid);
            units++;
        }

        Console.WriteLine(units);
    }

    public void Draw(int minX, int maxX, char[,] grid)
    {
        for (int j = 0; j < grid.GetLength(1); j++)
        {
            for (int i = minX; i < maxX; i++)
            {
                Console.Write(grid[i, j] == '\0' ? '.' : grid[i,j]);
            }

            Console.WriteLine();
        }
    }

    private bool Sink2(int x, int y, char[,] grid)
    {
        if (y == grid.GetLength(1) - 1)
        {
            grid[x, y] = 'O';
            return false;
        }

        if (grid[x, y + 1] == '\0') return Sink2(x, y + 1, grid);

        if (grid[x - 1, y + 1] == '\0') return Sink2(x - 1, y + 1, grid);

        if (grid[x + 1, y + 1] == '\0') return Sink2(x + 1, y + 1, grid);

        grid[x, y] = 'O';
        return true;
    }
}
