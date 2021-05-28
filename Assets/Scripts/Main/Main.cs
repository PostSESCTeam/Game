using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public static class Main
{
    public static bool IsChatOpened = false,
        IsFormsOpened = false,
        IsProfileOpened = true,
        IsSwipesFrozen = true,
        IsCallingOpen = false,
        IsTutorialOn = true,
        CanShoot = false,
        IsFirstDuel = true,
        IsFirstMessage = true,
        IsFirstStart = true;

    public static Dictionary<string, Chat> Chats;
    public static Dictionary<string, Dialog> Dialogs;
    public static Dictionary<string, double> FightProbabs;
    public static Dictionary<string, bool> IsLiked;

    private static string behName;
    private static Act actor = null;
    public static GameObject ContactsContent, StartTutorial, ChatsTutorial, DuelTutorial;
    private static Transform contact;
    public static ChatTabsManager CTM;

    public static Sprite[] Bodies;
    public static Sprite[][] Hairs, Ups, Bottoms;

    private static string[][] names;
    private static string[] descs;

    public static Sprite RegularChats, NewMessageChats;

    private static int likedAmount = 0;
    public static Text Desc;
    public static Image ChatsBtnImg;

    public static void Init(bool isTutorialOn)
    {
        IsChatOpened = false;
        IsFormsOpened = false;
        IsProfileOpened = true;
        IsSwipesFrozen = true;
        IsCallingOpen = false;
        IsTutorialOn = isTutorialOn;
        CanShoot = false;
        IsFirstDuel = true;
        IsFirstMessage = true;

        Chats = new Dictionary<string, Chat>();
        FightProbabs = new Dictionary<string, double>();
        IsLiked = new Dictionary<string, bool>();

        if (IsFirstStart)
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

            contact = Resources.Load<Transform>("Contact");

            RegularChats = Utils.GetSpriteFromFile(@"Assets\Sprites\Phone\Chat.png");
            NewMessageChats = Utils.GetSpriteFromFile(@"Assets\Sprites\Phone\ChatNotification.png");

            names = new Sex[] { Sex.Male, Sex.Female }
                .Select(i => File.ReadAllLines(@"Assets\Forms\" + i.ToString() + "Names.txt"))
                .ToArray();
            descs = File.ReadAllLines(@"Assets\Forms\Descriptions.txt");
        }

        IsFirstStart = false;
    }

    public static void Like(FormCard newLiked)
    {
        if (newLiked.IsSpecial)
        {
            IsLiked[newLiked.FullName] = true;
            StartChat(newLiked, true);
        }

        likedAmount++;
        Desc.text = $"Лайкнуто анкет: {likedAmount}";
    }

    public static void Dislike(FormCard newDisliked)
    {
        if (newDisliked.IsSpecial)
        {
            IsLiked[newDisliked.FullName] = false;
            StartChat(newDisliked, false);
        }
    }

    public static IEnumerator StartDuel(Animator animator, string rivalBehName = null)
    {
        actor = Object.FindObjectOfType<Act>();
        behName = rivalBehName;
        IsSwipesFrozen = true;
        CanShoot = false;
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Duel", LoadSceneMode.Additive);
        foreach (var i in SceneManager.GetActiveScene().GetRootGameObjects())
            i.SetActive(false);

        SceneManager.sceneLoaded += (scene, sceneMode) =>
        {
            var duel = Object.FindObjectOfType<Duel>();

            if (duel)
                duel.OnInitRival += () => duel.SetRivalBehaviour(behName);
        };
    }

    public static IEnumerator FinishDuel(Animator animator, DuelObject lostObject)
    {
        var isWin = !(lostObject is Player);
        CanShoot = false;

        foreach (var i in Object.FindObjectsOfType<Bullet>())
            Object.Destroy(i.gameObject);

        animator.SetTrigger(isWin ? "Winning" : "Losing");
        yield return new WaitForSeconds(2);
        Object.FindObjectOfType<Button>().onClick.AddListener(() =>
        {
            SceneManager.UnloadSceneAsync("Duel");
            foreach (var i in SceneManager.GetActiveScene().GetRootGameObjects().Where(i => i.name != "Death"))
                i.SetActive(true);

            actor.UpdateAfterDuel(isWin);
            IsSwipesFrozen = false;
            IsCallingOpen = false;
            IsFormsOpened = true;
        });
    }

    private static Sprite[][] LoadSprites(IEnumerable<DirectoryInfo> directories, string filePattern)
        => directories.Select(i => i.EnumerateFiles(filePattern)
            .Select(j => Utils.GetSpriteFromFile(j.ToString())).ToArray())
            .ToArray();

    private static Chat StartChat(FormCard pers, bool isLiked)
    {
        ChatsBtnImg.sprite = NewMessageChats;

        var partner = pers.FullName;
        FightProbabs[partner] = pers.FightProbability;
        var chat = new Chat(partner);
        chat.SendMessage(partner, Dialogs[partner].GetPartnersPhrases(isLiked).GetRandom());
        Chats[partner] = chat;

        var contactItem = Object.Instantiate(contact, ContactsContent.transform);
        contactItem.Find("Name").GetComponent<Text>().text = chat.Partner;
        contactItem.Find("Message").GetComponent<Text>().text = chat.LastMessage.Sentence;
        contactItem.GetComponent<Button>().onClick.AddListener(() => CTM.OpenChat(chat.Partner));

        if (IsFirstMessage)
        {
            ChatsTutorial.SetActive(true);
            IsFirstMessage = false;
        }

        return chat;
    }

    public static string GetRandomName(Sex sex) => names[(int)sex].GetRandom();
    public static string GetRandomDesc() => descs.GetRandom();
}