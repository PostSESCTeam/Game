using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public Sprite[] Bodies;
    public Sprite[][] Hairs, Ups, Bottoms;

    private string[][] names;
    private string[] descs;

    public Sprite RegularChats, NewMessageChats;

    private void Start()
    {
        var path = @"Assets\Sprites\Characters\";
        var sexFolders = new Sex[] { Sex.Male, Sex.Female }
            .Select(i => new DirectoryInfo(path + i.ToString()));
        Bodies = sexFolders
            .Select(i => Utils.GetSpriteFromFile(path + i.Name + @"\Body.png"))
            .ToArray();
        Hairs = LoadSprites(sexFolders, "Hair_*.png");
        Ups = LoadSprites(sexFolders, "Up_*.png");
        Bottoms = LoadSprites(sexFolders, "Bottom_*.png");

        RegularChats = Utils.GetSpriteFromFile(@"Assets\Sprites\Phone\Chat.png");
        NewMessageChats = Utils.GetSpriteFromFile(@"Assets\Sprites\Phone\ChatNotification.png");

        names = new Sex[] { Sex.Male, Sex.Female }
            .Select(i => File.ReadAllLines(@"Assets\Forms\" + i.ToString() + "Names.txt"))
            .ToArray();
        descs = File.ReadAllLines(@"Assets\Forms\Descriptions.txt");

        Main.Holder = this;
    }

    private static Sprite[][] LoadSprites(IEnumerable<DirectoryInfo> directories, string filePattern)
        => directories.Select(i => i.EnumerateFiles(filePattern)
            .Select(j => Utils.GetSpriteFromFile(j.ToString())).ToArray())
            .ToArray();

    public string GetRandomName(Sex sex) => names[(int)sex].GetRandom();
    public string GetRandomDesc() => descs.GetRandom();
}
