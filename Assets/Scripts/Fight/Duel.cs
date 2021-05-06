using System.Linq;
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
        var playerPos = map.EmptyCells.GetRandom();
        player.transform.position = new Vector3(playerPos.X - 9, 4 - playerPos.Y);

        var rivalPos = map.EmptyCells.Except(new (int, int)[] { playerPos })
            .Where(i => Mathf.Abs(playerPos.X - i.Item1) + Mathf.Abs(playerPos.Y - i.Item2) >= 6)
            .GetRandom();

        Debug.Log(rivalPos);
        rival.transform.position = new Vector3(rivalPos.Item1 - 9, 4 - rivalPos.Item2);
        Debug.Log(rival.transform.position);
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
}
