using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{

    public CinemachineVirtualCamera CVC;
    public Cinemachine3rdPersonFollow cinemachine3RdPersonFollow;
   

    public float shoulderOffset; // 0.5f for monster, 0.2f for human is best
    public bool isHuman;
    public bool isMonster;

    public bool hasInvis;
    public bool hasKey;
    public bool hasBuff;

    public GameObject humanUI;
    public GameObject monsterUI;


    private void Start()
    {
        if (CVC == null)
        {
            CVC = FindAnyObjectByType<CinemachineVirtualCamera>();
            cinemachine3RdPersonFollow = CVC.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        }
        if (IsClient && IsOwner)
        {
            {
                if (isHuman)
                {
                    cinemachine3RdPersonFollow.ShoulderOffset = new Vector3(0, shoulderOffset, 0);
                }
                else if (isMonster)
                {
                    cinemachine3RdPersonFollow.ShoulderOffset = new Vector3(0, shoulderOffset, 0);
                }
            }
        }

        if (isHuman)
        {
            humanUI.SetActive(true);
          
        }
        else
        {
           
            monsterUI.SetActive(true);
        }



    }

}
