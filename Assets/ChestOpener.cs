using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ChestOpener : NetworkBehaviour
{
    public Animator animator; // Reference to the animator component

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the collider is tagged as "Human"
        if (other.CompareTag("Human"))
        {
            // Check if the "F" key is pressed
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Call the OpenChest method on the server
                OpenChestServerRpc();
            }
        }
    }

    // Method to open the chest on the server and synchronize the animation
    [ServerRpc]
    void OpenChestServerRpc()
    {
        // Trigger the animation on the server
        OpenChestClientRpc();
    }

    // Method to open the chest on all clients and synchronize the animation
    [ClientRpc]
    void OpenChestClientRpc()
    {
        // Trigger the animation on all clients
        animator.SetTrigger("Open"); // Assumes you have a trigger parameter named "Open" in your animator controller
    }
}
