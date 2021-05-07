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
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();

        var playerPos = map.EmptyCells.GetRandom();
        player.transform.position = new Vector3(playerPos.X - 9, 4 - playerPos.Y);

        var rivalPos = map.EmptyCells.Except(new (int, int)[] { playerPos })
            .Where(i => Mathf.Abs(playerPos.X - i.Item1) + Mathf.Abs(playerPos.Y - i.Item2) >= 6)
            .GetRandom();

        rival.transform.position = new Vector3(rivalPos.Item1 - 9, 4 - rivalPos.Item2);
        DrawMap();
    }

    private void DrawMap()
    {
        foreach (var i in map.Blocks)
            foreach (var (X, Y) in i)
                tilemap.SetTile(new Vector3Int(X - 9, 4 - Y, 0), tile);

        var collider = tilemap.GetComponent<TilemapCollider2D>();
        collider.gameObject.SetActive(false);
        collider.gameObject.SetActive(true);
    }
}
