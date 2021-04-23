using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Form : MonoBehaviour
{
    private int randomAmount = 5;
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
        Redraw(curForm.Pictures, curForm.IsSpecial, drawPlaces);
    }

    private void Redraw(string[] files, bool isSpecial, SpriteRenderer[] places)
    {
        if (isSpecial)
        {
            places[0].gameObject.SetActive(true);
            for (var i = 1; i < 5; i++)
                places[i].gameObject.SetActive(false);
            places[0].sprite = GetSpriteFromFile(files[0]);
        }
        else
        {
            places[0].gameObject.SetActive(false);
            for (var i = 1; i < 5; i++)
            {
                places[i].gameObject.SetActive(true);
                places[i].sprite = GetSpriteFromFile(files[i - 1]);
            }
        }
    }

    private Sprite GetSpriteFromFile(string path)
    {
        var texture = new Texture2D(2, 2);
        texture.LoadImage(File.ReadAllBytes(path));
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
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
    public bool IsSpecial;
    private int id;

    public FormCard(string name, int age, Sex sex, string[] pictures, string description, 
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

    public static FormCard GenerateForm()
    {
        var random = new System.Random();
        var age = random.Next(18, 45);
        var sex = (Sex) random.Next(2);
        var namesPath = @"Assets\Forms\" + sex.ToString() + "Names.txt";
        var names = File.ReadAllLines(namesPath);
        var path = @"Assets\Sprites\Characters\" + "Female" + @"\";
        var picsFolder = new DirectoryInfo(path);
        //var id = random.Next(1).ToString();
        var hairs = picsFolder.EnumerateFiles("Hair_*.png");
        var ups = picsFolder.EnumerateFiles("Up_*.png");
        var bottoms = picsFolder.EnumerateFiles("Bottom_*.png");
        var pics = new string[] { path + "Body.png", path + hairs.GetRandom().Name, path + ups.GetRandom().Name, path + bottoms.GetRandom().Name };

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
