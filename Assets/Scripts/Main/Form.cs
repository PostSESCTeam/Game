using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Form : MonoBehaviour
{
    private int randomAmount = 16;
    private bool isFirst = true;
    private Characters chars;
    private GameObject frontFormsPlace, backFormsPlace;
    private Text[] frontTexts, backTexts;
    private SpriteRenderer[] frontDrawPlaces, backDrawPlaces;
    private SpriteRenderer frontBG, backBG;
    private Color frontColor, backColor;

    public FormCard CurForm { get; private set; }
    public FormCard NextForm { get; private set; }

    private void Start()
    {
        chars = new Characters();

        frontFormsPlace = GameObject.Find("FrontFormsPlace");
        frontTexts = frontFormsPlace.GetComponentsInChildren<Text>();
        frontBG = frontFormsPlace.GetComponent<SpriteRenderer>();
        frontDrawPlaces = GetDrawPlaces(frontFormsPlace);

        backFormsPlace = GameObject.Find("BackFormsPlace");
        backTexts = backFormsPlace.GetComponentsInChildren<Text>();
        backBG = backFormsPlace.GetComponent<SpriteRenderer>();
        backDrawPlaces = GetDrawPlaces(backFormsPlace);

        NextForm = chars.TakeRandomCard(randomAmount);
        backColor = Utils.GetRandomColor();
        Redraw(NextForm, backDrawPlaces, backTexts, backBG, backColor);
        ChangeFormCard(true);
    }

    private SpriteRenderer[] GetDrawPlaces(GameObject parent)
    {
        var parentTransform = parent.transform;
        return Enumerable.Range(2, parentTransform.childCount - 2)
            .Select(i => parentTransform.GetChild(i).gameObject.GetComponent<SpriteRenderer>())
            .Reverse()
            .ToArray();
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
        CurForm = NextForm;
        frontColor = backColor;
        Redraw(CurForm, frontDrawPlaces, frontTexts, frontBG, frontColor);
        NextForm = newForm;
        backColor = Utils.GetRandomColor();
        Redraw(NextForm, backDrawPlaces, backTexts, backBG, backColor);
    }

    private void Redraw(FormCard form, SpriteRenderer[] drawPlaces, Text[] texts, SpriteRenderer bg, Color color)
    {
        bg.color = color;
        texts[0].text = $"{form.Name}, {form.Age}";
        texts[1].text = form.Description;
        if (form.IsSpecial)
        {
            drawPlaces[0].gameObject.SetActive(true);
            for (var i = 1; i < 5; i++)
                drawPlaces[i].gameObject.SetActive(false);
            drawPlaces[0].sprite = form.Pictures[0];
        }
        else
        {
            drawPlaces[0].gameObject.SetActive(false);
            for (var i = 1; i < 5; i++)
            {
                drawPlaces[i].gameObject.SetActive(true);
                drawPlaces[i].sprite = form.Pictures[i - 1];
            }
        }
    }
}