using UnityEngine;
using UnityEngine.UI;

public class Scale : MonoBehaviour
{
    public const int ScaleSize = 20, BalancedValue = ScaleSize / 2;
    private Image scale;
    private float oldFillAmount = (float) BalancedValue / ScaleSize,
        newFillAmount = (float) BalancedValue / ScaleSize, t = 0.0f;

    private void Start() => scale = transform.GetComponentInChildren<Image>();

    private void FixedUpdate()
    {
        scale.fillAmount = Mathf.Lerp(oldFillAmount, newFillAmount, t);
        t += 0.5f * Time.deltaTime;

        if (t > 1.0f)
        {
            oldFillAmount = newFillAmount;
            t = 0;
        }
    }

    public void UpdateScale(int newScaleValue)
    {
        oldFillAmount = newFillAmount;
        t = 0;
        newFillAmount = (float)newScaleValue / ScaleSize;
    }
}
