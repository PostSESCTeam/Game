using System;
using UnityEngine;

public class Rival : DuelObject
{
    private const float fireRate = 1f;
    public Action<Vector3> RotateRival;
    public Action<Vector3> MoveRival;
    public Action<float> ShootRival;
    public Map Map;
    private DuelScale scale;
    public double NextMove = 0;

    private new void Start()
    {
        base.Start();
        GameObject rivalScale;

        do
            rivalScale = GameObject.Find("RivalScale");
        while (!rivalScale);

        scale = rivalScale.GetComponent<DuelScale>();
    }

    private new void FixedUpdate()
    {
        if (!Main.CanShoot) return;

        base.FixedUpdate();
        scale.UpdateScale(lives);
        var target = FindObjectOfType<Player>().gameObject.transform.position;
        MoveRival(target);
        RotateRival(target);
        ShootRival(fireRate);
    }
}
