using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvisibilityObject : NetworkBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        // Check if the object exiting the trigger is tagged as "Key"
        if (other.transform.GetComponent<PlayerManager>() != null)
        {
            if(other.transform.GetComponent<PlayerManager>().isHuman)
            {
               other.transform.GetComponent<HumanAbility>().BecomeInvisible();
            }

        }
    }


}
