using System;
using UnityEngine;

public class Player : DuelObject
{
    private float speed = 10.0f;
    private float fireRate = 0.3f;

    private new void Update()
    {
        base.Update();

        // Кто придумал причислять 0 к положительным числам в Mathf.Sign...
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            Move(new Vector3(Math.Sign(Input.GetAxis("Horizontal")), 
                             Math.Sign(Input.GetAxis("Vertical"))), 0.001f);

        Rotate(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetButtonDown("Fire1"))
            Shoot(fireRate);
    }

    public void Run(Vector3 direction) 
        => transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
}
