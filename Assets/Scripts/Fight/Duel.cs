using UnityEngine;
using UnityEngine.Tilemaps;

public class Duel : MonoBehaviour
{
    public Tile tile;
    private Map map;
    private Player player;
    private Rival rival;
    private Tilemap tilemap;

    private void Start()
    {
        map = Map.GenerateMap(10, 18);
        player = FindObjectOfType<Player>();
        rival = FindObjectOfType<Rival>();
        tilemap = FindObjectOfType<Tilemap>();

        player.transform.position = new Vector3(Random.Range(-9, 8), Random.Range(-5, 4));
        rival.transform.position = new Vector3(Random.Range(-9, 8), Random.Range(-5, 4));
        DrawMap();
    }

    private void DrawMap()
    {
        for (var y = 0; y < map.Cells.GetLength(0); y++)
            for (var x = 0; x < map.Cells.GetLength(1); x++)
                if (map.Cells[y, x] == Cell.Wall)
                    tilemap.SetTile(new Vector3Int(x - 9, 4 - y, 0), tile);
        var collider = tilemap.GetComponent<TilemapCollider2D>();
        collider.gameObject.SetActive(false);
        collider.gameObject.SetActive(true);
    }

    void Update()
    {
        
    }
}
