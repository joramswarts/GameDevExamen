using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Kracht om vooruit te bewegen
    public float forwardForce = 50f;
    // Kracht om zijwaarts te bewegen
    public float lateralForce = 15f;
    // Doelsnelheid voor de speler vooruit
    public float targetSpeed = 100f;
    // Maximale afstand die de speler naar links/rechts mag bewegen
    public float maxLateralPosition = 3f;

    private Rigidbody rb;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Awake()
    {
        // Pak de Rigidbody van de speler om mee te bewegen
        rb = GetComponent<Rigidbody>();

        // Sla de startpositie en rotatie van de speler op (voor reset)
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        // Bewegingen worden hier afgehandeld met physics
        ForwardMovement();
        LateralMovement();
    }

    void ForwardMovement()
    {
        float currentSpeed = rb.velocity.z;

        // Als de speler langzamer gaat dan targetSpeed, meer kracht geven
        if (currentSpeed < targetSpeed)
        {
            rb.AddForce(Vector3.forward * forwardForce * Time.fixedDeltaTime, ForceMode.Force);
        }
        // Zorg dat de snelheid niet hoger wordt dan de targetSpeed
        else if (currentSpeed > targetSpeed)
        {
            Vector3 clampedVelocity = rb.velocity;
            clampedVelocity.z = targetSpeed;
            rb.velocity = clampedVelocity;
        }
    }

    void LateralMovement()
    {
        // Input van toetsenbord/joystick links-rechts (-1 tot 1)
        float direction = Input.GetAxis("Horizontal");

        // Pas zijwaartse snelheid aan op basis van input
        Vector3 lateralVelocity = rb.velocity;
        lateralVelocity.x = direction * lateralForce;
        rb.velocity = lateralVelocity;

        // Houd speler binnen de max x-positie (links/rechts grens)
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -maxLateralPosition, maxLateralPosition);
        transform.position = clampedPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Als speler een obstakel raakt, speel geluid en ga naar GameOver scherm
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.destroyClip);
            GameStateManager.instance.changeToGameOver();
        }
    }

    public void ResetPlayer()
    {
        // Zet speler terug naar beginpositie en stop bewegingen
        transform.position = new Vector3(0f, 0.5f, 0f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
