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
        if(other.GetComponent<PlayerManager>() != null)
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();

            if(playerManager.isHuman)
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
                    }
                }
            }

           
        }
    }
    IEnumerator ScreamCoolDown()
    {
        
        yield return new WaitForSeconds(5f);
        hasScreamed = false;
   
    }
}
