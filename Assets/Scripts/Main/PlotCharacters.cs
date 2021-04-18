using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotCharacters
{
    private FormCard[] plotCards = new FormCard[] {
        new FormCard("������", 433, Sex.Male, "", 1.0, true),
        new FormCard("������", 12, Sex.Female, "������� �����", 1.0, true),
        new FormCard("����", 129, Sex.Male, "", 1.0, true)
    };

    public FormCard TakePlotCard(int number)
    {
        if (number < 0 || number >= plotCards.Length)
            throw new System.Exception();
        return plotCards[number];
    }

    //public FormCard 
}
