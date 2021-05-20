using System;
using UnityEngine;

public class Rival : DuelObject
{
    private const float fireRate = 2f;
    public Action<Vector3> RotateRival;
    public Action<Vector3> MoveRival;
    public Action<float> ShootRival;
    public Map Map;
    private DuelScale scale;

    private new void Start()
    {
        base.Start();
        scale = GameObject.Find("RivalScale").GetComponent<DuelScale>();
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        scale.UpdateScale(lives);
        var target = FindObjectOfType<Player>().gameObject.transform.position;
        RotateRival(target);
        MoveRival(target);
        ShootRival(fireRate);
    }
}
