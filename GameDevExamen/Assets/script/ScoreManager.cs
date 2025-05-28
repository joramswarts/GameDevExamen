using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreManager : MonoBehaviour
{
    public static scoreManager instance;

    public TextMeshProUGUI TxtScore;
    private int score = 0;

    public int distanceMultiplier = 1;

    private Transform player;

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

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        score = Mathf.FloorToInt(player.position.z * distanceMultiplier);
        TxtScore.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        TxtScore.text = "0";
    }
}
