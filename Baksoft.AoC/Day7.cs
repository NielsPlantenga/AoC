namespace Baksoft.AoC;

public class Day7
{
    private int i;
    private int sum = 0;
    public void Part1(string input)
    {
        i = 1;
        string[] inp = input.Trim().Split('\n');
        Node root = Traverse(inp, "/");
        Console.WriteLine(sum);
    }

    private Node Traverse(string[] lines, string name)
    {
        Node n = new Node(){Name = name, Nodes = new List<Node>()};
        while (i < lines.Length-1)
        {
            i++;
            string[] line = lines[i].Split(' ');
            switch (line[0])
            {
                case "$":
                {
                    if (line[1] == "cd")
                    {
                        if (line[2] == "..")
                        {
                            if (n.Size < 100000) sum += n.Size;
                            return n;
                        }

                        Node x = Traverse(lines, line[2]);
                        n.Size += x.Size;
                        n.Nodes.Add(x);
                    }

                    continue;
                }
                case "dir":
                    continue;
                default:
                    n.Size += int.Parse(line[0]);
                    break;
            }
        }

        return n;
    }

    public void Part2(string input)
    {
        i = 1;
        string[] inp = input.Trim().Split('\n');
        Node root = Traverse(inp, "/");
        int neededSpace = 30000000 - (70000000 - root.Size);
        int min = Traverse(root, neededSpace);
        Console.WriteLine(min);
    }

    private static int Traverse(Node n, int target)
    {
        if (n.Size < target) return int.MaxValue;
        if (!n.Nodes.Any()) return n.Size;
        int m = n.Nodes.Select(x => Traverse(x, target)).Min();
        return n.Size < m ? n.Size : m;
    }
}

public class Node
{
    public int Size { get; set; }
    public string Name { get; set; }
    public List<Node> Nodes { get; set; }
}
