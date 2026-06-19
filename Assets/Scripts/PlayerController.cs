using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private BoardManager board;    // Referenca ka BoardManager skripti
    private Vector2Int cellPosition;    // Vrsta i kolona polja na kom se nalazi igrač

    public void MoveTo(Vector2Int cell)    // Metoda koja pomjera igrača na traženo polje
    {
        cellPosition = cell;    // Zapamti novu poziciju
        transform.position = board.CellToWorld(cell);    // Pozicioniraj igrača u sredinu date ćelije
    }

    public void Spawn(BoardManager boardManager, Vector2Int cell)    // Metoda koja pozicionira igrača na dato polje
    {
        board = boardManager;    // Preuzimanje reference ka BoardManager skripti
        MoveTo(cell);    // Pozicioniraj igrača
    }

    void Update()
    {
        Vector2Int newCell = cellPosition;    // Nova pozicija igrača
        bool hasMoved = false;    // Da li se igrač pomjerio

        if(Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            newCell.y += 1;    // Ako je kliknuta strelica nagore uvećaj y koordinatu za 1
            hasMoved = true;    // Označi da je došlo do pomjeranja
        }
        else if(Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            newCell.y -= 1;    // Ako je kliknuta strelica nadolje umanji y koordinatu za 1
            hasMoved = true;    // Označi da je došlo do pomjeranja
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            newCell.x += 1;    // Ako je kliknuta strelica nadesno uvećaj x koordinatu za 1
            hasMoved = true;    // Označi da je došlo do pomjeranja
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            newCell.x -= 1;    // Ako je kliknuta strelica nalijevo umanji x koordinatu za 1
            hasMoved = true;    // Označi da je došlo do pomjeranja
        }

        if(hasMoved)    // Da li se igrač pomjerio
        {
            BoardManager.CellData cellData = board.GetCellData(newCell);    // Preuzmi podatke za novu ćeliju

            if (cellData != null && cellData.passable)    // Da li ćelija postoji i da li se na nju može stati?
            {
                MoveTo(newCell);    // Pozicioniraj igrača u novu ćeliju
            }
        }
    }
}
