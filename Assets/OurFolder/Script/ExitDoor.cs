using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ExitDoor : NetworkBehaviour
{
    public GameObject HumanWinUI;

    private bool isTimeFrozen = false;
    private bool isHumanWinUIActive = false;

    void Start()
    {

    }

    [ServerRpc(RequireOwnership = false)]
    void SetWinUIActiveServerRpc(bool isActive)
    {
        // Set the UI active state on the server
        isHumanWinUIActive = isActive;
        RpcSetWinUIActiveClientRpc(isActive);
    }

    [ClientRpc]
    void RpcSetWinUIActiveClientRpc(bool isActive)
    {
        // Set the UI active state on all clients
        HumanWinUI.SetActive(isActive);
    }

    public void OnTriggerEnter(Collider other)
    {
      

        Debug.Log("Entered the trigger" + other.tag + other.GetComponent<HumanAbility>().humanHasKey);
        bool hasKey = other.GetComponent<HumanAbility>().humanHasKey;

        // Checking if the object is a Human, if they have the key, and if the key is grabbed
        if (other.CompareTag("Human") && hasKey)
        {
            FreezeTime();
            isHumanWinUIActive = true;
            SetWinUIActiveServerRpc(true);
            Debug.Log("Key opened the door");
        }
    }

    void FreezeTime()
    {
        if (!isTimeFrozen)
        {
            Time.timeScale = 0f; // Freeze time
            isTimeFrozen = true;
        }
    }
}
