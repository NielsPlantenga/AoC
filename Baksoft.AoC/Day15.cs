namespace Baksoft.AoC;

public class Day15
{
    public void Part1(string input)
    {
        int rowId = 2000000;
        List<Sensor> sensors = input.Trim().Split('\n').Select(s =>
        {
            Sensor sn = new Sensor
            {
                X = int.Parse(s[12..].Split(',')[0]),
                Y = int.Parse(s.Split(", y=")[1].Split(':')[0]),
                Beacon = (int.Parse(s.Split(':')[1].Split("x=")[1].Split(',')[0]),
                    int.Parse(s.Split(':')[1].Split("y=")[1]))
            };
            sn.Range = Math.Abs(sn.X - sn.Beacon.Item1) + Math.Abs(sn.Y - sn.Beacon.Item2);
            return sn;
        }).ToList();

        IEnumerable<Sensor> aSensors = sensors.Where(s => Enumerable.Range(s.Y - s.Range, s.Range*2+1).Contains(rowId));
        Dictionary<int, char> row = new Dictionary<int, char>();

        foreach (Sensor s in aSensors)
        {
            int xRange = s.Range - Math.Abs(s.Y - rowId);
            IEnumerable<int> xR = s.X - xRange <= s.X + xRange
                ? Enumerable.Range(s.X - xRange, xRange*2+1)
                : Enumerable.Range(s.X + xRange, xRange*2+1);
            foreach (int i in xR)
            {
                row[i] = '#';
            }
        }

        foreach (Sensor s in sensors)
        {
            if (s.Y == rowId) row[s.X] = 'S';
            if (s.Beacon.Item2 == rowId) row[s.Beacon.Item1] = 'B';
        }

        Console.WriteLine(row.Keys.Count(k => row[k] == '#'));
    }

    public void Part2(string input)
    {
        List<Sensor> sensors = input.Trim().Split('\n').Select(s =>
        {
            Sensor sn = new Sensor
            {
                X = int.Parse(s[12..].Split(',')[0]),
                Y = int.Parse(s.Split(", y=")[1].Split(':')[0]),
                Beacon = (int.Parse(s.Split(':')[1].Split("x=")[1].Split(',')[0]),
                    int.Parse(s.Split(':')[1].Split("y=")[1]))
            };
            sn.Range = Math.Abs(sn.X - sn.Beacon.Item1) + Math.Abs(sn.Y - sn.Beacon.Item2);
            return sn;
        }).ToList();

        const int maxIds = 4000000;
        for (int i = 0; i <= maxIds; i++)
        {
            int i1 = i;
            IOrderedEnumerable<(int, int)> ls = sensors.Select(s =>
            {
                int rX = s.Range - (s.Y < 0 ? Math.Abs(i1 + s.Y) : Math.Abs(i1 - s.Y));
                if (rX < 0) return (0, 0);
                int xMin = s.X - rX;
                int xMax = s.X + rX;
                return (xMin < 0 ? 0 : xMin, xMax > maxIds ? maxIds : xMax);
            }).OrderBy(r => r.Item1).ThenBy(r => r.Item2);
            int xMin = 0;
            int xMax = 0;
            foreach ((int x1, int x2)  in ls)
            {
                if (x1-1 > xMax)
                {
                    long result = (long)(x1 - 1) * (long)4000000;
                    Console.WriteLine(result+i);
                    break;
                }
                xMax = Math.Max(xMax, x2);
            }
        }
    }
}

public class Sensor
{
    public int X { get; set; }
    public int Y { get; set; }
    public (int, int) Beacon { get; set; }
    public int Range { get; set; }
}
