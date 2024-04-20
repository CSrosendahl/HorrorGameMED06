using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;

using UnityEngine;

public class AffectedByScream : NetworkBehaviour
{

    private CameraShake cameraShake;
    private HumanAbility humanAbility;
    private Animator humanAnim;

    public float shakeIntensity = 1f;
    public float shakeDuration = 1f;
    public float freezeDuration = 1f;

    private void Start()
    {
       cameraShake = GetComponent<CameraShake>();
       humanAnim = GetComponent<Animator>();
       humanAbility= GetComponent<HumanAbility>();
     //  ScreamEffect();
    }

    public void ScreamEffect()
    {
        cameraShake.ShakeCamera(shakeIntensity, shakeDuration);
        humanAnim.Play("Scared");
        humanAbility.FreezeMovement();
        StartCoroutine(WaitToUnfreeze(freezeDuration));
    }

    IEnumerator WaitToUnfreeze(float duration)
    {
        yield return new WaitForSeconds(duration);

        
        humanAbility.UnFreezeMovement();
    }

}
