using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyRival : DuelObject
{
    private const float fireRate = 0.2f;

    private new void Update()
    {
        base.Update();
        var target = FindObjectOfType<Player>().gameObject.transform.position;
        Rotate(target);
        Move(target, 0.03f);
        StartCoroutine(AutoShoot(fireRate));
    }
}
