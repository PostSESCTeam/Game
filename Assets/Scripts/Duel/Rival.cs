using System;
using UnityEngine;

public class Rival : DuelObject
{
    private const float fireRate = 5f;
    public Action<Vector3> RotateRival;
    public Action<Vector3> MoveRival;
    public Action<float> ShootRival;

    private new void Update()
    {
        base.Update();
        var target = FindObjectOfType<Player>().gameObject.transform.position;
        RotateRival(target);
        MoveRival(target);
        ShootRival(fireRate);
    }
}
