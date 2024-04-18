using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkSpawner : NetworkBehaviour
{
    [SerializeField] private Transform spawnPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void SpawnObject()
    {
        Instantiate(spawnPrefab);
    }
}
