using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ExitDoor : NetworkBehaviour
{
    public Transform[] spawnPoints; // Array to hold spawn points
    public GameObject  KeyPrefab; // Object to spawn

    void Start()
    {
        // Check if spawn points are set and object to spawn is assigned
        if (spawnPoints.Length > 0 && KeyPrefab != null)
        {
            // Choose a random spawn point index
            int randomIndex = Random.Range(0, spawnPoints.Length);
            // Spawn the object at the chosen spawn point
            Instantiate(KeyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Spawn points or object to spawn not set!");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            Debug.Log("Key opened the door");
        }
    }
}
