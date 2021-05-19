using System.IO;
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
    public string CharacterSet { get; private set; }

    public FormCard(string name, int age, Sex sex, Sprite[] pictures, string description,
        double fightProbability = 0.0, bool isSpecial = false, string characterSet = null)
    {
        Name = name;
        Age = age;
        Sex = sex;
        Pictures = pictures;
        Description = description;
        IsSpecial = isSpecial;
        FightProbability = fightProbability;
        CharacterSet = characterSet;
    }

    public FormCard(string name, int age, Sex sex, string[] pictures, string description,
        double fightProbability = 0.0, bool isSpecial = false, string characterSet = null) 
            : this(name, age, sex, pictures.Select(i => Utils.GetSpriteFromFile(i)).ToArray(), 
                  description, fightProbability, isSpecial, characterSet)
    { }

    public static FormCard GenerateForm()
    {
        var age = Random.Range(18, 45);
        var sex = Random.Range(0, 2);
        var namesPath = @"Assets\Forms\" + ((Sex) sex).ToString() + "Names.txt";
        var names = File.ReadAllLines(namesPath);
        var descPath = @"Assets\Forms\Descriptions.txt";
        var descriptions = File.ReadAllLines(descPath);
        var pics = new Sprite[]
        {
            Main.Bodies[sex],
            Main.Hairs[sex].GetRandom(),
            Main.Ups[sex].GetRandom(),
            Main.Bottoms[sex].GetRandom()
        };

        return new FormCard(names.GetRandom(),
            age, (Sex) sex, pics, descriptions.GetRandom(), Random.Range(0f, 1f));
    }
}

public enum Sex
{
    Male,
    Female
}