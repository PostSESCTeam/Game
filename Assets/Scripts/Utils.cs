using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Utils
{
    public static T GetRandom<T>(this IEnumerable<T> collection)
        => collection.ElementAt(Random.Range(0, collection.Count()));

    public static IEnumerable<(int X, int Y)> GetNeighbours(this (int X, int Y) p)
    {
        var d = new int[]{ -1, 0, 1 };
        return d
            .SelectMany(i => d.Select(j => (p.X + i, p.Y + j)))
            .Where(i => !p.Equals(i));
    }

    public static IEnumerable<(int X, int Y)> GetNeighbours(this IEnumerable<(int X, int Y)> ps) 
        => ps.SelectMany(i => GetNeighbours(i)).Except(ps);

    public static Sprite GetSpriteFromFile(string path)
    {
        var texture = new Texture2D(1, 1);
        texture.LoadImage(File.ReadAllBytes(path));
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public static Color GetRandomColor() => new Color(Random.Range(0.6f, 1), Random.Range(0.6f, 1), Random.Range(0.8f, 1));

    public static string[] ReadLines(this TextAsset ta)
        => ta.text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
}
