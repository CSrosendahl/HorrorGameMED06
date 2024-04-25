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
        // Find the PlayerManager script in the scene
      

        // Check if the PlayerManager script exists
       
            // Check if the player is human or monster and show the corresponding UI
            if (humanManager.isHuman)
            {
                humanUI.SetActive(true);
                monsterUI.SetActive(false);
                Debug.Log("Human UI Active");
            }
            else if (monsterManager.isMonster)
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
