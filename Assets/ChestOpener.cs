using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class ChestOpener : NetworkBehaviour
{
    public Animator animator; // Reference to the animator component
    public GameObject keyGO;

    public bool hasKey;
    public bool wasOpened;


    private void Start()
    {
        keyGO.SetActive(false);
        wasOpened = false;
    }
    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the collider is tagged as "Human"
        if (other.CompareTag("Human") && !wasOpened)
        {

            //Keyboard kb = InputSystem.GetDevice<Keyboard>();

            //if(kb.eKey.wasPressedThisFrame)
            //{
            //    OpenChestServerRpc();
            //}

            OpenChestServerRpc();

           
            if(hasKey)
            {
                other.GetComponent<HumanAbility>().OpenChest();
                other.GetComponent<GrabKey>().keyOnHuman.SetActive(true);
            }
          
            Debug.Log("Inside trigger");

      
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
        animator.Play("ChestOpen");
        if (hasKey)
        {
           
            keyGO.SetActive(true);
            StartCoroutine(WaitForAnimation());
        }
        wasOpened = true;
    }

    IEnumerator WaitForAnimation()
    {
        
        yield return new WaitForSeconds(3.0f);
        keyGO.SetActive(false);

    }
}
