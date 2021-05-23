using UnityEngine;
using UnityEngine.Tilemaps;

public class Blocks : MonoBehaviour
{
    private Tilemap tilemap;
    public Map map;

    private void Start() 
        => tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            var pos = tilemap.WorldToCell(collision.gameObject.transform.position);
            tilemap.SetTile(pos, null);
            //map.Cells[pos.x - 7, pos.y - 5] = Cell.Empty;

            var collider = tilemap.GetComponent<TilemapCollider2D>();
            collider.gameObject.SetActive(false);
            collider.gameObject.SetActive(true);
        }
    }
}
