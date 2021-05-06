using UnityEngine;

public class Player : MonoBehaviour
{
    private const float stepSize = 0.1f;
    private SpriteRenderer sprite;
    private float speed = 5.0f;
    public float fireRate = 0.3f;
    private float nextFire = 0.0f;
    private Vector3 mousePosition;
    private int lives = 3;

    public Transform transformBullet;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (lives < 1)
        {
            Debug.Log("You lose!");
            Destroy(gameObject);
            Act.UpdateAfterDuel(false);
            //StartFade();
        }
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            var dx = Input.GetAxis("Horizontal") > 0 ? stepSize : Input.GetAxis("Horizontal") < 0 ? -stepSize : 0;
            var dy = Input.GetAxis("Vertical") > 0 ? stepSize : Input.GetAxis("Vertical") < 0 ? -stepSize : 0;
            // можно переписать на Mathf.Sign(), но там какие-то проблемы
            Run(new Vector3(dx, dy));
        }

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = mousePosition - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
            Shoot();
    }

    public void Run(Vector3 direction) 
        => transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

    public void Shoot()
    {
        nextFire = Time.time + fireRate;
        Vector3 position = transform.position;
        position.y += 0.1f;
        var bullet = Instantiate(transformBullet, position, transformBullet.transform.rotation).GetComponent<Bullet>();
        bullet.Parent = gameObject;
        bullet.Direction = transform.right;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bullet = collision.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject)
            lives--;
    }
}
