using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HumanAbility : NetworkBehaviour
{
    public Animator humanAnimator;
    public GameObject throwPrefab;
    public ThirdPersonController controller;
    private float startMovementSpeed;
    private float startSprintMovementSpeed;
    private float startJumpHeight;

    public Material humanClothesMat;
    public Material humanHairMat;
    public Material invisMaterial;

    public SkinnedMeshRenderer[] humanMeshRenderer;


    public Transform firePoint;
    public float throwForce = 25f;

    private void Start()
    {
        startMovementSpeed = controller.MoveSpeed;
        startSprintMovementSpeed = controller.SprintSpeed;
        startJumpHeight = controller.JumpHeight;
       // StartCoroutine(TestQuick());


    }

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
        ReturnVisible();
        Debug.Log("Human is throwing");
    
    }

    public void BecomeInvisible()
    {

        foreach (SkinnedMeshRenderer mesh in humanMeshRenderer)
        {
            mesh.material = invisMaterial;
        }


    }
    public void ReturnVisible()
    {
        humanMeshRenderer[0].material = humanHairMat;
        humanMeshRenderer[1].material = humanHairMat;

        humanMeshRenderer[2].material = humanClothesMat;
        humanMeshRenderer[3].material = humanClothesMat;
        humanMeshRenderer[4].material = humanClothesMat;
        humanMeshRenderer[5].material = humanClothesMat;


    }

    public void FreezeMovement()
    {
        controller.MoveSpeed = 0;
        controller.SprintSpeed = 0;
        controller.JumpHeight = 0;
    }

    public void UnFreezeMovement()
    {
        controller.MoveSpeed = startMovementSpeed;
        controller.SprintSpeed = startSprintMovementSpeed;
        controller.JumpHeight = startJumpHeight;
        
    }

    IEnumerator TestQuick()
    {
        yield return new WaitForSeconds(2);
        BecomeInvisible();
    }
}
