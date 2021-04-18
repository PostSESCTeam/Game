using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main
{
    public static bool IsChatOpened = false;
    public static bool IsFormsOpened = true;
    public static bool IsProfileOpened = false;

    private static List<FormCard> liked = new List<FormCard>();
    private static List<FormCard> disliked = new List<FormCard>();

    public static void AddLiked(FormCard newLiked) => liked.Add(newLiked);
    public static void AddDisliked(FormCard newDisliked) => disliked.Add(newDisliked);
    public List<FormCard> GetLiked() => liked;
    public List<FormCard> GetDisliked() => disliked;
}