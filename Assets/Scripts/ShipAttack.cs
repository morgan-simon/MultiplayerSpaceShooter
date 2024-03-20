using UnityEngine;
using Unity.Netcode;

public class ShipAttack : NetworkBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // Reference to the bullet prefab
    [SerializeField] private float speed = 10f;

    void Update()
    {
        // Check for space bar input
        if (IsOwner && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space bar pressed");
            // Call a method to shoot bullets when the space key is pressed
            ShootBullet();
        }
    }

  void ShootBullet()
{
    // Create a bullet instance
    GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);

    // Spawn the bullet over the network
    NetworkObject bulletNetworkObject = bulletInstance.GetComponent<NetworkObject>();
    bulletNetworkObject.Spawn();

    // Get the rigidbody component of the bullet
    Rigidbody2D bulletRigidbody = bulletInstance.GetComponent<Rigidbody2D>();

    // Set the velocity of the bullet to move in the forward direction at the specified speed
    bulletRigidbody.velocity = transform.right * speed;

    // Set the bullet's parent to null to prevent it from being affected by the player's movement
    bulletInstance.transform.parent = null;

    // Destroy the bullet after a certain amount of time (adjust as needed)
    Destroy(bulletInstance, 5f);
}


}
