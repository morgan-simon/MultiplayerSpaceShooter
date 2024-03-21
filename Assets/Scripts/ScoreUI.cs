using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;



public class ScoreUI : NetworkBehaviour {
    public TextMeshProUGUI countText;

    void Update()
    {
        if(IsServer)
        {
                // Optionally, you can update the score UI continuously in case it changes
                UpdateScoreServerRpc();
        }


    }

// Update the score UI on the server and synchronize to clients
    [ServerRpc]
    void UpdateScoreServerRpc()
    {
        // Access the totalEnemiesDied variable from EnemyHealth script directly
        int totalEnemiesDied = EnemyHealth.totalEnemiesDied;
        // Update the score UI text on the server
        countText.text = totalEnemiesDied.ToString();
        // Synchronize the score UI update to all clients
        UpdateScoreClientRpc(totalEnemiesDied);
    }

    // Client-side method to update the score UI
    [ClientRpc]
    void UpdateScoreClientRpc(int score)
    {
    
            // Update the score UI text on all clients
            countText.text = score.ToString();

    }
}
