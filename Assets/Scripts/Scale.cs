using UnityEngine;
using UnityEngine.UI;

public class Scale : MonoBehaviour
{
    private int ScaleSize;
    private Image scale;
    private float oldFillAmount = 0.5f, newFillAmount = 0.5f, t = 0.0f;

    public void Start() => scale = transform.GetComponentInChildren<Image>();

    public void FixedUpdate()
    {
        scale.fillAmount = Mathf.Lerp(oldFillAmount, newFillAmount, t);
        t += 0.5f * Time.deltaTime;

        if (t > 1.0f)
        {
            oldFillAmount = newFillAmount;
            t = 0;
        }
    }

    public void Init(int maxVal, float startFill = 0.5f)
    {
        ScaleSize = maxVal;
        oldFillAmount = startFill;
        newFillAmount = startFill;
    }

    public void UpdateScale(int newScaleValue)
    {
        t = 0;
        oldFillAmount = newFillAmount;
        newFillAmount = (float)newScaleValue / ScaleSize;
    }
}
