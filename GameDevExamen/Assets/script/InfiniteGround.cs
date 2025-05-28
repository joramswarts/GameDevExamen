using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteGround : MonoBehaviour
{
    // Lengte van het grondobject (hoe lang één stuk grond is)
    public float groundLength = 1000f;

    private void OnTriggerEnter(Collider other)
    {
        // Checkt of de speler het collidergebied raakt
        if (other.CompareTag("Player"))
        {
            // Verplaatst het hele grondobject (parent) verder naar voren
            // Hierdoor lijkt het alsof de grond oneindig doorgaat
            transform.parent.position += new Vector3(0, 0, groundLength * 2);
        }
    }
}
