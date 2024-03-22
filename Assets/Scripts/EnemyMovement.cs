using UnityEngine;
using Unity.Netcode;

public class EnemyMovement : NetworkBehaviour
{
    [SerializeField] public float enemySpeed = 1f;

    void Update()
    {
        // Move the enemy left
        transform.Translate(Vector3.left * enemySpeed * Time.deltaTime);

        // Check if the enemy has moved out of bounds
        if (transform.position.x < -10f) 
        {
            // Destroy the enemy
            Destroy(gameObject);
        }
    }
}

