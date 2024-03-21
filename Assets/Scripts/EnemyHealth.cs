using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{

    private int health = 3; // Initial health of the enemy

    public static int totalEnemiesDied = 0;

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
            
        }

    }

    private void Die()
    {
        totalEnemiesDied++; // Increment the total enemies died count
        Debug.Log("Enemy died. Total enemies died: " + totalEnemiesDied);
        Destroy(gameObject); // Destroy the enemy GameObject
    }

    // Method to get the total number of enemies that have died
    public static int GetTotalEnemiesDied()
    {
        return totalEnemiesDied;
    }
}
