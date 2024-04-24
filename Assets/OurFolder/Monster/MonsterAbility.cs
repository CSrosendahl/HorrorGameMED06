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

    public bool HumanCaught = false;

    public Collider screamTrigger;

    public MonsterWinUI monsterWinUI;


    private void Start()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Monster is hitting something" + other.tag);
        if (IsServer && other.gameObject.CompareTag("Human")) // Only execute on the server
        {
            HumanCaught = true;
            Debug.Log(HumanCaught);
            
            monsterWinUI.UpdateUIOnClientsServerRpc(true);
            
        }
    }
}
