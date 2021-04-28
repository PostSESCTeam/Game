using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 15.0f;
    private Vector3 direction;
    private SpriteRenderer sprite;

    public Vector3 Direction { set { direction = value; } }

    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        //sprite.sprite = Utils.GetSpriteFromFile(@"Assets\Sprites\Duel\Bullet.png");
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
}
