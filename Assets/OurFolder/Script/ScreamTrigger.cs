using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ScreamTrigger : NetworkBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Human"))
        {
            Debug.Log("Human was within the scream trigger");
            // Do something here
        }
    }
}
