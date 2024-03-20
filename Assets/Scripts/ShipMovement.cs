using UnityEngine;
using Unity.Netcode;

public class ShipMovement : NetworkBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float speed = 5f; // Speed of movement

    // Update is called once per frame
    void FixedUpdate()
    {
        // We're only updating the ship's movements when we're surely updating on the owning instance
        if (!IsOwner)
            return;

        KeyboardInput();
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


}
