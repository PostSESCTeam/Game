using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class Main
{
    public static bool IsChatOpened = false;
    public static bool IsFormsOpened = true;
    public static bool IsProfileOpened = false;

    private static readonly List<FormCard> liked = new List<FormCard>();
    private static readonly List<FormCard> disliked = new List<FormCard>();
    private static readonly string path = @"Assets\Sprites\Characters\";
    private static readonly IEnumerable<DirectoryInfo> sexFolders = new Sex[] { Sex.Female } // add Sex.Male
        .Select(i => new DirectoryInfo(path + i.ToString()));

    public static readonly List<Sprite> Bodies = sexFolders
        .Select(i => Utils.GetSpriteFromFile(path + i.Name + @"\Body.png"))
        .ToList();
    public static readonly List<List<Sprite>> Hairs = sexFolders
        .Select(i => i.EnumerateFiles("Hair_*.png").Select(j => Utils.GetSpriteFromFile(j.ToString())).ToList())
        .ToList();
    public static readonly List<List<Sprite>> Ups = sexFolders
        .Select(i => i.EnumerateFiles("Up_*.png").Select(j => Utils.GetSpriteFromFile(j.ToString())).ToList())
        .ToList();
    public static readonly List<List<Sprite>> Bottoms = sexFolders
        .Select(i => i.EnumerateFiles("Bottom_*.png").Select(j => Utils.GetSpriteFromFile(j.ToString())).ToList())
        .ToList();

    public static void AddLiked(FormCard newLiked) => liked.Add(newLiked);
    public static void AddDisliked(FormCard newDisliked) => disliked.Add(newDisliked);
    public static IEnumerable<FormCard> GetLiked() => liked;
    public static IEnumerable<FormCard> GetDisliked() => disliked;
}