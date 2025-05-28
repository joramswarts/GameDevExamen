using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        // Zoekt de speler in de scene op basis van de "Player" tag
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Checkt elke frame of dit object ver genoeg achter de speler zit
        // Als dat zo is, wordt het object vernietigd om geheugen te besparen
        if (transform.position.z < player.transform.position.z - 30)
        {
            Destroy(gameObject);
        }
    }
}
