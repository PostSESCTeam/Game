using UnityEngine;

public abstract class DuelObject : MonoBehaviour
{
    private float nextFire = 0.0f;
    public int lives = 3;
    private bool isDied = false;
    private Transform transformBullet;
    private new Rigidbody2D rigidbody;

    public void Start()
    {
        transformBullet = Resources.Load<Transform>("Bullet");
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        if (!isDied && lives == 0) Die();
        rigidbody.velocity = new Vector2();
    }

    public void Rotate(Vector3 destination)
    {
        Vector3 difference = (destination - transform.position).normalized;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    public void Move(Vector3 target, float speed) => rigidbody.velocity = target.normalized * speed;

    public void Die() 
    {
        isDied = true;
        var animator = GameObject.Find("SceneChanger").GetComponent<Animator>();
        StartCoroutine(Main.FinishDuel(animator, this));
    }

    public void Shoot(float fireRate)
    {
        if (Time.time < nextFire || !Main.CanShoot) return;

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
