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
    public int ID { get; private set; }

    public FormCard(string name, int age, Sex sex, Sprite[] pictures, string description,
        double fightProbability = 1.0, bool isSpecial = false)
    {
        Name = name;
        Age = age;
        Sex = sex;
        Pictures = pictures;
        Description = description;
        IsSpecial = isSpecial;
        FightProbability = fightProbability;
    }

    public FormCard(string name, int age, Sex sex, string[] pictures, string description,
        double fightProbability = 1.0, bool isSpecial = false) 
            : this(name, age, sex, pictures.Select(i => Utils.GetSpriteFromFile(i)).ToArray(), 
                  description, fightProbability, isSpecial)
    { }

    public static FormCard GenerateForm()
    {
        var random = new System.Random();
        var age = random.Next(18, 45);
        var sex = (Sex) random.Next(2);
        var namesPath = @"Assets\Forms\" + sex.ToString() + "Names.txt";
        var names = File.ReadAllLines(namesPath);
        var descripPath = @"Assets\Forms\Descriptions.txt";
        var descriptions = File.ReadAllLines(descripPath);
        var pics = new Sprite[] { 
            Main.Bodies[(int)sex],
            Main.Hairs[(int)sex].GetRandom(), 
            Main.Ups[(int)sex].GetRandom(), 
            Main.Bottoms[(int)sex].GetRandom() 
        };

        return new FormCard(names.GetRandom(),
            age, sex, pics, descriptions.GetRandom(), random.NextDouble());
        //TODO: generate description (using pregenerated info?)
    }
}

public enum Sex
{
    Male,
    Female
}