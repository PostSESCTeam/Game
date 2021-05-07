using System;
using System.Collections.Generic;

public class Characters
{
    private List<FormCard> plotCards = new List<FormCard>(new FormCard[] {
        new FormCard("Виктор", 433, Sex.Male, new string[] { @"Assets\Sprites\Characters\Plot\Victor.png" }, "Не кровосос, а дегустатор", 1.0, true),
        new FormCard("Фуршет", 12, Sex.Female, new string[] { @"Assets\Sprites\Characters\Plot\Fufe.png" }, "Невеста божья", 1.0, true),
        new FormCard("Марк", 129, Sex.Male, new string[] { @"Assets\Sprites\Characters\Plot\Reptiloid.png" }, "Что-то по рептилоидному", 1.0, true),
        new FormCard("Тварь", 37, Sex.Male, new string[] { @"Assets\Sprites\Characters\Plot\Tvar.png" }, "[ДАННЫЕ УДАЛЕНЫ]", 1.0, true),
        new FormCard("Лидия", 19, Sex.Female, new string[] { @"Assets\Sprites\Characters\Plot\Lydia.png" }, "Колюсь", 1.0, true),
        new FormCard("Эрик", 61, Sex.Male, new string[] { @"Assets\Sprites\Characters\Plot\Eric.png" }, "Спой мне, и я стану твоим личным Ангелом", 1.0, true),
        new FormCard("Дед", 73, Sex.Male, new string[] { @"Assets\Sprites\Characters\Plot\Ded.png" }, "К запаху привыкнешь", 1.0, true)
    });

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
        var index = new Random().Next(plotCards.Count + randomAmount);
        return index < plotCards.Count ? plotCards[index] : FormCard.GenerateForm();
    }
}
