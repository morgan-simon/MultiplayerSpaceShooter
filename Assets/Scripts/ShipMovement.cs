using UnityEngine;
using Unity.Netcode;

public class ShipMovement : NetworkBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float speed = 5f; // Speed of movement
    public float bulletSpeed = 10f; // Speed of the bullets

    // Update is called once per frame
    void Update()
    {
        // We're only updating the ship's movements when we're surely updating on the owning instance
        if (!IsOwner)
            return;

        HandleKeyboardInput();
    }

    private void HandleKeyboardInput()
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

        // Check for space bar input to shoot bullets
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Call the method to shoot bullets
            ShootBulletServerRpc(transform.position, transform.rotation);
        }
    }

    [ServerRpc]
    void ShootBulletServerRpc(Vector3 position, Quaternion rotation)
    {
        // Create a bullet instance on the server
        GameObject bulletInstance = Instantiate(bulletPrefab, position, rotation);

        // Spawn the bullet over the network
        NetworkObject bulletNetworkObject = bulletInstance.GetComponent<NetworkObject>();
        bulletNetworkObject.Spawn();

        // Get the rigidbody component of the bullet
        Rigidbody2D bulletRigidbody = bulletInstance.GetComponent<Rigidbody2D>();

        // Ensure the bullet has a rigidbody component
        if (bulletRigidbody == null)
        {
            Debug.LogError("Bullet prefab does not have a Rigidbody2D component!");
            return;
        }

        // Set the velocity of the bullet to move in the forward direction at the specified speed
        bulletRigidbody.velocity = rotation * Vector2.right * bulletSpeed;

        // Set the bullet's parent to null to prevent it from being affected by the player's movement
        bulletInstance.transform.parent = null;

        // Destroy the bullet after a certain amount of time (adjust as needed)
        Destroy(bulletInstance, 5f);
    }
}
