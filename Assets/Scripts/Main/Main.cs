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

    private static readonly Act actor = Object.FindObjectOfType<Act>();
    private static readonly List<FormCard> liked = new List<FormCard>();
    private static readonly List<FormCard> disliked = new List<FormCard>();
    private static readonly string path = @"Assets\Sprites\Characters\";
    private static readonly IEnumerable<DirectoryInfo> sexFolders = new Sex[] { Sex.Male, Sex.Female }
        .Select(i => new DirectoryInfo(path + i.ToString()));

    public static readonly List<Sprite> Bodies = sexFolders
        .Select(i => Utils.GetSpriteFromFile(path + i.Name + @"\Body.png"))
        .ToList();
    public static readonly List<List<FileInfo>> Hairs = sexFolders
        .Select(i => i.EnumerateFiles("Hair_*.png").ToList())
        .ToList();
    public static readonly List<List<FileInfo>> Ups = sexFolders
        .Select(i => i.EnumerateFiles("Up_*.png").ToList())
        .ToList();
    public static readonly List<List<FileInfo>> Bottoms = sexFolders
        .Select(i => i.EnumerateFiles("Bottom_*.png").ToList())
        .ToList();

    public static void Like(FormCard newLiked) => liked.Add(newLiked);
    public static void Dislike(FormCard newDisliked) => disliked.Add(newDisliked);
    public static IEnumerable<FormCard> GetLiked() => liked;
    public static IEnumerable<FormCard> GetDisliked() => disliked;

    public static IEnumerator StartDuel(Animator animator)
    {
        IsSwipesFrozen = true;
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("Duel", LoadSceneMode.Additive);
        foreach (var i in SceneManager.GetActiveScene().GetRootGameObjects())
            i.SetActive(false);
    }

    public static IEnumerator FinishDuel(Animator animator, DuelObject lostObject)
    {
        var isWin = !(lostObject is Player);

        foreach (var i in Object.FindObjectsOfType<Bullet>())
            Object.Destroy(i.gameObject);

        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.UnloadSceneAsync("Duel");
        foreach (var i in SceneManager.GetActiveScene().GetRootGameObjects())
            i.SetActive(true);

        actor.UpdateAfterDuel(isWin);
        IsSwipesFrozen = false;
    }
}