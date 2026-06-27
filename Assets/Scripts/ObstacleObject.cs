using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleObject : CellObject    // Klasa zasnovana na CellObject klasi
{
    public int maxHealth = 3;    // Maksimalno "zdravlje" prepreke
    public Tile[] obstacleTiles;

    private int health;    // Trenutno zdravlje prepreke
    private Tile originalTile;    // Originalni sprajt pozadine, prije postavljanja prepreke

    public override void Init(Vector2Int cell)
    {
        base.Init(cell);

        health = maxHealth;    // Inicijalizuj zdravlje
        originalTile = GameManager.Instance.boardManager.GetCellTile(cell);    // Zapamti originalni sprajt pozadine
        GameManager.Instance.boardManager.SetCellTile(cell, obstacleTiles[Random.Range(0, obstacleTiles.Length)]);
    }

    public override bool PlayerWantsToEnter()    // Metoda koja određuje da li je moguće stati u ćeliju sa preprekom
    {
        health--;    // Umanji zdravlje za 1

        if (health > 0)    // Da li je trenutno zdravlje veće od nule
        {
            return false;    // Jeste - vrati da nije moguće ući na polje
        }

        GameManager.Instance.boardManager.SetCellTile(cell, originalTile);    // Postavi originalni sprajt ćelije
        Destroy(gameObject);    // Uništi samog sebe
        return true;    // Vrati da je doyvoljeno ući na polje
    }
}
