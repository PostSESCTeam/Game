using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Form : MonoBehaviour
{
    private FormCard curForm;
    private Characters chars;
    private GameObject formsPlace;

    public FormCard CurForm
    {
        get { return curForm; }
    }

    void Awake()
    {
        formsPlace = GameObject.Find("FormsPlace");
        chars = new Characters();
        ChangeFormCard();
    }

    public void ChangeFormCard() => ChangeFormCard(chars.TakeRandomCard(1));

    public void ChangeFormCard(FormCard newForm)
    {
        if (Main.IsFormsOpened && Input.GetAxis("Horizontal") > 0)
            Debug.Log($"You liked {curForm.Name}, {curForm.Age}");
        else if (Input.GetAxis("Horizontal") < 0) 
            Debug.Log($"You disliked {curForm.Name}, {curForm.Age}");

        curForm = newForm;
        var texts = formsPlace.GetComponentsInChildren<Text>();
        texts[0].text = $"{curForm.Name}, {curForm.Age}"; 
        texts[1].text = curForm.Description; 
    }
}

public class FormCard
{
    public string Name;
    public int Age;
    public Sex Sex;
    public string Description;
    public double FightProbability;
    public int ID;
    public bool IsSpecial;
    //TODO: generate and store images

    public FormCard(string name, int age, Sex sex, string desc, double fightProbability = 1.0, bool isSpecial = false)
    {
        Name = name;
        Age = age;
        Sex = sex;
        Description = desc;
        IsSpecial = isSpecial;
        FightProbability = fightProbability;
    }

    public static FormCard GenerateForm()
    {
        var random = new System.Random();
        var age = random.Next(18, 45);
        var sex = (Sex) random.Next(2);
        var path = @"Assets\Forms\" + sex.ToString() + "Names.txt";
        var names = File.ReadAllLines(path);

        return new FormCard(names[random.Next(names.Length)],
            age, sex, "");
        //TODO: generate description (using pregenerated info?)
    }
}

public enum Sex
{
    Male,
    Female
}
