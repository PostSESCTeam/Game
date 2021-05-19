using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Main
{
    public static bool IsChatOpened = false;
    public static bool IsFormsOpened = true;
    public static bool IsProfileOpened = false;
    public static bool IsSwipesFrozen = false;

    private static string behName = null;
    private static Act actor = null;
    private static readonly List<FormCard> liked = new List<FormCard>(), disliked = new List<FormCard>();

    public static List<Sprite> Bodies;
    public static List<List<Sprite>> Hairs, Ups, Bottoms;

    public static void Like(FormCard newLiked) => liked.Add(newLiked);
    public static void Dislike(FormCard newDisliked) => disliked.Add(newDisliked);
    public static IEnumerable<FormCard> GetLiked() => liked;
    public static IEnumerable<FormCard> GetDisliked() => disliked;

    public static void Init()
    {
        var path = @"Assets\Sprites\Characters\";
        var sexFolders = new Sex[] { Sex.Male, Sex.Female }.Select(i => new DirectoryInfo(path + i.ToString()));
        Bodies = sexFolders
            .Select(i => Utils.GetSpriteFromFile(path + i.Name + @"\Body.png"))
            .ToList();
        Hairs = LoadSprites(sexFolders, "Hair_*.png");
        Ups = LoadSprites(sexFolders, "Up_*.png");
        Bottoms = LoadSprites(sexFolders, "Bottom_*.png");
    }

    public static IEnumerator StartDuel(Animator animator, string rivalBehName = null)
    {
        actor = Object.FindObjectOfType<Act>();
        behName = rivalBehName;
        IsSwipesFrozen = true;
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("Duel", LoadSceneMode.Additive);
        foreach (var i in SceneManager.GetActiveScene().GetRootGameObjects())
            i.SetActive(false);

        SceneManager.sceneLoaded += (scene, sceneMode) =>
        {
            var duel = Object.FindObjectOfType<Duel>();
            duel.OnInitRival += () => duel.SetRivalBehaviour(rivalBehName);
        };
    }

    public static IEnumerator FinishDuel(Animator animator, DuelObject lostObject)
    {
        var isWin = !(lostObject is Player);

        foreach (var i in Object.FindObjectsOfType<Bullet>())
            Object.Destroy(i.gameObject);

        if (isWin)
        {
            animator.SetTrigger("Winning");
            yield return new WaitForSeconds(5);
        }
        else
        {
            animator.SetTrigger("FadeOut");
            yield return new WaitForSeconds(1);
        }
        SceneManager.UnloadSceneAsync("Duel");
        foreach (var i in SceneManager.GetActiveScene().GetRootGameObjects())
            i.SetActive(true);

        actor.UpdateAfterDuel(isWin);
        IsSwipesFrozen = false;
    }

    private static List<List<Sprite>> LoadSprites(IEnumerable<DirectoryInfo> directories, string filePattern)
        => directories.Select(i => i.EnumerateFiles(filePattern)
            .Select(j => Utils.GetSpriteFromFile(j.ToString())).ToList())
            .ToList();
}