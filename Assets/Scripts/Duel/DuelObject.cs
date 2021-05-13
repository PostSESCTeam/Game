using UnityEngine;

public abstract class DuelObject : MonoBehaviour
{
    private float nextFire = 0.0f;
    private int lives = 3;
    private bool isDied = false;
    private Transform transformBullet;

    private void Start() => transformBullet = Resources.Load<Transform>("Bullet");

    public void Update()
    {
        if (!isDied && lives == 0) Die();
    }

    public void Rotate(Vector3 destination)
    {
        Vector3 difference = (destination - transform.position).normalized;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    public void Move(Vector3 target, float speed) 
        => transform.position = Vector2.MoveTowards(transform.position, target, speed);

    public void Die() 
    {
        isDied = true;
        var animator = GameObject.Find("SceneChanger").GetComponent<Animator>();
        StartCoroutine(Main.FinishDuel(animator, this));
    }

    public void Shoot(float fireRate)
    {
        if (Time.time < nextFire) return;

        nextFire = Time.time + fireRate;
        var bullet = Instantiate(transformBullet,
                                 transform.position,
                                 transformBullet.transform.rotation).GetComponent<Bullet>();
        bullet.Parent = gameObject;
        bullet.Direction = transform.right;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var bullet = collision.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject)
            lives--;
    }
}
