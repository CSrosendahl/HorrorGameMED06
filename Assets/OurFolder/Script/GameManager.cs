using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public int humanCount;
    public List<GameObject> humanList = new List<GameObject>();

    private void Start()
    {
      StartCoroutine(WaitToRunCode());
    }
  
 
    IEnumerator WaitToRunCode()
    {

        yield return new WaitForSeconds(2f);
        foreach (var playerObj in GameObject.FindGameObjectsWithTag("Human"))
        {
            humanList.Add(playerObj);
            Debug.Log("Adding:" + playerObj.gameObject.name + " to list");

        }
    }



}
