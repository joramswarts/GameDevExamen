using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class coinManager : MonoBehaviour
{
    public static coinManager instance;

    public TextMeshProUGUI TxtCoin;
    private int totalCoins = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin(int amount)
    {
        totalCoins += amount;
        TxtCoin.text = totalCoins.ToString();
    }

    public void ResetCoins()
    {
        totalCoins = 0;
        TxtCoin.text = "0";
    }
}
