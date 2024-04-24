using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MonsterWinUI : NetworkBehaviour
{
    public GameObject HumanCaughtUI;

    // Method to update the UI based on the human caught state
    public void UpdateUI(bool humanCaught)
    {
        HumanCaughtUI.SetActive(humanCaught);
    }

    // Server method to trigger UI update on all clients
    [ServerRpc(RequireOwnership = false)]
    public void UpdateUIOnClientsServerRpc(bool humanCaught)
    {
        RpcUpdateUIOnClientsClientRpc(humanCaught); // Corrected method name
    }

    [ClientRpc]
    void RpcUpdateUIOnClientsClientRpc(bool humanCaught) // Corrected method name
    {
        UpdateUI(humanCaught);
    }
}
