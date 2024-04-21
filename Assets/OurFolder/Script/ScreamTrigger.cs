using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ScreamTrigger : NetworkBehaviour
{
    bool hasScreamed;

    private void Start()
    {
        hasScreamed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();

            if (playerManager.isHuman)
            {
                Debug.Log("Human was within the scream trigger");
                // Do something here
                if (other.GetComponent<AffectedByScream>() != null)
                {
                    if (!hasScreamed)
                    {
                        other.GetComponent<AffectedByScream>().ScreamEffect();
                        hasScreamed = true;
                        StartCoroutine(ScreamCoolDown());

                        // Call the server RPC to trigger scream effect on other clients
                        if (IsClient)
                        {
                            // Call the server RPC function directly
                            ScreamOnClientsServerRPC();
                        }
                    }
                }
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void ScreamOnClientsServerRPC()
    {
        // This function will be executed on all clients by the server
        RpcScreamEffectClientRPC();//Call RPC to trigger scream effect on all clients
    }

    [ClientRpc]
    void RpcScreamEffectClientRPC()
    {
     
        // Trigger scream effect here on all clients
        
        foreach (var playerGameObject in GameObject.FindGameObjectsWithTag("Human"))
        {
            var affectedByScream = playerGameObject.GetComponent<AffectedByScream>();
            if (affectedByScream != null)
            {
                affectedByScream.ScreamEffect();
            }
        }
    }

    IEnumerator ScreamCoolDown()
    {
        yield return new WaitForSeconds(5f);
        hasScreamed = false;
    }
}
