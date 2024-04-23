using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HumanAbility : NetworkBehaviour
{
    public Animator humanAnimator;
    public GameObject[] throwPrefabs;
    public ThirdPersonController controller;
    public PlayerManager playerManager;
    private float startMovementSpeed;
    private float startSprintMovementSpeed;
    private float startJumpHeight;

    public Material humanClothesMat;
    public Material humanHairMat;
    public Material invisMaterial;

    public SkinnedMeshRenderer[] humanMeshRenderer;


    public Transform firePoint;
    public float throwForce = 35f;

    private void Start()
    {
        startMovementSpeed = controller.MoveSpeed;
        startSprintMovementSpeed = controller.SprintSpeed;
        startJumpHeight = controller.JumpHeight;
      


    }

    // This is being called as an animation event. It is not being called by the player's input. Humanthrow is
    public void Throw()
    {
        // Get a random throw object
        GameObject throwPrefab = throwPrefabs[Random.Range(0, throwPrefabs.Length)];

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

    public void OpenChest()
    {
       
        humanAnimator.Play("OpenChest");
     
        FreezeMovement();
        StartCoroutine(WaitXTime(9.15f));
        Debug.Log("Human is opening chest");
    
    }

    public void BecomeInvisible()
    {

        foreach (SkinnedMeshRenderer mesh in humanMeshRenderer)
        {
            mesh.material = invisMaterial;
        }

        StartCoroutine(ReturnVisibleAfterXTime(10f));


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

    public void ShrinkCharacter()
    {
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        StartCoroutine(ReturnToNormalSizeAfterXTime(10f));
    }
    public void UnShrinkCharacter()
    {

        transform.localScale = new Vector3(1f, 1f, 1f);

    }

    IEnumerator ReturnToNormalSizeAfterXTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        UnShrinkCharacter();
    }

    IEnumerator ReturnVisibleAfterXTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        ReturnVisible();
    }
    IEnumerator WaitXTime(float duration)
    {
        
        yield return new WaitForSeconds(duration);
        UnFreezeMovement();
       
    
    }
}
