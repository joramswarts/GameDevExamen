using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class coinManager : MonoBehaviour
{
    // Singleton instance zodat we makkelijk overal bij kunnen
    public static coinManager instance;

    // UI tekst om het aantal verzamelde coins te laten zien
    public TextMeshProUGUI TxtCoin;

    // Houdt het totaal aantal verzamelde coins bij
    private int totalCoins = 0;

    private void Awake()
    {
        // Singleton pattern: zorg dat er maar 1 coinManager is
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Voeg coins toe en update de UI tekst
    public void AddCoin(int amount)
    {
        totalCoins += amount;
        TxtCoin.text = totalCoins.ToString();
    }

    // Reset het aantal coins (bijv. bij opnieuw starten)
    public void ResetCoins()
    {
        totalCoins = 0;
        TxtCoin.text = "0";
    }
}
