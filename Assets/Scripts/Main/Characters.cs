using System;
using System.Collections;
using UnityEngine;

public class Characters
{
    private FormCard[] plotCards = new FormCard[] {
        new FormCard("Виктор", 433, Sex.Male, "", 1.0, true),
        new FormCard("Фуршет", 12, Sex.Female, "Невеста божья", 1.0, true),
        new FormCard("Марк", 129, Sex.Male, "", 1.0, true)
    };

    public FormCard TakePlotCard(int index)
    {
        if (index < 0 || index >= plotCards.Length)
            throw new ArgumentException("Incorrect index!");

        return plotCards[index];
    }

    public FormCard TakeRandomCard(int randomAmount)
    {
        var index = new System.Random().Next(plotCards.Length + randomAmount);
        try
        {
            return plotCards[index];
        }
        catch
        {
            return FormCard.GenerateForm();
        }
    }
}
