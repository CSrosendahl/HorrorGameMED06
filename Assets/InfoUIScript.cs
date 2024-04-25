using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InfoUIScript : MonoBehaviour
{
    public GameObject humanUI; // Reference to the UI elements for human player
    public GameObject monsterUI; // Reference to the UI elements for monster player

    void Start()
    {
        // Find the PlayerManager script in the scene
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();

        // Check if the PlayerManager script exists
        if (playerManager != null)
        {
            // Check if the player is human or monster and show the corresponding UI
            if (playerManager.isHuman)
            {
                humanUI.SetActive(true);
                monsterUI.SetActive(false);
                Debug.Log("Human UI Active");
            }
            else if (playerManager.isMonster)
            {
                humanUI.SetActive(false);
                monsterUI.SetActive(true);
            }
            else
            {
                // If neither is true, hide both UIs
                humanUI.SetActive(false);
                monsterUI.SetActive(false);
            }
        }
       
    }
}
