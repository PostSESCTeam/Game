using UnityEngine;
using UnityEngine.Tilemaps;

public class Blocks : MonoBehaviour
{
    private Tilemap tilemap;
    private GameObject block;

    private void Start()
    {
        tilemap = FindObjectOfType<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            var pos = collision.gameObject.transform.position;
            tilemap.SetTile(tilemap.WorldToCell(pos), null);
            var collider = tilemap.GetComponent<TilemapCollider2D>();
            collider.gameObject.SetActive(false);
            collider.gameObject.SetActive(true);
            //пока что тело блока остается на карте
        }
    }
}
