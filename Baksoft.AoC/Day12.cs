using System.Collections;

namespace Baksoft.AoC;

public class Day12
{
    public void Part1(string input)
    {
        char[][] grid = input.Trim().Split('\n').Select(s => s.ToArray()).ToArray();
        PriorityQueue<Square, int> nodes = new ();
        int startX = Array.FindIndex(grid, c => c.Contains('S'));
        int startY = Array.FindIndex(grid[startX], p => p == 'S');
        int goalX = Array.FindIndex(grid, c => c.Contains('E'));
        int goalY = Array.FindIndex(grid[goalX], p => p == 'E');
        grid[startX][startY] = 'a';
        grid[goalX][goalY] = 'z';

        Square square = new Square(startX, startY, new List<Square>(), grid, goalX, goalY);
        nodes.Enqueue(square, square.heuristic);

        while (true)
        {
            Square node = nodes.Dequeue();
            if (node.X == goalX && node.Y == goalY)
            {
                Console.WriteLine(node.Path.Count);
                break;
            }

            node.Expand(nodes, grid, goalX, goalY);
            // nodes = new Queue<Square>(nodes.OrderBy(n => n.heuristic));
            // Console.WriteLine(nodes.Count);
            Console.WriteLine($"{node.X},{node.Y}, {node.Path.Count}, {grid[node.X][node.Y]}");
        }

    }


    public int Search(char[][] grid, int goalX, int goalY, int startX, int startY)
    {
        PriorityQueue<Square, int> nodes = new ();

        Square square = new Square(startX, startY, new List<Square>(), grid, goalX, goalY);
        nodes.Enqueue(square, square.heuristic);

        while (nodes.Count != 0)
        {
            Square node = nodes.Dequeue();
            if (node.X == goalX && node.Y == goalY)
            {
                Console.WriteLine(node.Path.Count);
                return node.Path.Count;
                // break;
            }

            node.Expand(nodes, grid, goalX, goalY);
            // nodes = new Queue<Square>(nodes.OrderBy(n => n.heuristic));
            // Console.WriteLine(nodes.Count);
            // Console.WriteLine($"{node.X},{node.Y}, {node.Path.Count}, {grid[node.X][node.Y]}");
        }

        return int.MaxValue;
    }

    public void Part2(string input)
    {
        char[][] grid = input.Trim().Split('\n').Select(s => s.ToArray()).ToArray();
        int startX = Array.FindIndex(grid, c => c.Contains('S'));
        int startY = Array.FindIndex(grid[startX], p => p == 'S');
        int goalX = Array.FindIndex(grid, c => c.Contains('E'));
        int goalY = Array.FindIndex(grid[goalX], p => p == 'E');
        grid[startX][startY] = 'a';

        int lowest = int.MaxValue;

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] != 'a') continue;
                char[][] newGrid = grid.Select(r => r.ToArray()).ToArray();
                int result = Search(newGrid, goalX, goalY, i, j);
                lowest = Math.Min(lowest, result);
            }
        }

        Console.WriteLine(lowest);
    }
}

public class Square
{
    public int X { get; set; }
    public int Y { get; set; }
    public List<Square> Path { get; set; }
    public int heuristic { get; set; }

    public Square(int x, int y, List<Square> path, char[][] grid, int goalX, int goalY)
    {
        X = x;
        Y = y;
        Path = path;
        heuristic = Math.Abs(X - goalX) + Math.Abs(Y - goalY) + Path.Count;
    }

    public bool Expand(PriorityQueue<Square, int> nodes, char[][] grid, int goalX, int goalY)
    {
        if (grid[X][Y] == '|') return false;

        // if (Path.Any(s => s.X == X && s.Y == Y)) return false;

        //left
        if (X > 0 && grid[X][Y] - grid[X-1][Y] >= -1)
        {
            List<Square> newPath = Path.ToList();
            newPath.Add(this);
            Square sq = new Square(X - 1, Y, newPath, grid, goalX, goalY);
            nodes.Enqueue(sq, sq.heuristic);
        }
        //right
        if (X < grid.Length - 1 && grid[X][Y] - grid[X + 1][Y] >= -1)
        {
            List<Square> newPath = Path.ToList();
            newPath.Add(this);
            Square sq = new Square(X + 1, Y, newPath, grid, goalX, goalY);
            nodes.Enqueue(sq, sq.heuristic);
        }
        //down
        if (Y > 0 && grid[X][Y] - grid[X][Y-1] >= -1)
        {
            List<Square> newPath = Path.ToList();
            newPath.Add(this);
            Square sq = new Square(X, Y-1, newPath, grid, goalX, goalY);
            nodes.Enqueue(sq, sq.heuristic);
        }
        //up
        if (Y < grid[0].Length - 1 && grid[X][Y] - grid[X][Y+1] >= -1)
        {
            List<Square> newPath = Path.ToList();
            newPath.Add(this);
            Square sq = new Square(X, Y+1, newPath, grid, goalX, goalY);
            nodes.Enqueue(sq, sq.heuristic);
        }

        grid[X][Y] = '|';

        return false;
    }
}
