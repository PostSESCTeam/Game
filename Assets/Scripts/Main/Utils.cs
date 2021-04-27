using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public static class Utils
{
    public static T GetRandom<T>(this IEnumerable<T> collection)
        => collection.ElementAt(new System.Random().Next(collection.Count()));

    public static async Task<Sprite> GetSpriteFromFileAsync(string path)
    {
        var texture = new Texture2D(1, 1);
        byte[] result;

        using (FileStream fs = File.Open(path, FileMode.Open))
        {
            result = new byte[fs.Length];
            await fs.ReadAsync(result, 0, (int) fs.Length);
        }

        Debug.Log(path);
        texture.LoadImage(result);
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public static Sprite GetSpriteFromFile(string path)
    {
        var texture = new Texture2D(1, 1);
        texture.LoadImage(File.ReadAllBytes(path));
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
