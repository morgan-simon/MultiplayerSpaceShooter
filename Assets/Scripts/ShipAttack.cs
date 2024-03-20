using UnityEngine;
using Unity.Netcode;

public class ShipAttack : NetworkBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // Reference to the bullet prefab
    [SerializeField] private float bulletSpeed = 10f;


    void Update()
    {
        // Check for space bar input to shoot bullets
        if (IsOwner && Input.GetKeyDown(KeyCode.Space))
        {
            // Call the method to shoot bullets
            ShootBulletServerRpc();
        }
    }
    
    [ServerRpc]
    void ShootBulletServerRpc()
    {
        // Create a bullet instance on the server
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Spawn the bullet over the network
        NetworkObject bulletNetworkObject = bullet.GetComponent<NetworkObject>();
        bulletNetworkObject.Spawn();

        // Get the rigidbody component of the bullet
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        // Set the velocity of the bullet to move in the forward direction at the specified speed
        bulletRigidbody.velocity = transform.right * bulletSpeed;

        // Set the bullet's parent to null to prevent it from being affected by the player's movement
        bullet.transform.parent = null;

        // Destroy the bullet after a certain amount of time (adjust as needed)
        Destroy(bullet, 5f);
    }
}