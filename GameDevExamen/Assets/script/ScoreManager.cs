using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreManager : MonoBehaviour
{
    // Singleton instance zodat we overal makkelijk bij kunnen
    public static scoreManager instance;

    // UI tekst om score te laten zien
    public TextMeshProUGUI TxtScore;

    // Interne score variabele
    private int score = 0;

    // Vermenigvuldiger voor afstand om score te berekenen (bijv. 1 punt per meter)
    public int distanceMultiplier = 1;

    // Referentie naar de speler om z-positie te kunnen meten
    private Transform player;

    private void Awake()
    {
        // Singleton pattern: als er nog geen instance is, maak deze, anders vernietig deze gameobject
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Zoek speler object via tag, zodat we de afstand kunnen bijhouden
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Elke frame score updaten op basis van speler z-positie
        UpdateScore();
    }

    private void UpdateScore()
    {
        // Score is floor van speler z-positie keer multiplier
        score = Mathf.FloorToInt(player.position.z * distanceMultiplier);

        // Score tekst updaten in UI
        TxtScore.text = score.ToString();
    }

    public void ResetScore()
    {
        // Score resetten naar 0 en UI ook updaten
        score = 0;
        TxtScore.text = "0";
    }
}
