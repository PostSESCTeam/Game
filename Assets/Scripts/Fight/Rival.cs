using UnityEngine;
using UnityEngine.SceneManagement;

public class Rival : MonoBehaviour
{
    private int lives;

    private void Start()
    {
        lives = 3;
    }

    private void Update()
    {
        if (lives < 1)
        {
            Debug.Log("You win!");
            Destroy(gameObject);
            Act.UpdateAfterDuel(true);
            FuckGoBack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>())
            lives--; 
    }

    public void FuckGoBack()
    {
        SceneManager.UnloadSceneAsync("Duel");
        foreach (var i in SceneManager.GetActiveScene().GetRootGameObjects())
            i.SetActive(true);
    }
}
