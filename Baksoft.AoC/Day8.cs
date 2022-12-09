namespace Baksoft.AoC;

public class Day8
{
    private Tree[][] trees;
    public void Part1(string input)
    {
        trees = input.Trim().Split('\n').Select(r => r.Select(t => new Tree() { Height = t - '0' }).ToArray())
            .ToArray();
        int visibleTrees = 0;

        for (int i = 0; i < trees.Length; i++)
        {
            for (int j = 0; j < trees[i].Length; j++)
            {
                Scan(i, j);
                if (trees[i][j].Visible) visibleTrees++;
            }
        }

        Console.WriteLine(visibleTrees);
    }

    private void Scan(int i, int j)
    {
        Tree tree = trees[i][j];

        if (i == 0)
        {
            tree.VisN = true;
            tree.scN = 1;
        }
        else
        {
            tree.VisN = trees[..i].Select(t => t[j].Height).Max() < tree.Height;
            tree.scN = trees[..i].Select(t => t[j].Height).Reverse().TakeWhile(t => t < tree.Height).Count()+1;
        }
        //north
        tree.VisN = i == 0 || trees[..i].Select(t => t[j].Height).Max() < tree.Height;
        tree.scN =
            i == 0 ? 1 : trees[..i].Select(t => t[j].Height).Reverse().TakeWhile(t => t < tree.Height).Count()+1;
        if (i != 0 && tree.VisN) tree.scN--;

        //west
        tree.VisW = j == 0 || trees[i][..j].Select(t => t.Height).Max() < tree.Height;
        tree.scW =
            j == 0 ? 1 : trees[i][..j].Select(t => t.Height).Reverse().TakeWhile(t => t < tree.Height).Count()+1;
        if (j != 0 && tree.VisW) tree.scW--;

        //south
        tree.VisS = i == trees.Length - 1 || trees[(i+1)..].Select(t => t[j].Height).Max() < tree.Height;
        tree.scS = i == trees.Length - 1 ? 1 : trees[(i+1)..].Select(t => t[j].Height).TakeWhile(t => t < tree.Height).Count()+1;
        if (i != trees.Length - 1 && tree.VisS) tree.scS--;

        //east
        tree.VisE = j == trees[i].Length - 1 || trees[i][(j+1)..].Select(t => t.Height).Max() < tree.Height;
        tree.scE = j == trees[i].Length - 1 ? 1 : trees[i][(j+1)..].Select(t => t.Height).TakeWhile(t => t < tree.Height).Count()+1;
        if (j != trees[i].Length - 1 && tree.VisE) tree.scS--;

        tree.Visible = tree.VisW || tree.VisE || tree.VisN || tree.VisS;
        tree.scT = tree.scW * tree.scE * tree.scN * tree.scS;
    }

    private bool LineOfSight(Tree[][] line, int j, int h)
    {
        return line.Select(t => t[j].Height).Max() < h;
    }

    private bool LineOfSight(Tree[] line, int h)
    {
        return line.Select(t => t.Height).Max() < h;
    }

    public void Part2(string input)
    {
        trees = input.Trim().Split('\n').Select(r => r.Select(t => new Tree() { Height = t - '0' }).ToArray())
            .ToArray();

        for (int i = 0; i < trees.Length; i++)
        {
            for (int j = 0; j < trees[i].Length; j++)
            {
                Scan(i, j);
                if (trees[i][j].Visible) ;
            }
        }

        int t = trees.SelectMany(r => r).Max(x => x.scT);
        Console.WriteLine(t);
    }
}

public class Tree
{
    public int Height { get; set; }
    public bool VisN { get; set; }
    public bool VisE { get; set; }
    public bool VisS { get; set; }
    public bool VisW { get; set; }
    public bool Visible { get; set; }

    public int scN { get; set; }
    public int scE { get; set; }
    public int scS { get; set; }
    public int scW { get; set; }

    public int scT { get; set; }
}
