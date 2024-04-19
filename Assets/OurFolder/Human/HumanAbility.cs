using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HumanAbility : NetworkBehaviour
{
    public Animator humanAnimator;
    public GameObject throwPrefab;

  
    
    public Transform firePoint;
    public float throwForce = 25f;


    // This is being called as an animation event. It is not being called by the player's input. Humanthrow is
   public void Throw()
    {
        GameObject throwObject = Instantiate(throwPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = throwObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        // Get the player's current forward direction
        Vector3 throwDirection = transform.forward;

        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }

    public void HumanThrow()
    {
        
        humanAnimator.Play("Throw");
        Debug.Log("Human is throwing");
    
    }

}
