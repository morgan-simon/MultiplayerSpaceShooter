using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float bulletSpeed = 10f; // Speed of the bullet
    [SerializeField] private float lifetime = 5f; // Lifetime of the bullet
    public int bulletDamage = 1;

    void Start()
    {
        // Get the rigidbody component of the bullet
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Set the velocity of the bullet to move in the forward direction 
        rb.velocity = transform.right * bulletSpeed;

        // Start a coroutine to destroy the bullet after the specified lifetime
        StartCoroutine(DestroyAfterLifetime());
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        if(IsServer){
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(bulletDamage);
                Destroy(gameObject); // Destroy the bullet upon impact
            }
        }
        
    }

    IEnumerator DestroyAfterLifetime()
    {
        // Wait for the specified lifetime
        yield return new WaitForSeconds(lifetime);

        // Destroy the bullet after the lifetime expires
        Destroy(gameObject);
    }
}
