using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleObject : CellObject
{
    public Tile[] obstacleTiles;

    public override void Init(Vector2Int cell)
    {
        base.Init(cell);
        GameManager.Instance.boardManager.SetCellTile(cell, obstacleTiles[Random.Range(0, obstacleTiles.Length)]);
    }

    public override bool PlayerWantsToEnter()    // Metoda koja određuje da li je moguće stati u ćeliju sa preprekom
    {
        return false;    // Po defaultu vraati da nije moguće
    }
}
