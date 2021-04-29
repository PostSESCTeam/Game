using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject parent;
    private const float speed = 15.0f;
    private Vector3 direction;

    public Vector3 Direction { set => direction = value; }

    public GameObject Parent { set => parent = value; }

    private void Start() => Destroy(gameObject, 5);

    private void Update() 
        => transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != parent)
            Destroy(gameObject);
    }
}
