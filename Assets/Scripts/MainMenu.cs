using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(PlayGame);
        //GameObject.Find("Options").GetComponent<Button>().onClick.AddListener(OpenForms);
        //GameObject.Find("Authors").GetComponent<Button>().onClick.AddListener(OpenProfile);
        GameObject.Find("Quit").GetComponent<Button>().onClick.AddListener(QuitGame);
    }

    public void PlayGame()
    {
        Debug.Log("Let's get started");
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quited");
        Application.Quit();
    }
}
