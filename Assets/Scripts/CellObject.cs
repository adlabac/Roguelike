using System.Numerics;
using UnityEngine;

public class CellObject : MonoBehaviour
{
    protected Vector2Int cell;

    public virtual void Init(Vector2Int cellIndex)    // Inicijalizacija objekta
    {
        cell = cellIndex;    // Sačuvaj poziciju ćelije
    }

    public virtual bool PlayerWantsToEnter()    // Metoda koja određuje da li je moguće stati u ćeliju sa objektom
    {
        return true;    // Po defaultu vrati da je dozvoljeno stati
    }

    public virtual void PlayerEntered()    // Poziva se kad igrač stane da ćeliju
    {
    }
}
