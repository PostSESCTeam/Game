using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float speed = 15.0f;
    public Vector3 Direction;
    public GameObject Parent;

    private void Start() => Destroy(gameObject, 3);

    private void Update() 
        => transform.position = Vector3.MoveTowards(transform.position,
                                                    transform.position + Direction,
                                                    speed * Time.deltaTime);

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject != Parent)
            Destroy(gameObject);
    }
}
