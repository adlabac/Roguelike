using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }    // Singleton instanca GameManager skripte

    private int foodAmount = 100;    // Inicijalna količina hrane

    public BoardManager boardManager;    // Referenca ka BoardManager skripti
    public PlayerController playerController;    // Referenca ka PlayerController skripti

    public TurnManager turnManager { get; private set; }    // Referenca ka TurnManeger skripti
    public UIDocument UI;

    private Label foodLabel;

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
        foodLabel = UI.rootVisualElement.Q<Label>("FoodLabel");
        foodLabel.text = "Hrana: " + foodAmount;

        turnManager = new TurnManager();    // Kreiraj TurnManager instance
        turnManager.OnTick += OnTurn;    // Registracija na OnTick događaj TurnManager klase

        boardManager.Init();    // Inicijalizuj polja za igru
        playerController.Spawn(boardManager, new Vector2Int(1, 1));   // Pozicioniraj lika u sredinu ćelije (1, 1)
    }

    public void ChangeFood(int amount)    // Metoda koja je zadužena za promjenu količine hrane
    {
        foodAmount += amount;    // Ažuriraj količinu hrane
    }

    void OnTurn()    // Metoda koju obrađuje OnTick događaj TurnManager metode
    {
        ChangeFood(-1);    // Umanji količinu hrane za 1
    }
}
