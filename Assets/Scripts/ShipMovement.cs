using UnityEngine;
using Unity.Netcode;

public class ShipMovement : NetworkBehaviour
{
    public GameObject bulletPrefab; // Bullet prefab
    public float speed = 5f; // Movement speed

    void FixedUpdate()
    {
        // Only update ship movement if it is by the owner
        if (!IsOwner)
            return;

        KeyboardInput();
        ClampPosition();
    }

    private void KeyboardInput()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        // Check for individual key presses
        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1f; // Move upwards
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1f; // Move downwards
        }

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f; // Move left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f; // Move right
        }

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized * speed * Time.deltaTime;

        // Apply movement to the player's position
        transform.Translate(movement);
    }

    private void ClampPosition()
    {
        // Clamp ship's position within boundaries
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -7.3f, 7.3f);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -4.3f, 4.3f);
        transform.position = clampedPosition;
    }

}
