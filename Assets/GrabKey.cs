using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class GrabKey : NetworkBehaviour
{
    public GameObject keyOnHuman;

    private void Start()
    {
        keyOnHuman.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Human"))
        {
            
       }
   }
}
