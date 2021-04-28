using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float stepSize = 0.1f;
    private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    private float speed = 5.0f;
    private Bullet bullet;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
    }

    void Update()
    {
        if (Input.GetButton("Horizontal") && Input.GetButton("Vertical"))
        {
            Run(new Vector3(Input.GetAxis("Horizontal") > 0 ? stepSize : -stepSize, Input.GetAxis("Vertical") > 0 ? stepSize : -stepSize));
        }
        else if (Input.GetButton("Horizontal"))
        {
            Run(new Vector3(Input.GetAxis("Horizontal") > 0 ? stepSize : -stepSize, 0));
            sprite.flipX = Input.GetAxis("Horizontal") < 0.0f;
        } 
        else if (Input.GetButton("Vertical"))
        {

            Run(new Vector3(0, Input.GetAxis("Vertical") > 0 ? stepSize : -stepSize));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Run(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    public void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.1f;
        Instantiate(bullet, position, bullet.transform.rotation);
    }
}
