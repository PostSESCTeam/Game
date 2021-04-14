using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Form
{
    public string Name;
    public int Age;
    public Sex Sex;
    public string Description;

    private static void GenerateForm()
    {
        var random = new System.Random();
        Age = random.Next(18, 45);
        Sex = (Sex)random.Next(2);
        if (Sex == Sex.Female)
        {

        }
    }
}

public enum Sex
{
    Male,
    Female
}
