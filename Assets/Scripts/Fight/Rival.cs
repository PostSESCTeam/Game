using UnityEngine;

public class Rival : MonoBehaviour
{
    private int lives;
    public Animator animator;

    private void Start()
    {
        lives = 3;
    }

    private void Update()
    {
        if (lives < 1)
        {
            Debug.Log("You win!");
            Act.UpdateAfterDuel(true);
            StartCoroutine(Main.FinishDuel(animator, true));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>())
            lives--; 
    }
}
