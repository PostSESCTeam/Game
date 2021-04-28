using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Form : MonoBehaviour
{
    private int randomAmount = 5;
    private bool isFirst = true;
    private Characters chars;
    private GameObject formsPlace;
    private Text[] texts;
    private SpriteRenderer[] drawPlaces;

    public FormCard CurForm { get; private set; }

    void Start()
    {
        chars = new Characters();
        formsPlace = GameObject.Find("FormsPlace");
        texts = formsPlace.GetComponentsInChildren<Text>();
        drawPlaces = Enumerable.Range(0, formsPlace.transform.childCount)
            .Skip(2)
            .Select(i => formsPlace.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>())
            .Reverse()
            .ToArray();
        ChangeFormCard(true);
    }

    public void ChangeFormCard(bool isLiked) 
        => ChangeFormCard(chars.TakeRandomCard(randomAmount), isLiked);

    public void ChangeFormCard(FormCard newForm, bool isLiked)
    {
        if (!isFirst)
        {
            if (isLiked)
                Debug.Log($"You liked {CurForm.Name}, {CurForm.Age}");
            else
                Debug.Log($"You disliked {CurForm.Name}, {CurForm.Age}");
        }

        isFirst = false;
        CurForm = newForm;
        Redraw();
    }

    private void Redraw()
    {
        texts[0].text = $"{CurForm.Name}, {CurForm.Age}"; 
        texts[1].text = CurForm.Description;
        if (CurForm.IsSpecial)
        {
            drawPlaces[0].gameObject.SetActive(true);
            for (var i = 1; i < 5; i++)
                drawPlaces[i].gameObject.SetActive(false);
            drawPlaces[0].sprite = CurForm.Pictures[0];
        }
        else
        {
            drawPlaces[0].gameObject.SetActive(false);
            for (var i = 1; i < 5; i++)
            {
                drawPlaces[i].gameObject.SetActive(true);
                drawPlaces[i].sprite = CurForm.Pictures[i - 1];
            }
        }
    }
}