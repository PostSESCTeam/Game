using UnityEngine;
using UnityEngine.UI;

public class MainScale : Scale
{
    public const int ScaleSize = 20, BalancedValue = ScaleSize / 2;

    public new void Start()
    {
        base.Start();
        Init(ScaleSize);
    }
}
