using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private GameObject authors, settings;
    private bool isAuthorsOpen = false;
    private bool isSettingsOpen = false;

    private void Start()
    {
        authors = GameObject.Find("Authors");
        authors.GetComponentInChildren<Button>().onClick.AddListener(() => isAuthorsOpen = false);
        settings = GameObject.Find("Settings");
        settings.GetComponentInChildren<Button>().onClick.AddListener(() => isSettingsOpen = false);

        GameObject.Find("PlayBtn").GetComponent<Button>().onClick.AddListener(() => StartCoroutine(PlayGame()));
        GameObject.Find("OptionsBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            isAuthorsOpen = false;
            isSettingsOpen = true;
        });
        GameObject.Find("AuthorsBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            isAuthorsOpen = true;
            isSettingsOpen = false;
        });
        GameObject.Find("QuitBtn").GetComponent<Button>().onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        authors.SetActive(isAuthorsOpen);
        settings.SetActive(isSettingsOpen);
    }

    public IEnumerator PlayGame()
    {
        Debug.Log("Let's get started");
        //Main.IsTutorialOn = GameObject.Find("TutorialToggle").GetComponent<Toggle>().isOn;
        GameObject.Find("SceneChanger").GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("MainScene");
        yield return new WaitWhile(() => SceneManager.GetActiveScene().name != "MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quited");
        Application.Quit();
    }
}
