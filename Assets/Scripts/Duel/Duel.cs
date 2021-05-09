using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Duel : MonoBehaviour
{
    public static readonly Action<Rival, Vector3>[] moveRival = new Action<Rival, Vector3>[]
    {
        (rival, target) => rival.Move(target, 0.03f),
        (rival, target) => rival.Move(-target, 0.008f),
        (rival, target) =>
        {
            var direction = target.x - rival.transform.position.x;
            if (Mathf.Abs(direction) < 7)
                rival.Move(target, 0.004f);
        }
    };

    private Tile[] tiles;
    private Map map;
    private Player player;
    private Rival rival;
    private Tilemap tilemap;

    private void Start()
    {
        tiles = Resources.LoadAll<Tile>("Tiles");
        Debug.Log(tiles.Length);
        map = Map.GenerateMap(10, 18);
        player = FindObjectOfType<Player>();
        //rival = FindObjectOfType<Rival>();
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();

        var emptyCells = map.EmptyCells.Where(i => i.X > 0 && i.Y < 9);
        var playerPos = emptyCells.GetRandom();
        player.transform.position = new Vector3(playerPos.X - 9, 4 - playerPos.Y);

        //var rivalPos = emptyCells.Except(new (int, int)[] { playerPos })
        //    .Where(i => Mathf.Abs(playerPos.X - i.Item1) + Mathf.Abs(playerPos.Y - i.Item2) >= 6)
        //    .GetRandom();

        //rival.transform.position = new Vector3(rivalPos.Item1 - 9, 4 - rivalPos.Item2);
        //var moveFunc = moveRival.GetRandom();
        //rival.MoveRival = target => moveFunc(rival, target);
        DrawMap();
    }

    private void DrawMap()
    {
        foreach (var i in map.Blocks)
            foreach (var (X, Y) in i)
            {
                //var tile = ;
                tilemap.SetTile(new Vector3Int(X - 9, 4 - Y, 0), tiles.GetRandom());
            }

        var collider = tilemap.GetComponent<TilemapCollider2D>();
        collider.gameObject.SetActive(false);
        collider.gameObject.SetActive(true);
    }
}
