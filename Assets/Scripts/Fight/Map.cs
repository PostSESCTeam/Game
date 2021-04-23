using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public int MapHeight, MapWidth;
    public Cell[,] map;

    public Map(int mapHeight, int mapWidth, Cell[,] map)
    {
        MapHeight = mapHeight;
        MapWidth = mapWidth;
        this.map = map;
    }

    public static Map GenerateMap(int height, int width)
    {

        return new Map(height, width, new Cell[height, width]);
    }
}

public enum Cell
{
    Empty,
    Wall
}
