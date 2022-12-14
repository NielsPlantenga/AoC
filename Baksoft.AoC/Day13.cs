using System.Text.Json.Nodes;

namespace Baksoft.AoC;

public class Day13 : Comparer<JsonNode>
{
    public void Part1(string input)
    {
        string[] pairs = input.Trim().Split("\n\n");
        int sum = 0;

        for (int i = 0; i < pairs.Length; i++)
        {
            string[] pair = pairs[i].Split('\n');
            JsonNode x = JsonNode.Parse(pair[0]);
            JsonNode y = JsonNode.Parse(pair[1]);

            if (Compare(x, y) == 1) sum += i + 1;
        }
        Console.WriteLine(sum);
    }

    public void Part2(string input)
    {
        input += "[[2]]\n[[6]]";
        IEnumerable<JsonNode?> packets = input.Trim().Split('\n').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => JsonNode.Parse(s));
        List<JsonNode> ordered = new List<JsonNode>(){packets.First()};

        foreach (JsonNode packet in packets.Skip(1))
        {
            bool inserted = false;
            for (int i = 0; i < ordered.Count; i++)
            {
                if (Compare(packet, ordered[i]) == 1)
                {
                    ordered.Insert(i, packet);
                    inserted = true;
                    break;
                }
            }
            if (!inserted) ordered.Add(packet);
        }

        int index2 = ordered.FindIndex(n => n.ToString() == "[\n  [\n    2\n  ]\n]") + 1;
        int index6 = ordered.FindIndex(n => n.ToString() == "[\n  [\n    6\n  ]\n]") + 1;

        Console.WriteLine(index2 * index6);
    }

    public override int Compare(JsonNode left, JsonNode right)
    {
        if (left is JsonArray && right is JsonArray)
        {
            JsonArray l = left.AsArray();
            JsonArray r = right.AsArray();

            for (int i = 0; i < l.Count; i++)
            {
                if (i == r.Count) return 0;
                int result = Compare(l[i], r[i]);
                if (result != 2) return result;
            }

            return l.Count == r.Count ? 2 : 1;
        }

        if (left is JsonArray)
        {
            return Compare(left, new JsonArray(JsonValue.Create(right.GetValue<int>())));
        }

        if (right is JsonArray)
        {
            return Compare(new JsonArray(JsonValue.Create(left.GetValue<int>())), right);
        }

        return left.GetValue<int>() < right.GetValue<int>()? 1 : left.GetValue<int>() > right.GetValue<int>() ? 0 : 2;
    }
}
