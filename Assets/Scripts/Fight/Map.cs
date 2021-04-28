using System.Collections.Generic;
using System.Linq;

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
                    var point = fields.GetRandom();
                    var orient = (Orientation)rand.Next(2);
                    var block = Enumerable.Range(0, i).Select(k => (X: point.X, Y: point.Y + k));
                    
                    if (block.All(k => fields.Contains(k)) && block.GetNeighbours().All(k => fields.Contains(k)))
                    {
                        fields.Except(block);
                        blocks.Add(block.ToArray());
                        break;
                    }
                }
            }
        }
        return new Map(height, width, blocks);
    }
}

public enum Cell
{
    Empty,
    Wall
}

public enum Orientation
{
    Vertical,
    Horizontal
}
