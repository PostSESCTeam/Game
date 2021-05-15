using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DuelBehaviours
{
    private static readonly Dictionary<string, Action<Rival, Vector3>> rotateRival = new List<(string Name, Action<Rival, Vector3> Action)>
    {
        ("Standard", (rival, target) => rival.Rotate(target)),
        ("Random", (rival, target) =>
        {
            var randPoint = UnityEngine.Random.insideUnitCircle;
            rival.Rotate(new Vector3(randPoint.x, randPoint.y) + rival.transform.position);
        })
    }.ToDictionary(i => i.Name, i => i.Action);

    private static readonly Dictionary<string, Action<Rival, Vector3>> moveRival = new List<(string Name, Action<Rival, Vector3> Action)>
    {
        ("Standard", (rival, target) => rival.Move(target, 0.03f)),
        ("Shy", (rival, target) => rival.Move(-target, 0.008f)),
        ("Lazy", (rival, target) =>
        {
            var direction = target.x - rival.transform.position.x;
            if (Mathf.Abs(direction) < 7)
                rival.Move(target, 0.004f);
        })
    }.ToDictionary(i => i.Name, i => i.Action);

    private static readonly Dictionary<string, Action<Rival, float>> shootRival = new List<(string, Action<Rival, float>)>
    {
        ("Standard", (rival, fireRate) => rival.Shoot(fireRate)),
        ("Random", (rival, fireRate) => rival.Shoot(UnityEngine.Random.Range(0.5f, 3f)))
    }.ToDictionary(i => i.Item1, i => i.Item2);

    public static void SetBehaviour(this Rival rival, string behName = null)
    {
        var rotateFunc = rotateRival.GetRandom().Value;
        var moveFunc = moveRival.GetRandom().Value;
        var shootFunc = shootRival.GetRandom().Value;

        if (behName != null)
        {
            rotateFunc = rotateRival[behName];
            moveFunc = moveRival[behName];
            shootFunc = shootRival[behName];
        }

        rival.SetBehaviour(rotateFunc, moveFunc, shootFunc);
    }

    public static void SetBehaviour(this Rival rival, Action<Rival, Vector3> rotate, Action<Rival, Vector3> move, Action<Rival, float> shoot)
    {
        Debug.Log("ye");
        rival.RotateRival = target => rotate(rival, target);
        rival.MoveRival = target => move(rival, target);
        rival.ShootRival = fireRate => shoot(rival, fireRate);
    }
}
