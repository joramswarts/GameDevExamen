using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] coinPrefabs;
    public Transform player;
    public Vector3 spawnPosition;

    public float coinChance = 0.3f;
    public float distanceBetweenObstacle = 15f;
    public float horizonDistance = 200f;

    private Vector3 startSpawnPosition;

    private void Start()
    {
        // Als player niet is toegewezen, zoek automatisch
        if(player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if(playerObj != null)
                player = playerObj.transform;
            else
                Debug.LogError("Player object met tag 'Player' niet gevonden!");
        }

        // Zet startSpawnPosition vlak bij speler
        if(player != null)
        {
            startSpawnPosition = new Vector3(0f, 1.5f, player.position.z + 10f);
            spawnPosition = startSpawnPosition;
        }
    }

    private void Update()
    {
        if(player == null)
            return; // geen player, geen spawn

        float distance = Vector3.Distance(player.position, spawnPosition);

        if(distance < horizonDistance)
        {
            int x = Random.Range(-3, 4);
            spawnPosition = new Vector3(x, 1.5f, spawnPosition.z + distanceBetweenObstacle);

            if(Random.value < coinChance)
            {
                spawnPosition.y = 0.6f;
                GameObject coinPrefab = coinPrefabs[Random.Range(0, coinPrefabs.Length)];
                Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
                Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    public void ResetGenerator()
    {
        if(player != null)
        {
            spawnPosition = new Vector3(0f, 1.5f, player.position.z + 10f);
        }
        else
        {
            spawnPosition = startSpawnPosition;
        }
    }
}
