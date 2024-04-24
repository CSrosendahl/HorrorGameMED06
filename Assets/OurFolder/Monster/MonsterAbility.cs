using StarterAssets;
using System.Collections;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;

public class MonsterAbility : NetworkBehaviour
{
    public Animator monsterAnim;
    public ThirdPersonController controller;
    private float startMovementSpeed;
    private float startSprintMovementSpeed;
    private float startJumpHeight;

    public Collider screamTrigger;
    public TMP_Text monsterCaughtText; // Reference to the UI Text component

    private void Start()
    {
        startMovementSpeed = controller.MoveSpeed;
        startSprintMovementSpeed = controller.SprintSpeed;
        startJumpHeight = controller.JumpHeight;
        monsterAnim = GetComponent<Animator>();
        screamTrigger.enabled = false;
        monsterCaughtText.enabled = false; // Initially hide the UI Text
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

    [ClientRpc]
    private void RpcEnableMonsterCaughtTextClientRpc()
    {
        monsterCaughtText.enabled = true;
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
        if (IsServer && other.gameObject.CompareTag("Human")) // Only execute on the server
        {
            Debug.Log("Monster is hitting human");
            // Call Rpc to enable UI Text on all clients
            RpcEnableMonsterCaughtTextClientRpc();
        }
    }
}
