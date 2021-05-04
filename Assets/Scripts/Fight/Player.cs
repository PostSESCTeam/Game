using UnityEngine;

public class Player : MonoBehaviour
{
    private const float stepSize = 0.1f;
    private SpriteRenderer sprite;
    private float speed = 5.0f;

    private Vector3 mousePosition;

    public Transform transformBullet;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
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

        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    public void Run(Vector3 direction) 
        => transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

    public void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.1f;
        var bullet = Instantiate(transformBullet, position, transformBullet.transform.rotation).GetComponent<Bullet>();
        bullet.Parent = gameObject;
        // TODO: определить правильное направление движения пули
        bullet.Direction = transform.right;
    }
}
