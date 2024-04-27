using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ChestManager : NetworkBehaviour
{
    public static ChestManager instance;

    private void Awake()
    {
        instance = this;
    }
    public GameObject[] chests;
    public int chestsOpened = 0;

    void Start()
    {
        

        // Randomly select a chest and flag it as having a key
        int randomIndex = Random.Range(0, chests.Length);
        chests[randomIndex].GetComponent<ChestOpener>().hasKey = true;

      
    }

}
