using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InfoUIScript : MonoBehaviour
{
    public GameObject humanUI; // Reference to the UI elements for human player
    public GameObject monsterUI; // Reference to the UI elements for monster player
    public PlayerManager humanManager;
    public PlayerManager monsterManager;

    void Start()
    {
        if (humanManager.isHuman)
        {
            humanUI.SetActive(true);
            monsterUI.SetActive(false);
            Debug.Log("Human UI Active");
        }

        else
        {
            humanUI.SetActive(false);
            monsterUI.SetActive(true);
            Debug.Log("Monster UI Active");
        }


    }

    private void Update()
    {
     
    }
}
