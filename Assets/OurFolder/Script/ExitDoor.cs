using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ExitDoor : NetworkBehaviour
{
 
    public GameObject HumanWinUI;


    private bool isTimeFrozen = false;

    void Start()
    {
       
    }

    public void OnTriggerEnter(Collider other)
    {
        GrabKey grabKeyScript = other.GetComponent<GrabKey>();
        Debug.Log("Object hit door" + other.tag);
        Debug.Log("Key grabbed state: " + grabKeyScript.keyGrapped);
        if (other.CompareTag("Human") && grabKeyScript != null && grabKeyScript.keyGrapped == true)
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
