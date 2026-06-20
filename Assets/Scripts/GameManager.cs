using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }    // Singleton instanca GameManager skripte

    public BoardManager boardManager;    // Referenca ka BoardManager skripti
    public PlayerController playerController;    // Referenca ka PlayerController skripti

    public TurnManager turnManager { get; private set; }    // Referenca ka TurnManeger skripti

    private void Awake()    // Singleton pattern GameManager skripte
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        turnManager = new TurnManager();    // Kreiraj TurnManager instance
        boardManager.Init();    // Inicijalizuj polja za igru
        playerController.Spawn(boardManager, new Vector2Int(1, 1));   // Pozicioniraj lika u sredinu ćelije (1, 1)
    }
}
