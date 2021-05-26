using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RivalBehaviours
{
    private static readonly Dictionary<string, Action<Rival, Vector3>> rotateRival
        = new List<(string, Action<Rival, Vector3>)>
    {
        ("Standard", (rival, target) => rival.Rotate(target)),
        //("Random", (rival, target) =>
        //{
        //    var randPoint = UnityEngine.Random.insideUnitCircle;
        //    rival.Rotate(new Vector3(randPoint.x, randPoint.y) + rival.transform.position);
        //})
    }.ToDictionary(i => i.Item1, i => i.Item2);

    private static readonly Dictionary<string, Action<Rival, Vector3>> moveRival
        = new List<(string, Action<Rival, Vector3>)>
    {
        ("Standard", (rival, target) =>
        {
            if ((target - rival.transform.position).magnitude > 2)
                rival.Move(target, 1f);
        }),
        ("Crazy", (rival, target) =>
        {
            if ((target - rival.transform.position).magnitude > 2)
                rival.Move(target, 3f);
        }),
        ("Lazy", (rival, target) =>
        {
            var dist = (target - rival.transform.position).magnitude;
            if (dist > 2 && dist < 7)
                rival.Move(target, 0.2f);
        }),
        ("Rational", (rival, target) =>
        {
            if ((target - rival.transform.position).magnitude > 2)
            {
                var start = rival.transform.position;
                var startInt = (X: Mathf.RoundToInt(start.x) + 7, Y: Mathf.RoundToInt(start.y) + 5);
                var res = (X: Mathf.RoundToInt(target.x) + 7, Y: Mathf.RoundToInt(target.y) + 5);
                var paths = rival.Map.FindPaths(startInt);

                try
                {
                    while (paths[res] != startInt)
                        res = paths[res];

                    var dir = new Vector3(res.X - startInt.X, res.Y - startInt.Y);
                    rival.Move(dir, 3f);
                }
                catch { }
            }
        }),
        ("Teleport", (rival, target) => 
        {
            if (Time.time < rival.NextMove) return;

            var start = rival.transform.position;
            var range = new int[] { -2, -1, 0, 1, 2 };

            var res = range.SelectMany(i => range.Select(j => new Vector3(i, j) + target))
                .Where(i =>
                {
                    var a = (X: Mathf.RoundToInt(i.x) + 7, Y: Mathf.RoundToInt(i.y) + 5);

                    return i.x > 0 && i.y > 0 && rival.Map.IsInBounds(a) && rival.Map.IsEmpty(a);
                })
                .GetRandom();

            rival.transform.position = res;
            rival.NextMove = Time.time + 2;
        })
    }.ToDictionary(i => i.Item1, i => i.Item2);

    private static readonly Dictionary<string, Action<Rival, float>> shootRival
        = new List<(string, Action<Rival, float>)>
    {
        ("Standard", (rival, fireRate) => rival.Shoot(fireRate)),
        ("Crazy", (rival, fireRate) => rival.Shoot(fireRate / 2)),
        ("Random", (rival, fireRate) => rival.Shoot(UnityEngine.Random.Range(1f, 3f)))
    }.ToDictionary(i => i.Item1, i => i.Item2);

    public static void SetBehaviour(this Rival rival, string behName = null)
    {
        Action<Rival, Vector3> rotateFunc = null, moveFunc = null;
        Action<Rival, float> shootFunc = null;

        if (behName != null)
        {
            rotateRival.TryGetValue(behName, out rotateFunc);
            moveRival.TryGetValue(behName, out moveFunc);
            shootRival.TryGetValue(behName, out shootFunc);
        }

        rival.SetBehaviour(rotateFunc ?? rotateRival.GetRandom().Value,
                           moveFunc ?? moveRival.GetRandom().Value,
                           shootFunc ?? shootRival.GetRandom().Value);
    }

    public static void SetBehaviour(this Rival rival, Action<Rival, Vector3> rotate,
        Action<Rival, Vector3> move, Action<Rival, float> shoot)
    {
        rival.RotateRival = target => rotate(rival, target);
        rival.MoveRival = target => move(rival, target);
        rival.ShootRival = fireRate => shoot(rival, fireRate);
    }

    private static Dictionary<(int, int), (int, int)> FindPaths(this Map map, (int, int) start)
    {
        var paths = new Dictionary<(int, int), (int, int)>();
        var queue = new Queue<(int, int)>();
        paths.Add(start, (-1, -1));
        queue.Enqueue(start);

        while (queue.Count != 0)
        {
            var point = queue.Dequeue();
            foreach (var newPoint in point.GetNeighbours()
                .Where(i => map.IsInBounds(i) && !paths.ContainsKey(i)))
            {
                queue.Enqueue(newPoint);
                paths.Add(newPoint, point);
            }
        }

        return paths;
    }
}
