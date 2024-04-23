using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ExitDoor : NetworkBehaviour
{
    public Transform[] spawnPoints; // Array to hold spawn points
    public GameObject  KeyPrefab; // Object to spawn
    public GameObject HumanWinUI;


    private bool isTimeFrozen = false;

    void Start()
    {
        // Check if spawn points are set and object to spawn is assigned
        if (spawnPoints.Length > 0 && KeyPrefab != null)
        {
            // Choose a random spawn point index
            int randomIndex = Random.Range(0, spawnPoints.Length);
            // Spawn the object at the chosen spawn point
            Instantiate(KeyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
            Debug.Log("Key spawned at " + spawnPoints[randomIndex].position);
        }
        else
        {
            Debug.LogError("Spawn points or object to spawn not set!");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        GrabKey grabKeyScript = other.GetComponent<GrabKey>();
        Debug.Log("Object hit door" + other.tag);
        if (other.CompareTag("Human") && grabKeyScript != null && grabKeyScript.keyGrapped)
        {
            FreezeTime();
            HumanWinUI.SetActive(true);
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
