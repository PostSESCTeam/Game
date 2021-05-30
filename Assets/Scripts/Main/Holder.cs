using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Holder : MonoBehaviour
{
	public TextAsset MalesNamesFile, FemalesNamesFile, DescsFile, VictorChatFile, FufeChatFile, ReptiloidChatFile,
		TvarChatFile, LydiaChatFile, EricChatFile, AlexeyChatFile, DedChatFile, SkeletChatFile, IgorChatFile,
		KeshaChatFile, LeonidChatFile, KoshkaChatFile, MargoChatFile;
	

	public Dictionary<string, Sprite> PlotPers;
	public Sprite[] Bodies;
	public Sprite[][] Hairs, Ups, Bottoms;
	public Dictionary<string, string[]> Chats;

	public Sprite RegularChats, NewMessageChats;

	private string[][] names;
	private string[] descs;

	private void Start()
	{
		var path = @"Characters";
		var sexFolders = new Sex[] { Sex.Male, Sex.Female };
		Bodies = Resources.LoadAll<Sprite>($"{path}/Bodies");

		Hairs = sexFolders
			.Select(i => Resources.LoadAll<Sprite>($"{path}/{i}/Hair"))
			.ToArray();

		Ups = sexFolders
			.Select(i => Resources.LoadAll<Sprite>($"{path}/{i}/Up"))
			.ToArray();

		Bottoms = sexFolders
			.Select(i => Resources.LoadAll<Sprite>($"{path}/{i}/Bottom"))
			.ToArray();

		RegularChats = Resources.Load<Sprite>("Chat");
		NewMessageChats = Resources.Load<Sprite>("ChatNotification");

		names = new TextAsset[] { MalesNamesFile, FemalesNamesFile }
			.Select(i => i.ReadLines())
			.ToArray();
		descs = DescsFile.ReadLines();

		PlotPers = Resources.LoadAll<Sprite>($"{path}/Plot")
			.Select(i => (i.name, i))
			.ToDictionary(i => i.Item1, i => i.Item2);

		Chats = new (string, TextAsset)[] {("Victor", VictorChatFile), ("Fufe", FufeChatFile), ("Reptiloid", ReptiloidChatFile),
			("Tvar", TvarChatFile), ("Lydia", LydiaChatFile), ("Eric", EricChatFile), ("Alexey", AlexeyChatFile),
			("Ded", DedChatFile), ("Skelet", SkeletChatFile), ("Igor", IgorChatFile), ("Kesha", KeshaChatFile),
			("Leonid", LeonidChatFile), ("Koshka", KoshkaChatFile), ("Margo", MargoChatFile)}
			.ToDictionary(i => i.Item1, i => i.Item2.ReadLines());

		Main.Holder = this;
	}

	public string GetRandomName(Sex sex) => names[(int)sex].GetRandom();
	public string GetRandomDesc() => descs.GetRandom();
}
