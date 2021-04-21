using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Form : MonoBehaviour
{
    private int randomAmount = 5;
    private FormCard curForm;
    private Characters chars;
    private GameObject formsPlace;
    private SpriteRenderer picPlace;

    public FormCard CurForm
    {
        get { return curForm; }
    }

    void Start()
    {
        chars = new Characters();
        formsPlace = GameObject.Find("FormsPlace");
        picPlace = formsPlace.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
        ChangeFormCard(isFirst: true);
    }

    public void ChangeFormCard(bool isLiked = false, bool isFirst = false) 
        => ChangeFormCard(chars.TakeRandomCard(randomAmount), isLiked, isFirst);

    public void ChangeFormCard(FormCard newForm, bool isLiked = false, bool isFirst = false)
    {
        if (!isFirst)
        {
            if (isLiked)
                Debug.Log($"You liked {curForm.Name}, {curForm.Age}");
            else
                Debug.Log($"You disliked {curForm.Name}, {curForm.Age}");
        }

        curForm = newForm;
        var texts = formsPlace.GetComponentsInChildren<Text>();
        texts[0].text = $"{curForm.Name}, {curForm.Age}"; 
        texts[1].text = curForm.Description;
        var texture = new Texture2D(2, 2);
        texture.LoadImage(File.ReadAllBytes(curForm.Pictures[0]));
        picPlace.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}

public class FormCard
{
    public string Name;
    public int Age;
    private Sex sex;
    public string[] Pictures;
    public string Description;
    private double fightProbability;
    private bool isSpecial;
    private int id;

    public FormCard(string name, int age, Sex sex, string[] pictures, string description, 
        double fightProbability = 1.0, bool isSpecial = false)
    {
        this.Name = name;
        this.Age = age;
        this.sex = sex;
        this.Pictures = pictures;
        this.Description = description;
        this.isSpecial = isSpecial;
        this.fightProbability = fightProbability;
    }

    public static FormCard GenerateForm()
    {
        var random = new System.Random();
        var age = random.Next(18, 45);
        var sex = (Sex) random.Next(2);
        var path = @"Assets\Forms\" + sex.ToString() + "Names.txt";
        var names = File.ReadAllLines(path);
        var picsFolder = new DirectoryInfo(@"Assets\Sprites\Characters\" + "Female" + @"\");
        var hairs = picsFolder.EnumerateFiles("Hair_*.png");
        var ups = picsFolder.EnumerateFiles("Up_*.png");
        var bottoms = picsFolder.EnumerateFiles("Bottom_*.png");
        var pics = new string[] { @"Assets\Sprites\Characters\" + "Female" + @"\" + "Body.png" };//hairs.GetRandom(), ups.GetRandom(), };

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
