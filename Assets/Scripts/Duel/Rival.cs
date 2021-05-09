using System;
using UnityEngine;

public class Rival : DuelObject
{
    private const float fireRate = 1.5f;
    public Action<Vector3> MoveRival;


    private new void Update()
    {
        base.Update();
        var target = FindObjectOfType<Player>().gameObject.transform.position;
        Rotate(target);
        MoveRival(target);
        Shoot(fireRate); // TODO: different shooting styles?
    }
}
