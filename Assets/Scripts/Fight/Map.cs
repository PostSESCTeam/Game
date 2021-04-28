using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Map
{
    public int MapHeight, MapWidth;
    public Cell[,] Cells;
    public List<(int X, int Y)[]> Blocks;
    public static Dictionary<int, int> BlockAmount = new (int Size, int Amount)[] { (1, 3), (2, 2), (3, 1) }
        .ToDictionary(i => i.Size, i => i.Amount);

    public Map(int mapHeight, int mapWidth, List<(int X, int Y)[]> blocks)
    {
        MapHeight = mapHeight;
        MapWidth = mapWidth;
        Blocks = blocks;
        Cells = new Cell[MapHeight, MapWidth];
        foreach (var (X, Y) in blocks.SelectMany(i => i))
            Cells[X, Y] = Cell.Wall;
    }

    private static bool IsInBounds(int height, int width, (int X, int Y) point) 
        => (0 <= point.X) && (point.X < width) && (0 <= point.Y) && (point.Y < height);

    public static Map GenerateMap(int height, int width)
    {
        var blocks = new List<(int X, int Y)[]>();
        var rand = new System.Random();
        var fields = Enumerable.Range(0, height).SelectMany(i => Enumerable.Range(0, width).Select(j => (X: i, Y: j)));

        foreach (var i in BlockAmount.Keys)
        {
            if (fields.Count() == 0)
                break;

            for (var j = 0; j < BlockAmount[i]; j++)
            {
                if (fields.Count() == 0)
                    break;

                while (true)
                {
                    var (X, Y) = fields.GetRandom();
                    var isHorizontal = Convert.ToBoolean(rand.Next(2));
                    var block = Enumerable.Range(0, i)
                        .Select(k => isHorizontal ? (X, Y: Y + k) : (X: X + k, Y));
                    var neighs = block.GetNeighbours().Where(i => IsInBounds(height, width, i));

                    if (block.All(k => fields.Contains(k)) && neighs.All(k => fields.Contains(k)))
                    {
                        fields = fields.Except(block).Except(neighs);
                        blocks.Add(block.ToArray());
                        break;
                    }
                }
            }
        }
        return new Map(height, width, blocks);
    }

    public override string ToString()
    {
        var builder = new StringBuilder();

        for (var i = 0; i < MapHeight; i++)
        {
            for (var j = 0; j < MapWidth; j++)
                builder.Append((int)Cells[i, j]);
            builder.Append('\n');
        }

        return builder.ToString();
    }
}

public enum Cell
{
    Empty,
    Wall
}