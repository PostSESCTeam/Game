using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Characters
{
    private List<FormCard> plotCards;

    public Characters()
    {
        plotCards = new List<FormCard>(new FormCard[]
        {
            //CreatePlotCharacter("Виктор", 433, Sex.Male, "Не кровосос, а дегустатор", 1.0, "Victor"),
            //CreatePlotCharacter("Фуршет", 12, Sex.Female, "Невеста божья", 1.0, "Fufe"),
            //CreatePlotCharacter("Марк", 129, Sex.Male, "Что-то по рептилоидному", 1.0, "Reptiloid", "Teleport"),
            //CreatePlotCharacter("Тварь", 37, Sex.Male, "[ДАННЫЕ УДАЛЕНЫ]", 1.0, "Tvar", "Crazy"),
            //CreatePlotCharacter("Лидия", 19, Sex.Female, "Колюсь", 1.0, "Lydia"),
            //CreatePlotCharacter("Эрик", 61, Sex.Male, "Спой мне, и я стану твоим личным Ангелом", 1.0, "Eric"),
            //CreatePlotCharacter("Алексей", 19, Sex.Male, "Ходит дурачок по лесу, ищет дурачок глупее себя", 1.0, "Alexey", "Crazy"),
            //CreatePlotCharacter("Дед", 73, Sex.Male, "К запаху привыкнешь", 1.0, "Ded"),
            //CreatePlotCharacter("Константин", 19, Sex.Male, "заведу роман с твоим скелетом в шкафу", 1.0, "Skelet"),
            //CreatePlotCharacter("Игорь", 15, Sex.Male, "зацени прессак", 1.0, "Igor"),
            //CreatePlotCharacter("Иннокентий", 16, Sex.Male, "Рогатым рогов не наставить!", 1.0, "Kesha"),
            //CreatePlotCharacter("Леня", 16, Sex.Male, "Я с незнакомыми не знакомлюсь!!!", 1.0, "Leonid"),
            //CreatePlotCharacter("Фиса", 8, Sex.Female, "КИС КИС КИС КИС Я КОТИК ТЫ КОТИК", 0.0, "Koshka"),
            //CreatePlotCharacter("Марго", 32, Sex.Female, "СКУЧАЕШЬ? МОЙ САЙТ [скрыто настройками фильтров]", 1.0, "Margo")
        });

        Main.Dialogs = plotCards.Select(i => (i.FullName, new Dialog(File.ReadAllLines($@"Assets\Chats\{i.LatinName}.txt"))))
            .ToDictionary(i => i.Item1, i => i.Item2);
    }

    public FormCard TakePlotCard(int index)
    {
        if (index < 0 || index >= plotCards.Count)
            throw new ArgumentException("Incorrect index!");

        var res = plotCards[index];
        plotCards.RemoveAt(index);
        return res;
    }

    public FormCard TakeRandomCard(int randomAmount)
    {
        var index = UnityEngine.Random.Range(0, plotCards.Count + randomAmount);
        return index < plotCards.Count ? TakePlotCard(index) : FormCard.GenerateForm();
    }

    private FormCard CreatePlotCharacter(string name, int age, Sex sex, string description,
        double fightProbability, string latinName, string characterSet = null) 
        => new FormCard(name, age, sex, new string[] { $@"Assets\Sprites\Characters\Plot\{latinName}.png" },
              description, fightProbability, true, latinName, characterSet);
}
