using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rival : MonoBehaviour
{
    private int lives;
    public Animator animator;
    public Transform bullet;
    public float fireRate = 1.5f;
    private Vector3 target;

    private void Start()
    {
        lives = 3;
        target = FindObjectOfType<Player>().gameObject.transform.position;
        InvokeRepeating("Shoot", fireRate, fireRate);
    }

    private void Update()
    {
        if (lives < 1)
        {
            Debug.Log("You win!");
            Destroy(gameObject);
            Act.UpdateAfterDuel(true);
            StartFade();
        }

        var player = FindObjectOfType<Player>();
        if (player)
            target = player.gameObject.transform.position;
        else
        {
            Destroy(gameObject);
            StartFade();
        }
    }

    public void Shoot()
    {
        var position = transform.position;
        position.y += 0.1f;
        var newBullet = Instantiate(bullet, position, bullet.transform.rotation).GetComponent<Bullet>() as Bullet;
        newBullet.Parent = gameObject;
        // TODO: определить правильное направление движения пули
        newBullet.Direction = -newBullet.transform.right;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bullet = collision.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject)
            lives--;
    }

    public void StartFade()
    {
        animator.SetTrigger("FadeOut");
    }

    public void FuckGoBack()
    {
        SceneManager.UnloadSceneAsync("Duel");
        foreach (var i in SceneManager.GetActiveScene().GetRootGameObjects())
            i.SetActive(true);
    }
}
