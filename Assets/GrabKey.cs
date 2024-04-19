using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class GrabKey : NetworkBehaviour
{
    public GameObject keyOnHuman;

    private InputAction eAction;

    private bool keyGrapped = false;

    private GameObject collidedKey;


    private void Start()
    {
        keyOnHuman.SetActive(false);

        // Get reference to the 'E' action
        eAction = GetComponent<PlayerInput>().actions["E"];

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is tagged as "Key"
        if (other.CompareTag("Key"))
        {
            // Check if the key has not been grabbed yet
            if (!keyGrapped)
            {
                collidedKey = other.gameObject;

                // Add a listener to the 'E' action
                eAction.performed += ctx => OnEPressed();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is tagged as "Key"
        if (other.CompareTag("Key"))
        {
            // Check if the key has been grabbed
            if (keyGrapped)
            {
                // Remove the listener from the 'E' action
                eAction.performed -= ctx => OnEPressed();
            }
        }
    }

    private void OnEPressed()
    {
        // Set keyGrapped to true to indicate that the key has been grabbed
        keyGrapped = true;
        Destroy(collidedKey);
        // Activate the key on the player
        keyOnHuman.SetActive(true);
       

    }
}
