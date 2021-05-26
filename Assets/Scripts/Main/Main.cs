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
        IsFirstDuel = true;

    public static List<Chat> Chats;

    private static string behName;
    private static Act actor = null;
    private static List<FormCard> liked = new List<FormCard>(), disliked = new List<FormCard>();

    private const string path = @"Assets\Sprites\Characters\";
    private static IEnumerable<DirectoryInfo> sexFolders = new Sex[] { Sex.Male, Sex.Female }
        .Select(i => new DirectoryInfo(path + i.ToString()));
    public static List<Sprite> Bodies = sexFolders
        .Select(i => Utils.GetSpriteFromFile(path + i.Name + @"\Body.png"))
        .ToList();
    public static List<List<Sprite>> Hairs = LoadSprites(sexFolders, "Hair_*.png"),
        Ups = LoadSprites(sexFolders, "Up_*.png"),
        Bottoms = LoadSprites(sexFolders, "Bottom_*.png");

    public static void Like(FormCard newLiked)
    {
        liked.Add(newLiked);
        StartChat(newLiked);
    }

    public static void Dislike(FormCard newDisliked)
    {
        disliked.Add(newDisliked);
        StartChat(newDisliked);
    }

    public static IEnumerable<FormCard> GetLiked() => liked;
    public static IEnumerable<FormCard> GetDisliked() => disliked;

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
            foreach (var i in SceneManager.GetActiveScene().GetRootGameObjects())
                i.SetActive(true);

            actor.UpdateAfterDuel(isWin);
            IsSwipesFrozen = false;
            IsCallingOpen = false;
            IsFormsOpened = true;
        });
    }

    private static List<List<Sprite>> LoadSprites(IEnumerable<DirectoryInfo> directories, string filePattern)
        => directories.Select(i => i.EnumerateFiles(filePattern)
            .Select(j => Utils.GetSpriteFromFile(j.ToString())).ToList())
            .ToList();

    private static Chat StartChat(FormCard pers)
    {
        var chat = new Chat($"{pers.Name}, {pers.Age}");

        return chat;
    }
}