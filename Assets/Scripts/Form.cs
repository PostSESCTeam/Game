using System;
using System.IO;
using UnityEngine;

public class Form
{
    public string Name;
    public int Age;
    public Sex Sex;
    public string Description;
    //TODO: generate and store images

    public Form(string name, int age, Sex sex, string desc)
    {
        Name = name;
        Age = age;
        Sex = sex;
        Description = desc;
    }

    public static Form GenerateForm()
    {
        var random = new System.Random();
        var age = random.Next(18, 45);
        var sex = (Sex) random.Next(2);
        var path = @"Assets\Forms\" + sex.ToString() + "Names.txt";
        var names = File.ReadAllLines(path);

        return new Form(names[random.Next(names.Length)],
            age, sex, "");
        //TODO: generate description (using pregenerated info?)
    }
}

public enum Sex
{
    Male,
    Female
}
