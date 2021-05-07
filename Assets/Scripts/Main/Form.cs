using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Form : MonoBehaviour
{
    private int randomAmount = 15;
    private bool isFirst = true;
    private Characters chars;
    private GameObject frontFormsPlace;
    private GameObject backFormsPlace;
    private Text[] frontTexts;
    private Text[] backTexts;
    private SpriteRenderer[] frontDrawPlaces;
    private SpriteRenderer[] backDrawPlaces;

    public FormCard CurForm { get; private set; }
    public FormCard NextForm { get; private set; }

    private void Start()
    {
        chars = new Characters();
        frontFormsPlace = GameObject.Find("FrontFormsPlace");
        backFormsPlace = GameObject.Find("BackFormsPlace");
        frontTexts = frontFormsPlace.GetComponentsInChildren<Text>();
        backTexts = backFormsPlace.GetComponentsInChildren<Text>();
        frontDrawPlaces = Enumerable.Range(0, frontFormsPlace.transform.childCount)
            .Skip(2)
            .Select(i => frontFormsPlace.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>())
            .Reverse()
            .ToArray();
        backDrawPlaces = Enumerable.Range(0, backFormsPlace.transform.childCount)
            .Skip(2)
            .Select(i => backFormsPlace.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>())
            .Reverse()
            .ToArray();
        NextForm = chars.TakeRandomCard(randomAmount);
        Redraw(NextForm, backDrawPlaces, backTexts);
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
        CurForm = NextForm;
        Redraw(CurForm, frontDrawPlaces, frontTexts);
        NextForm = newForm;
        Redraw(NextForm, backDrawPlaces, backTexts);
    }

    private void Redraw(FormCard form, SpriteRenderer[] drawPlaces, Text[] texts)
    {
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