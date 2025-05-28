using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    // Waarde van deze munt (standaard 1)
    public int coinValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        // Check of de speler de coin raakt
        if (other.CompareTag("Player"))
        {
            // Speel munt geluid af via AudioManager
            AudioManager.instance.PlaySFX(AudioManager.instance.coinClip);

            // Voeg muntwaarde toe aan de coinManager
            coinManager.instance.AddCoin(coinValue);

            // Vernietig de coin, want hij is opgepakt
            Destroy(gameObject);
        }
    }
}
