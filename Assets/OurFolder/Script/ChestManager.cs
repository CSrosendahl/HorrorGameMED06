using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ChestManager : NetworkBehaviour
{
    public GameObject[] chests;

    void Start()
    {
        

        // Randomly select a chest and flag it as having a key
        int randomIndex = Random.Range(0, chests.Length);
        chests[randomIndex].GetComponent<ChestOpener>().hasKey = true;

      
    }

}
