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

    }
}