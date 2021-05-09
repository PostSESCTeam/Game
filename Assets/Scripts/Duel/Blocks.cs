using UnityEngine;
using UnityEngine.Tilemaps;

public class Blocks : MonoBehaviour
{
    private Tilemap tilemap;
    private GameObject block;

    private void Start()
    {
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("fad");
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            var pos = collision.gameObject.transform.position;
            var bullet = collision.GetComponent<Bullet>();
            var dir = bullet.Direction;
            var tileX = dir.x > 0 ? Mathf.CeilToInt(pos.x) : Mathf.FloorToInt(pos.x);
            var tileY = dir.y < 0 ? Mathf.CeilToInt(pos.y) : Mathf.FloorToInt(pos.y);
            tilemap.SetTile(new Vector3Int(tileX - 9, 4 - tileY, 0), null);
            tilemap.SetTile(tilemap.WorldToCell(pos), null);

            Debug.Log(new Vector3Int(tileX, tileY, 0));
            Debug.Log(tilemap.WorldToCell(pos));
           
            var collider = tilemap.GetComponent<TilemapCollider2D>();
            collider.gameObject.SetActive(false);
            collider.gameObject.SetActive(true);
        }
    }
}
