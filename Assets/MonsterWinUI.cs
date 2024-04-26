using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MonsterWinUI : NetworkBehaviour
{
    public GameObject HumanCaughtUI;
    

    // Method to update the UI based on the human caught state

 
    public void UpdateUI()
    {
        bool allHumansCaught = true;

        Debug.Log(GameObject.FindGameObjectsWithTag("Human").Length);
        // Check each GameObject tagged as "Human"
        foreach (var playerGameObject in GameObject.FindGameObjectsWithTag("Human"))
        {
            // Access the HumanAbility component
            HumanAbility humanAbility = playerGameObject.GetComponent<HumanAbility>();
          
            // Check if the human is caught
            if (!humanAbility.wasCaught)
            {
                // If any human is not caught, set the flag to false and break the loop
                allHumansCaught = false;
                break;
            }
        }

        // If all humans are caught, activate the HumanCaughtUI
        if (allHumansCaught)
        {
            HumanCaughtUI.SetActive(true);
        }
    }

    // Server method to trigger UI update on all clients
    [ServerRpc(RequireOwnership = false)]
    public void UpdateUIOnClientsServerRpc()
    {
        RpcUpdateUIOnClientsClientRpc(); // Corrected method name
    }

    [ClientRpc]
    void RpcUpdateUIOnClientsClientRpc() // Corrected method name
    {
        UpdateUI();
    }
}
