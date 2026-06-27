using UnityEngine;

public class FoodObject : CellObject    // Klasa zasnovana na CellObject klasi
{
    public int foodAmount = 10;    // Default količina hrane koju donosi objekat

    public override void PlayerEntered()    // Metoda koja se izvršava kada korinik uđe u polje
    {
        Destroy(gameObject);    // Uništi samog sebe
        GameManager.Instance.ChangeFood(foodAmount);    // Uvećaj količinu hrane
    }
}
