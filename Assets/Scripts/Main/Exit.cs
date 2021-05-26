using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    private void Start()
    {
        var exitConfirmation = GameObject.Find("Confirmation");

        GameObject.Find("Yes").GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("exit");
            SceneManager.LoadScene("MainMenu");
        });
        GameObject.Find("No").GetComponent<Button>().onClick.AddListener(() => SetWindowActive(exitConfirmation, false));
        GameObject.Find("Exit").GetComponentInChildren<Button>().onClick.AddListener(() => SetWindowActive(exitConfirmation, true));

        exitConfirmation.SetActive(false);
    }

    private void SetWindowActive(GameObject window, bool isActive)
    {
        Main.IsSwipesFrozen = isActive;
        window.SetActive(isActive);
    }
}
