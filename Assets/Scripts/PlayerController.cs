using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private BoardManager board;    // Referenca ka BoardManager skripti
    private Vector2Int cellPosition;    // Vrsta i kolona polja na kom se nalazi igrač

    public void Spawn(BoardManager boardManager, Vector2Int cell)    // Metoda koja pozicionira igrača na dato polje
    {
        board = boardManager;    // Preuzimanje reference ka BoardManager skripti
        cellPosition = cell;    // Preuzimanje početne lokacija igrača

        transform.position = board.CellToWorld(cell);    // Određivanje pozicije igrača na osnovu lokacije
    }
}
