using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    private GameObject authors, settings, load;
    private bool isAuthorsOpen = false, isSettingsOpen = false, isTutorialOn = true;

    private void Start()
    {
        authors = GameObject.Find("Authors");
        authors.GetComponentInChildren<Button>().onClick.AddListener(() => isAuthorsOpen = false);
        settings = GameObject.Find("Settings");
        settings.GetComponentInChildren<Button>().onClick.AddListener(() => isSettingsOpen = false);

        load = GameObject.Find("Loading");
        load.SetActive(false);

        var toggle = FindObjectOfType<Toggle>();
        toggle.onValueChanged.AddListener(value => isTutorialOn = value);

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
        var animator = GameObject.Find("SceneChanger").GetComponent<Animator>();
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        animator.gameObject.SetActive(false);

        var oper = SceneManager.LoadSceneAsync("MainScene");
        Main.IsTutorialOn = isTutorialOn;
        Main.Chats = new List<Chat>();
        load.SetActive(true);

        while (!oper.isDone)
            yield return null;
    }

    public void QuitGame()
    {
        Debug.Log("Quited");
        Application.Quit();
    }
}
