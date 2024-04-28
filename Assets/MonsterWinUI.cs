using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEditor;

public class MonsterWinUI : NetworkBehaviour
{
    public GameObject HumanCaughtUI;


    // Method to update the UI based on the human caught state

 

    public void UpdateUI()
    {
  

        bool allHumansCaught = true;
        foreach (GameObject human in GameManager.instance.humanList)
        {
           HumanAbility humanAbility =  human.GetComponent<HumanAbility>();

            if (!humanAbility.wasCaught)
            {
                // If any human is not caught, set the flag to false and break the loop
                allHumansCaught = false;
                break;
            }
        }
        if (allHumansCaught)
        {
            Debug.Log("All humans caught, activating UI");
            HumanCaughtUI.SetActive(true);
        }

    }

    // Server method to trigger UI update on all clients
    [ServerRpc(RequireOwnership = false)]
    public void UpdateUIOnClientsServerRpc()
    {
        RpcUpdateUIOnClientsClientRpc(); // Corrected method name
    }

    [ClientRpc]
    void RpcUpdateUIOnClientsClientRpc() // Corrected method name
    {
        UpdateUI();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MonsterWinUI))]
public class TestScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Add a button to the inspector
        if (GUILayout.Button("Run Test Function"))
        {
            // Get the TestScript component attached to the selected GameObject
            MonsterWinUI testScript = (MonsterWinUI)target;

            // Call the TestFunction
            testScript.UpdateUI();
        }
    }
}
#endif
