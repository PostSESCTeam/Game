using System.Collections.Generic;

public class Dialog
{
    private string[][][] partnersPhrases, playersPhrases;
    private int phraseIndex = 0, phrasesCount;

    public Dialog(string[] file)
    {
        var partnerLiked = new List<string[]>();
        var partnerDisliked = new List<string[]>();
        var playerLiked = new List<string[]>();
        var playerDisliked = new List<string[]>();
        var buffer = new List<string>();

        foreach (var i in file)
        {
            if (i != "=====" && i != "-----" && i != string.Empty)
                buffer.Add(i);
            else
            {
                var strs = buffer.ToArray();
                if (i == string.Empty)
                {
                    if (playerLiked.Count == playerDisliked.Count)
                    {
                        playerLiked.Add(strs);
                        playerDisliked.Add(strs);
                    }
                    else
                        playerDisliked.Add(strs);
                }
                else if (i == "=====")
                {
                    if (partnerLiked.Count == partnerDisliked.Count)
                    {
                        partnerLiked.Add(strs);
                        partnerDisliked.Add(strs);
                    }
                    else
                        partnerDisliked.Add(strs);
                }
                else
                {
                    if (playerLiked.Count == partnerLiked.Count)
                        playerLiked.Add(strs);
                    else
                        partnerLiked.Add(strs);
                }
            }
        }

        phrasesCount = partnerLiked.Count;
        partnersPhrases = new[] { partnerLiked.ToArray(), partnerDisliked.ToArray() };
        partnersPhrases = new[] { playerLiked.ToArray(), playerDisliked.ToArray() };
    }

    private string[] GetPhraseList(bool isLiked, string[][][] phrases)
        => phraseIndex == phrasesCount ? null : phrases[isLiked ? 0 : 1][phraseIndex];

    public (string[], string[]) GetNextPhrases(bool isLiked)
    {
        var res = (GetPhraseList(isLiked, partnersPhrases), GetPhraseList(isLiked, playersPhrases));
        phraseIndex++;
        return res;
    }
}
