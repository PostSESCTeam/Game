using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    private static int scaleSize = 20;
    public int balancedValue = (scaleSize / 2);
    public int scaleValue = 10;

    public void UpdateScale(int dt)
    {
        var newValue = scaleValue + dt;
        if (newValue < scaleSize && newValue > 0) scaleValue = newValue;
        else
        {
            Debug.Log("YOU DIED!");
            Destroy(FindObjectOfType<Act>());
        }
    }
}
