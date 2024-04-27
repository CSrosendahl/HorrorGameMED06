using StarterAssets;
using System.Collections;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using System;
using JetBrains.Annotations;

public class MonsterAbility : NetworkBehaviour
{
    public Animator monsterAnim;
    public ThirdPersonController controller;
    private float startMovementSpeed;
    private float startSprintMovementSpeed;
    private float startJumpHeight;
    public PlayerManager playerManager;

 

    public Collider screamTrigger;
    public AudioClip monsterScream;
    public bool canScream;
    public float screamCoolDown = 10f;

    public MonsterWinUI monsterWinUI;
    public bool monsterIsReaching;
   

    

    private void Start()
    {
        
      
        monsterIsReaching = false;
        canScream = true;
        startMovementSpeed = controller.MoveSpeed;
        startSprintMovementSpeed = controller.SprintSpeed;
        startJumpHeight = controller.JumpHeight;
        monsterAnim = GetComponent<Animator>();
        screamTrigger.enabled = false;
        GameObject monsterWinUIGameObject = GameObject.FindObjectOfType<MonsterWinUI>().gameObject;
        monsterWinUI = monsterWinUIGameObject.GetComponent<MonsterWinUI>();

    }

    public void MonsterReach()
    {
        monsterIsReaching = true;
        monsterAnim.Play("Reach");
        Debug.Log("Monster is reaching");
        Debug.Log(monsterIsReaching);
    }

    public void MonsterScream()
    {
        if(canScream)
        {
            monsterAnim.Play("Scream");
            MonsterScreamAudio();
            FreezeMovement();
            screamTrigger.enabled = true;
            canScream = false;

            StartCoroutine(WaitToUnfreeze(2.15f));
            StartCoroutine(ScreamCoolDown(screamCoolDown));
            Debug.Log("Monster is screaming");
        }

       

    }


    IEnumerator ScreamCoolDown(float duration)
    {
        yield return new WaitForSeconds(duration);
        canScream = true;
    }

    public void MonsterScreamAudio()
    {
        AudioSource.PlayClipAtPoint(monsterScream, transform.position);

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

    private void OnTriggerEnter(Collider other)
    {
        if(IsClient && IsOwner)
        {
            if (other.gameObject.CompareTag("Human") && monsterIsReaching) // Only execute on the server
            {
              

                if (IsClient)
                {
                    CaughtOnClientsServerRPC();
                }

                   
             

            }
        }
 
    }

  

    [ServerRpc(RequireOwnership = false)]
    void CaughtOnClientsServerRPC()
    {
        // This function will be executed on all clients by the server
        RpcHumanCaughtClientRPC();
    }

    [ClientRpc]
    void RpcHumanCaughtClientRPC()
    {

        

        foreach (var playerGameObject in GameObject.FindGameObjectsWithTag("Human"))
        {
            var HumanAbility = playerGameObject.GetComponent<HumanAbility>();
            var playerManager = playerGameObject.GetComponent<PlayerManager>();
            if (HumanAbility != null)
            {
                if(playerManager.isBryce)
                {
                    Debug.Log("Bryce Should scream");
                    Debug.Log("Bryce caught");
                    HumanAbility.caughtClip = HumanAbility.bryceClip;
                }
                else
                {
                    Debug.Log("Sofie Should scream");
                    Debug.Log("Sofie caught");
                    HumanAbility.caughtClip = HumanAbility.sofieClip;
                }

                HumanAbility.WasCaught();
                
                monsterWinUI.UpdateUIOnClientsServerRpc();
              
            }
        }
    }
}
