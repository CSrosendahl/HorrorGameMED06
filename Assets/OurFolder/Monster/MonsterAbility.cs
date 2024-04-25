using StarterAssets;
using System.Collections;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using System;

public class MonsterAbility : NetworkBehaviour
{
    public Animator monsterAnim;
    public ThirdPersonController controller;
    private float startMovementSpeed;
    private float startSprintMovementSpeed;
    private float startJumpHeight;

    public bool HumanCaught = false;

    public Collider screamTrigger;
    public AudioClip monsterScream;
    public bool canScream;
    public float screamCoolDown = 10f;

    public MonsterWinUI monsterWinUI;

    

    private void Start()
    {
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
        monsterAnim.Play("Reach");
        Debug.Log("Monster is reaching");
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
        Debug.Log("Monster is hitting something" + other.tag);
        if (IsServer && other.gameObject.CompareTag("Human")) // Only execute on the server
        {
            HumanCaught = true;
            Debug.Log(HumanCaught);
            
            monsterWinUI.UpdateUIOnClientsServerRpc(true);
            
        }
    }
}
