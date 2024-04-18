using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkSpawner : NetworkBehaviour
{
     public Transform spawnPrefab;
    // Start is called before the first frame update
 

    [ServerRpc]
    public void ServerSpawnObjectServerRPC()
    {
        Instantiate(spawnPrefab);
    }
}
