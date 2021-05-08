using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyRival : DuelObject
{
    private const float fireRate = 2.5f;

    private new void Update()
    {
        base.Update();
        var target = FindObjectOfType<Player>().gameObject.transform.position;
        Rotate(target);
        var direction = target.x - transform.position.x;
        if (Mathf.Abs(direction) < 7)
            Move(target, 0.004f);
        StartCoroutine(AutoShoot(fireRate));
    }
}
