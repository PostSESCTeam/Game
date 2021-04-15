using System;
using System.IO;
using UnityEngine;

public class Form : MonoBehaviour
{
    private FormCard curForm;

    private void Awake()
    {
        curForm = FormCard.GenerateForm();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
            ChangeFormCard();
    }

    private void ChangeFormCard() => ChangeFormCard(FormCard.GenerateForm());

    //TODO: change controls & add like/dislike processing
    private void ChangeFormCard(FormCard newForm)
    {
        if (Input.GetAxis("Horizontal") > 0)
            Debug.Log($"You liked {curForm.Name}, {curForm.Age}");
        else Debug.Log($"You disliked {curForm.Name}, {curForm.Age}");

        curForm = newForm;
    }
}

public class FormCard
{
    public string Name;
    public int Age;
    public Sex Sex;
    public string Description;
    public int ID;
    public bool IsSpecial;
    //TODO: generate and store images

    public FormCard(string name, int age, Sex sex, string desc, bool isSpecial = false)
    {
        Name = name;
        Age = age;
        Sex = sex;
        Description = desc;
        IsSpecial = isSpecial;
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
