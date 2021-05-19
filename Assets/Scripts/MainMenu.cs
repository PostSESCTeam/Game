using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(() => StartCoroutine(PlayGame()));
        //GameObject.Find("Options").GetComponent<Button>().onClick.AddListener(OpenForms);
        //GameObject.Find("Authors").GetComponent<Button>().onClick.AddListener(OpenProfile);
        GameObject.Find("Quit").GetComponent<Button>().onClick.AddListener(QuitGame);
    }

    public IEnumerator PlayGame()
    {
        Debug.Log("Let's get started");
        GameObject.Find("SceneChanger").GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("MainScene");
        Main.Init();
        yield return new WaitWhile(() => SceneManager.GetActiveScene().name != "MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quited");
        Application.Quit();
    }
}
