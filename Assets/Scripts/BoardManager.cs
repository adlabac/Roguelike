using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    public class CellData    // Klasa koja sadrži podatke o jednom polju
    {
        public bool passable;
    }

    public int width;    // Broj polja po širini
    public int height;    // Broj polja po visini
    public Tile[] groundTiles;    // Sprajtovi pozadine
    public Tile[] wallTiles;    // Sprajtovi zidova
    public PlayerController Player;    // Referenca ka PlayerController skripti

    private Tilemap tilemap;    // Referenca ka tilemapi
    private Grid grid;    // Referenca ka gridu tilemape
    private CellData[,] boardData;    // Dvodimenzionalni niz koji sadrži podatke o svim poljima

    public Vector3 CellToWorld(Vector2Int cellIndex)    // Metoda koja vraća poziciju sredine polja na osnovu broja njegove vrste i kolone
    {
        return grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }

    void Start()
    {
        tilemap = GetComponentInChildren<Tilemap>();    // Preuzimanje Tilemap komponente iz "djeteta" komponente
        grid = GetComponentInChildren<Grid>();    // Preuzimanje Grid komponente iz "djeteta" komponente

        boardData = new CellData[width, height];    // Kreiranje dvodimenziobnanog niza

        for (int y = 0; y < height; y++)    // Prolazak kroz sve vrste
        {
            for(int x = 0; x < width; x++)    // Prolazak kroz sve kolona
            {
                Tile tile;    // Promjenljiva sprajta trenutnog polja
                boardData[x, y] = new CellData();    // Kreiraanje klase podataka za polje

                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)    // Da li je pozicija uz samu ivicu
                {
                    tile = wallTiles[Random.Range(0, wallTiles.Length)];    // Izabheri slučajni sprajt pozadine
                    boardData[x, y].passable = false;    // Označi polje kao neprolazno
                }
                else
                {
                    tile = groundTiles[Random.Range(0, groundTiles.Length)];    // Izabheri slučajni sprajt zida
                    boardData[x, y].passable = true;    // Označi polje kao neprolazno
                }

                tilemap.SetTile(new Vector3Int(x, y, 0), tile);    // Dodijeli sprajt odgovarajućem polju na koordinati (x, y)
            }
        }

        Player.Spawn(this, new Vector2Int(1, 1));    // Pozicioniraj lika u sredinu ćelije (1, 1)
    }
}
