using System.Linq;
using UnityEngine;

public class FormCard
{
    public string Name { get; private set; }
    public int Age { get; private set; }
    public Sex Sex { get; private set; }
    public Sprite[] Pictures { get; private set; }
    public string Description { get; private set; }
    public double FightProbability { get; private set; }
    public bool IsSpecial { get; private set; }
    public string LatinName { get; private set; }
    public string CharacterSet { get; private set; }

    public string FullName { get => $"{Name}, {Age}"; }

    public FormCard(string name, int age, Sex sex, Sprite[] pictures, string description,
        double fightProbability = 0.0, bool isSpecial = false, string latinName = null, string characterSet = null)
    {
        Name = name;
        Age = age;
        Sex = sex;
        Pictures = pictures;
        Description = description;
        IsSpecial = isSpecial;
        FightProbability = fightProbability;
        LatinName = latinName;
        CharacterSet = characterSet;
    }

    public FormCard(string name, int age, Sex sex, string[] pictures, string description,
        double fightProbability = 0.0, bool isSpecial = false, string latinName = null, string characterSet = null) 
            : this(name, age, sex, pictures.Select(i => Utils.GetSpriteFromFile(i)).ToArray(), 
                  description, fightProbability, isSpecial, latinName, characterSet)
    { }

    public static FormCard GenerateForm()
    {
        var age = Random.Range(18, 45);
        var sex = Random.Range(0, 2);
        var pics = new Sprite[]
        {
            Main.Holder.Bodies[sex],
            Main.Holder.Hairs[sex].GetRandom(),
            Main.Holder.Ups[sex].GetRandom(),
            Main.Holder.Bottoms[sex].GetRandom()
        };

        return new FormCard(Main.Holder.GetRandomName((Sex) sex), age, (Sex) sex, pics,
            Main.Holder.GetRandomDesc(), Random.Range(0f, 1f));
    }
}

public enum Sex
{
    Male,
    Female
}