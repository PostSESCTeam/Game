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
            if ((target - rival.transform.position).magnitude > 1)
                rival.Move(target, 1f);
        }),
        ("Crazy", (rival, target) =>
        {
            if ((target - rival.transform.position).magnitude > 3)
                rival.Move(target, 3f);
        }),
        //("Shy", (rival, target) => rival.Move(-target, 0.8f)),
        ("Lazy", (rival, target) =>
        {
            if ((target - rival.transform.position).magnitude < 7)
                rival.Move(target, 0.2f);
        }),
        //("Rational", (rival, target) =>
        //{
        //    Debug.Log("well fuck");
        //    if ((target - rival.transform.position).magnitude > 3)
        //    {
        //        var start = rival.transform.position;
        //        var startInt = (Mathf.FloorToInt(start.x), Mathf.FloorToInt(start.y));
        //        var paths = FindPaths(startInt, rival.Map);

        //        var a = (Mathf.FloorToInt(target.x), Mathf.FloorToInt(target.y));
        //        while (a != startInt)
        //            a = paths[a];

        //        var dir = new Vector3(a.Item1, a.Item2);
        //        rival.Move(dir, (dir - target).magnitude);
        //    }
        //})
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

    private static Dictionary<(int, int), (int, int)> FindPaths((int, int) start, Map map)
    {
        var paths = new Dictionary<(int, int), (int, int)>();
        var queue = new Queue<(int, int)>();
        paths.Add(start, (-1, -1));
        queue.Enqueue(start);

        while (queue.Count != 0)
        {
            var point = queue.Dequeue();
            foreach (var newPoint in point.GetNeighbours()
                .Where(i => map.IsInBounds(i) && !paths.ContainsKey(i) && map.Cells[i.Y, i.X] == Cell.Empty))
            {
                queue.Enqueue(newPoint);
                paths.Add(newPoint, point);
            }
        }

        return paths;
    }
}
