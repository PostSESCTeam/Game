using UnityEngine;
using UnityEngine.UI;

public class Scale : MonoBehaviour
{
    public const int ScaleSize = 20, BalancedValue = ScaleSize / 2;
    private Image scale;
    private float fillAmount = 0.5f;

    private void Start() => scale = transform.GetComponentInChildren<Image>();

    //TODO: smooth changing
    public void UpdateScale(int newScaleValue) => scale.fillAmount = (float)newScaleValue / ScaleSize;
}
