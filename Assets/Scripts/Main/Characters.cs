using System;

public class Characters
{
    private readonly FormCard[] plotCards = new FormCard[] {
        new FormCard("Виктор", 433, Sex.Male, new string[] { @"Assets\Sprites\Characters\Female\Body.png" }, "", 1.0, true),
        new FormCard("Фуршет", 12, Sex.Female, new string[] { @"Assets\Sprites\Characters\Female\Body.png" }, "Невеста божья", 1.0, true),
        new FormCard("Марк", 129, Sex.Male, new string[] { @"Assets\Sprites\Characters\Female\Body.png" }, "", 1.0, true)
    };

    public FormCard TakePlotCard(int index)
    {
        if (index < 0 || index >= plotCards.Length)
            throw new ArgumentException("Incorrect index!");

        return plotCards[index];
    }

    public FormCard TakeRandomCard(int randomAmount)
    {
        var index = new Random().Next(plotCards.Length + randomAmount);
        return index < plotCards.Length ? plotCards[index] : FormCard.GenerateForm();
    }
}
