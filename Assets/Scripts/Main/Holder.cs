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
        var path = @"Assets\Resources\Characters\";
        var sexes = new Sex[] { Sex.Male, Sex.Female };
        Bodies = new Sex[] { Sex.Male, Sex.Female }
            .Select(i => Resources.Load<Sprite>($@"{path}\{i}\Body.png"))
            .ToArray();

        Hairs = new Sex[] { Sex.Male, Sex.Female }
            .Select(i => Resources.LoadAll<Sprite>($@"{path}\{i}\Hair\"))
            .ToArray();

        Ups = new Sex[] { Sex.Male, Sex.Female }
            .Select(i => Resources.LoadAll<Sprite>($@"{path}\{i}\Up\"))
            .ToArray();

        Bottoms = new Sex[] { Sex.Male, Sex.Female }
            .Select(i => Resources.LoadAll<Sprite>($@"{path}\{i}\Bottom\"))
            .ToArray();

        RegularChats = Utils.GetSpriteFromFile(@"Assets\Sprites\Phone\Chat.png");
        NewMessageChats = Utils.GetSpriteFromFile(@"Assets\Sprites\Phone\ChatNotification.png");

        names = new Sex[] { Sex.Male, Sex.Female }
            .Select(i => File.ReadAllLines(@"Assets\Forms\" + i.ToString() + "Names.txt"))
            .ToArray();
        descs = File.ReadAllLines(@"Assets\Forms\Descriptions.txt");

        Main.Holder = this;
    }

    public string GetRandomName(Sex sex) => names[(int)sex].GetRandom();
    public string GetRandomDesc() => descs.GetRandom();
}
