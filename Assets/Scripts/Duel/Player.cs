using System;
using UnityEngine;

public class Player : DuelObject
{
    private float speed = 10f;
    private float fireRate = 0.3f;
    private DuelScale scale;

    private new void Start()
    {
        base.Start();
        GameObject playerScale;

        do
            playerScale = GameObject.Find("PlayerScale");
        while (!playerScale);

        scale = playerScale.GetComponent<DuelScale>();
        Debug.Log(scale);
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        scale.UpdateScale(lives);

        // Кто придумал причислять 0 к положительным числам в Mathf.Sign...
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            Move(new Vector3(Math.Sign(Input.GetAxis("Horizontal")), 
                             Math.Sign(Input.GetAxis("Vertical"))), speed);

        Rotate(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetButtonDown("Fire1"))
            Shoot(fireRate);
    }
}
