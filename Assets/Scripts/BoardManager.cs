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

    private Tilemap tilemap;    // Referenca ka tilemapi
    private CellData[,] boardData;    // Dvodimenzionalni niz koji sadrži podatke o svim poljima

    void Start()
    {
        tilemap = GetComponentInChildren<Tilemap>();    // Preuzimanje Tilemap komponente iz "djeteta" komponente
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
    }
}
