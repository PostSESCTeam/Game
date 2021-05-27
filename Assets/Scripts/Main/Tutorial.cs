using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    void Start()
    {
        if (!Main.IsTutorialOn)
            Destroy(gameObject);

        Main.StartTutorial = GameObject.Find("AboutGame");
        var aboutScale = GameObject.Find("AboutScale");
        var aboutProfile = GameObject.Find("AboutProfile");
        var aboutForms = GameObject.Find("AboutForms");
        Main.ChatsTutorial = GameObject.Find("AboutChats");
        Main.DuelTutorial = GameObject.Find("AboutDuel");

        Main.StartTutorial.GetComponentInChildren<Button>().onClick.AddListener(() => 
        {
            Main.StartTutorial.SetActive(false);
            aboutScale.SetActive(true);
        });

        aboutScale.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            aboutScale.SetActive(false);
            aboutProfile.SetActive(true);
        });

        aboutProfile.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            aboutProfile.SetActive(false);
            aboutForms.SetActive(true);
        });

        aboutForms.GetComponentInChildren<Button>().onClick.AddListener(() => aboutForms.SetActive(false));

        Main.ChatsTutorial.GetComponentInChildren<Button>().onClick.AddListener(() => Main.ChatsTutorial.SetActive(false));

        Main.DuelTutorial.GetComponentInChildren<Button>().onClick.AddListener(() => Destroy(gameObject));

        Main.StartTutorial.SetActive(false);
        aboutScale.SetActive(false);
        aboutProfile.SetActive(false);
        aboutForms.SetActive(false);
        Main.ChatsTutorial.SetActive(false);
        Main.DuelTutorial.SetActive(false);
    }
}
