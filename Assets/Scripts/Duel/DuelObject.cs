using System.Collections;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public abstract class DuelObject : MonoBehaviour
{
    private float nextFire = 0.0f;
    private int lives = 3;
    public Transform transformBullet;

    public void Update()
    {
        if (lives == 0) Die();
    }

    public void Rotate(Vector3 destination)
    {
        Vector3 difference = (destination - transform.position).normalized;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    public void Move(Vector3 target, float speed )
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed);
    }

    public void Die() 
    {
        var animator = GameObject.Find("SceneChanger").GetComponent<Animator>();
        StartCoroutine(Main.FinishDuel(animator, !(this is Player)));
    }

    public IEnumerator AutoShoot(float fireRate)
    {
        yield return new WaitForSeconds(fireRate);
        Shoot(fireRate);
    }

    public void Shoot(float fireRate)
    {
        if (Time.time < nextFire) return;

        nextFire = Time.time + fireRate;
        Vector3 position = transform.position;
        var bullet = Instantiate(transformBullet, position, transformBullet.transform.rotation).GetComponent<Bullet>();
        bullet.Parent = gameObject;
        bullet.Direction = transform.right;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var bullet = collision.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject)
            lives--;
    }
}
