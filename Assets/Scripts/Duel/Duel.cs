using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Duel : MonoBehaviour
{
    private Tile[] tiles;
    private Map map;
    private Player player;
    private Rival rival;
    private Tilemap tilemap;

    private void Start()
    {
        tiles = Resources.LoadAll<Tile>("Tiles");
        map = Map.GenerateMap(10, 18);
        player = FindObjectOfType<Player>();
        rival = FindObjectOfType<Rival>();
        OnInitRival.Invoke();
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();

        var emptyCells = map.EmptyCells.Where(i => i.X > 0 && i.Y > 0);
        var playerPos = emptyCells.GetRandom();
        player.transform.position = new Vector3(playerPos.X - 9, playerPos.Y - 5);

        var rivalPos = emptyCells.Except(new (int, int)[] { playerPos })
            .Where(i => Mathf.Abs(playerPos.X - i.Item1) + Mathf.Abs(playerPos.Y - i.Item2) >= 6)
            .GetRandom();

        rival.transform.position = new Vector3(rivalPos.Item1 - 9, rivalPos.Item2 - 5);
        //rival.SetBehaviour((rival, target) => { }, (rival, target) => { }, (rival, fireRate) => { });
        DrawMap();
    }

    public void SetRivalBehaviour(string behName = null) => rival.SetBehaviour(behName);

    public event Action OnInitRival;

    private void DrawMap()
    {
        foreach (var i in map.Blocks)
        {
            var tile = tiles.GetRandom();
            foreach (var (X, Y) in i)
                tilemap.SetTile(new Vector3Int(X - 9, Y - 5, 0), tile);
        }

        var collider = tilemap.GetComponent<TilemapCollider2D>();
        collider.gameObject.SetActive(false);
        collider.gameObject.SetActive(true);
    }
}