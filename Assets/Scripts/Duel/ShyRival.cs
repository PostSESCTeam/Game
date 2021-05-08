using UnityEngine;
using UnityEngine.EventSystems;

public class ShyRival : DuelObject
{
    private const float fireRate = 1.5f;

    private new void Update()
    {
        base.Update();
        var target = FindObjectOfType<Player>().gameObject.transform.position;
        Rotate(target);
        Move(-target, 0.008f);
        StartCoroutine(AutoShoot(fireRate));
    }
}