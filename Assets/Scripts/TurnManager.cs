using UnityEngine;

public class TurnManager
{
    private int turnCount;

    public TurnManager()    // Constructor klase
    {
        turnCount = 0;    // Inicijalizuj broj poteza
    }

    public void Tick()    // Metoda koja se poziva za svaki potez
    {
        turnCount++;    // Uvećaj broj poteza
        Debug.Log("Broj poteza: " + turnCount);    // Prikaži broj poteza u konzoli
    }

}
