using System;
using UnityEngine;

public class Rival : DuelObject
{
    private const float fireRate = 2f;
    public Action<Vector3> RotateRival;
    public Action<Vector3> MoveRival;
    public Action<float> ShootRival;

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        var target = FindObjectOfType<Player>().gameObject.transform.position;
        RotateRival(target);
        MoveRival(target);
        ShootRival(fireRate);
    }
}
