using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private CinemachineVirtualCamera CVC;
    private Cinemachine3rdPersonFollow cinemachine3RdPersonFollow;
   

    public float shoulderOffset; // 0.5f for monster, 0.2f for human is best
    public bool isHuman;
    public bool isMonster;

    public bool hasInvis;
    public bool hasKey;
    public bool hasBuff;
    

    private void Awake()
    {
        CVC = FindAnyObjectByType<CinemachineVirtualCamera>();
        cinemachine3RdPersonFollow = CVC.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

        if(isMonster)
        {
            cinemachine3RdPersonFollow.ShoulderOffset.y = shoulderOffset;
        }
        else if(isHuman)
        {
            cinemachine3RdPersonFollow.ShoulderOffset.y = shoulderOffset;
        }
    }

  
}
