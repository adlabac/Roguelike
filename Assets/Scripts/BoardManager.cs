using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public class CellData    // Klasa koja sadrži podatke o jednom polju
    {
        public bool passable;    // Da li na polje može stati igrač
        public CellObject containedObject;    // Objekat koji se nalazi na polju
    }

    public int width;    // Broj polja po širini
    public int height;    // Broj polja po visini
    public Tile[] groundTiles;    // Sprajtovi pozadine
    public Tile[] wallTiles;    // Sprajtovi zidova
    public FoodObject[] foodPrefabs;    // Prefabi hrane
    public ObstacleObject obstaclePrefab;    // Prefab prepreke

    private Tilemap tilemap;    // Referenca ka tilemapi
    private Grid grid;    // Referenca ka gridu tilemape
    private CellData[,] boardData;    // Dvodimenzionalni niz koji sadrži podatke o svim poljima
    private List<Vector2Int> emptyCells;    // Lista praznih ćelija

    public Vector3 CellToWorld(Vector2Int cellIndex)    // Metoda koja vraća poziciju sredine polja na osnovu broja njegove vrste i kolone
    {
        return grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }

    public CellData GetCellData(Vector2Int cellIndex)    // Metoda koja vraća podatke za traženo polje, ukoliko su koordinate unutar zatadih granica
    {
        if(cellIndex.x < 0 || cellIndex.x >= width || cellIndex.y < 0 || cellIndex.y >= height)    // Da li su koordinate unutar zadatih granica
        {
            return null;    // Ako su zadate koordinate van table, vrati null
        }

        return boardData[cellIndex.x, cellIndex.y];    // Vrati podatke za traženu ćeliju
    }

    public Tile GetCellTile(Vector2Int cellIndex)    // Metoda koja vraća sprajt traženog polja
    {
        return tilemap.GetTile<Tile>(new Vector3Int(cellIndex.x, cellIndex.y, 0));    // Vrati sprajt polja na datoj koordinati
    }

    public void SetCellTile(Vector2Int cellIndex, Tile tile)    // Metoda koja postavlja/mijenja sprajt za dato polje
    {
        tilemap.SetTile(new Vector3Int(cellIndex.x, cellIndex.y, 0), tile);    // Dodijeli sprajt polju na datoj koordinati
    }

    void AddObject(CellObject obj, Vector2Int coord)    // Metoda za dodavanje novog objekta
    {
        CellData data = GetCellData(coord);    // Preuzmi podatke o ćeliji
        obj.transform.position = CellToWorld(coord);    // Pozicioniraj objekat u sredinu ćelije
        data.containedObject = obj;    // Dodaj objekat podacima za datu ćeliju
        obj.Init(coord);    // Inicijalizuj klasu
    }

    void GenerateFood()    // Generisanje i raspoređivanje hrane
    {
        int foodCount = 5;    // Količina hrane koju treba rasporediti
        for (int i = 0; i < foodCount; i++)    // Ponoviti generisanje željeni broj puta
        {
            int randomIndex = Random.Range(0, emptyCells.Count);    // Izaberi slučajno neku od slobodnih ćelija
            Vector2Int coord = emptyCells[randomIndex];    // Uzmi koordinatu iz izabrane ćelije

            emptyCells.RemoveAt(randomIndex);    // Ukloni izabranu ćeliju iz liste slobodnih
            FoodObject newFood = Instantiate(foodPrefabs[Random.Range(0, foodPrefabs.Length)]);    // Kreiraj novi objekat na osnovu slučajno izabranog prefaba

            AddObject(newFood, coord);    // Dodaj objekat
        }
    }

    void GenerateObstacles()    // Generisanje i raspoređivanje prepreka
    {
        int obsracleCount = Random.Range(6, 10);    // Broj prepreka koje treba rasporediti
        for (int i = 0; i < obsracleCount; i++)    // Ponoviti generisanje željeni broj puta
        {
            int randomIndex = Random.Range(0, emptyCells.Count);    // Izaberi slučajno neku od slobodnih ćelija
            Vector2Int coord = emptyCells[randomIndex];    // Uzmi koordinatu iz izabrane ćelije

            emptyCells.RemoveAt(randomIndex);    // Ukloni izabranu ćeliju iz liste slobodnih
            ObstacleObject newObstacle = Instantiate(obstaclePrefab);    // Kreiraj novu prepreku na osnovu prefaba
            AddObject(newObstacle, coord);    // Dodaj objekat
        }
    }

    public void Init()
    {
        tilemap = GetComponentInChildren<Tilemap>();    // Preuzimanje Tilemap komponente iz "djeteta" komponente
        grid = GetComponentInChildren<Grid>();    // Preuzimanje Grid komponente iz "djeteta" komponente
        emptyCells = new List<Vector2Int>();    // Kreiraj praznu listu pozicija praznih ćelija

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
                    emptyCells.Add(new Vector2Int(x, y));
                }
                SetCellTile(new Vector2Int(x, y), tile);    // Dodijeli sprajt odgovarajućem polju na koordinati (x, y)
            }
        }

        emptyCells.Remove(new Vector2Int(1, 1));
        GenerateObstacles();
        GenerateFood();
    }
}
