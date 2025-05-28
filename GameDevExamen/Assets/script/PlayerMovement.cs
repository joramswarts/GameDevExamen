using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardForce = 50f;
    public float lateralForce = 15f;
    public float targetSpeed = 100f;
    public float maxLateralPosition = 3f;

    private Rigidbody rb;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Sla startpositie en rotatie op
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        ForwardMovement();
        LateralMovement();
    }

    void ForwardMovement()
    {
        float currentSpeed = rb.velocity.z;

        if (currentSpeed < targetSpeed)
        {
            rb.AddForce(Vector3.forward * forwardForce * Time.fixedDeltaTime, ForceMode.Force);
        }
        else if (currentSpeed > targetSpeed)
        {
            Vector3 clampedVelocity = rb.velocity;
            clampedVelocity.z = targetSpeed;
            rb.velocity = clampedVelocity;
        }
    }

    void LateralMovement()
    {
        float direction = Input.GetAxis("Horizontal");

        Vector3 lateralVelocity = rb.velocity;
        lateralVelocity.x = direction * lateralForce;
        rb.velocity = lateralVelocity;

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -maxLateralPosition, maxLateralPosition);
        transform.position = clampedPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.destroyClip);
            GameStateManager.instance.changeToGameOver();
        }
    }

    public void ResetPlayer()
    {
        transform.position = new Vector3(0f, 0.5f, 0f);  // Zet terug op juiste startpositie
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
