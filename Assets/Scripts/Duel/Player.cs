using System;
using UnityEngine;

public class Player : DuelObject
{
    private float speed = 10f;
    private float fireRate = 0.3f;

    private new void FixedUpdate()
    {
        base.FixedUpdate();

        // Кто придумал причислять 0 к положительным числам в Mathf.Sign...
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            Move(new Vector3(Math.Sign(Input.GetAxis("Horizontal")), 
                             Math.Sign(Input.GetAxis("Vertical"))), speed);

        Rotate(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetButtonDown("Fire1"))
            Shoot(fireRate);
    }
}
