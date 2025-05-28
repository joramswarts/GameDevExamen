using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    // Lijst van obstakel en coin prefabs die we kunnen spawnen
    public GameObject[] obstaclePrefabs;
    public GameObject[] coinPrefabs;

    // Verwijzing naar de speler (zodat we weten waar te spawnen)
    public Transform player;

    // De huidige positie waar het volgende object gespawned wordt
    public Vector3 spawnPosition;

    // Kans dat er een coin gespawned wordt in plaats van een obstakel
    public float coinChance = 0.3f;

    // Afstand tussen elk object dat gespawned wordt
    public float distanceBetweenObstacle = 15f;

    // Hoe ver vooruit we alvast obstakels/coinen gaan spawnen
    public float horizonDistance = 200f;

    // Beginpositie om vanuit te resetten
    private Vector3 startSpawnPosition;

    private void Start()
    {
        // Als de speler nog niet is ingesteld, probeer hem dan te vinden met de tag
        if(player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if(playerObj != null)
                player = playerObj.transform;
            else
                Debug.LogError("Player object met tag 'Player' niet gevonden!");
        }

        // Zet de eerste spawnpositie iets voor de speler
        if(player != null)
        {
            startSpawnPosition = new Vector3(0f, 1.5f, player.position.z + 10f);
            spawnPosition = startSpawnPosition;
        }
    }

    private void Update()
    {
        // Als er geen speler is, doen we niks
        if(player == null)
            return;

        // Bereken afstand tussen de speler en waar we gaan spawnen
        float distance = Vector3.Distance(player.position, spawnPosition);

        // Alleen spawnen als de speler dichtbij genoeg komt
        if(distance < horizonDistance)
        {
            // Kies een willekeurige X-positie binnen -3 en 3 (bijv. rijbanen)
            int x = Random.Range(-3, 4);
            spawnPosition = new Vector3(x, 1.5f, spawnPosition.z + distanceBetweenObstacle);

            // Kansberekening of er een coin of obstakel komt
            if(Random.value < coinChance)
            {
                // Coin komt wat lager te liggen
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

    // Reset de spawnpositie naar het begin, bijv. bij Game Restart
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
