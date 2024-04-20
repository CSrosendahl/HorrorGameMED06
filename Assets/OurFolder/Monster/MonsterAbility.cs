using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MonsterAbility : NetworkBehaviour
{
    public Animator monsterAnim;
    public ThirdPersonController controller;
    private float startMovementSpeed;
    private float startSprintMovementSpeed;
    private float startJumpHeight;

    public Collider screamTrigger;

    
    private void Start()
    {

        startMovementSpeed = controller.MoveSpeed;
        startSprintMovementSpeed = controller.SprintSpeed;
        startJumpHeight = controller.JumpHeight;
        monsterAnim = GetComponent<Animator>();
        screamTrigger.enabled = false;



    }


    public void MonsterReach()
    {
        monsterAnim.Play("Reach");
        Debug.Log("Monster is reaching");
    }

    public void MonsterScream()
    {
        monsterAnim.Play("Scream");
        FreezeMovement();
        screamTrigger.enabled = true;
        StartCoroutine(WaitToUnfreeze(2.15f));
        Debug.Log("Monster is screaming");
    }

    IEnumerator WaitToUnfreeze(float wait)
    {
        
        yield return new WaitForSeconds(wait);
        UnFreezeMovement();


    }

    public void UnFreezeMovement()
    {
        controller.MoveSpeed = startMovementSpeed;
        controller.SprintSpeed = startSprintMovementSpeed;
        controller.JumpHeight = startJumpHeight;
        screamTrigger.enabled = false;
    }

    public void FreezeMovement()
    {
        controller.MoveSpeed = 0;
        controller.SprintSpeed = 0;
        controller.JumpHeight = 0;
    }
}
