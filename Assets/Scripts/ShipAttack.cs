using UnityEngine;
using Unity.Netcode;

public class ShipAttack : NetworkBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // Reference to the bullet prefab
    // [SerializeField] private float bulletSpeed = 10f;

    public int bulletDamage = 1;

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
        // Create bullet
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Spawn bullet
        NetworkObject bulletNetworkObject = bullet.GetComponent<NetworkObject>();
        bulletNetworkObject.Spawn();

        // Set the bullet's parent to null to prevent it from being affected by the player's movement
        // bullet.transform.parent = null;

        // Destroy the bullet after a certain amount of time 
        // TODO: Destroy bullet when it collides with an asteroid
        // Destroy(bullet, 5f);
    }
}