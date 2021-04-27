using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Form : MonoBehaviour
{
    private int randomAmount = 5;
    private bool isFirst = true;
    private FormCard curForm;
    private Characters chars;
    private GameObject formsPlace;
    private SpriteRenderer[] drawPlaces;
    
    public FormCard CurForm
    {
        get { return curForm; }
    }

    void Start()
    {
        chars = new Characters();
        formsPlace = GameObject.Find("FormsPlace");
        drawPlaces = Enumerable.Range(0, formsPlace.transform.childCount).Skip(2).Select(i => formsPlace.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>()).Reverse().ToArray();
        ChangeFormCard(true);
    }

    public void ChangeFormCard(bool isLiked) 
        => ChangeFormCard(chars.TakeRandomCard(randomAmount), isLiked);

    public void ChangeFormCard(FormCard newForm, bool isLiked)
    {
        if (!isFirst)
        {
            if (isLiked)
                Debug.Log($"You liked {curForm.Name}, {curForm.Age}");
            else
                Debug.Log($"You disliked {curForm.Name}, {curForm.Age}");
        }

        isFirst = false;
        curForm = newForm;
        var texts = formsPlace.GetComponentsInChildren<Text>();
        texts[0].text = $"{curForm.Name}, {curForm.Age}"; 
        texts[1].text = curForm.Description;
        Redraw(curForm.Pictures, curForm.IsSpecial, drawPlaces);
    }

    private void Redraw(Sprite[] files, bool isSpecial, SpriteRenderer[] places)
    {
        if (isSpecial)
        {
            places[0].gameObject.SetActive(true);
            for (var i = 1; i < 5; i++)
                places[i].gameObject.SetActive(false);
            places[0].sprite = files[0];
        }
        else
        {
            places[0].gameObject.SetActive(false);
            for (var i = 1; i < 5; i++)
            {
                places[i].gameObject.SetActive(true);
                places[i].sprite = files[i - 1];
            }
        }
    }
}

public class FormCard
{
    public string Name;
    public int Age;
    private Sex sex;
    public Sprite[] Pictures;
    public string Description;
    private double fightProbability;
    public bool IsSpecial;
    private int id;

    public FormCard(string name, int age, Sex sex, Sprite[] pictures, string description,
        double fightProbability = 1.0, bool isSpecial = false)
    {
        this.Name = name;
        this.Age = age;
        this.sex = sex;
        this.Pictures = pictures;
        this.Description = description;
        this.IsSpecial = isSpecial;
        this.fightProbability = fightProbability;
    }

    public FormCard(string name, int age, Sex sex, string[] pictures, string description,
        double fightProbability = 1.0, bool isSpecial = false)
    {
        this.Name = name;
        this.Age = age;
        this.sex = sex;
        this.Pictures = pictures.Select(i => Utils.GetSpriteFromFile(i)).ToArray();
        this.Description = description;
        this.IsSpecial = isSpecial;
        this.fightProbability = fightProbability;
    }

    public static FormCard GenerateForm()
    {
        var random = new System.Random();
        var age = random.Next(18, 45);
        var sex = (Sex) random.Next(2);
        var namesPath = @"Assets\Forms\" + sex.ToString() + "Names.txt";
        var names = File.ReadAllLines(namesPath);
        var pics = new Sprite[] { 
            Main.Bodies[0], //(int) sex
            Main.Hairs[0].GetRandom(), 
            Main.Ups[0].GetRandom(), 
            Main.Bottoms[0].GetRandom() 
        };

        return new FormCard(names.GetRandom(),
            age, sex, pics, "", random.NextDouble());
        //TODO: generate description (using pregenerated info?)
    }
}

public enum Sex
{
    Male,
    Female
}
