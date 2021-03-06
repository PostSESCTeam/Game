using UnityEngine;
using UnityEngine.UI;

public class InfoInput : MonoBehaviour
{
    void Start()
    {
        var texts = GameObject.Find("ProfileText").GetComponentsInChildren<Text>();
        var input = gameObject.GetComponentInChildren<InputField>();
        input.onEndEdit.AddListener(i =>
        {
            texts[0].text = i;
            Main.IsSwipesFrozen = false;
            if (Main.IsTutorialOn)
                Main.StartTutorial.SetActive(true);
            Destroy(gameObject);
        });

        Main.Desc = texts[1];
    }
}
