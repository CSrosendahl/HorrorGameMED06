using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToLoadNextScene());
    }

 

    IEnumerator WaitToLoadNextScene()
    {
        yield return new WaitForSeconds(77.17626f);
        LoadNext();
      
    }
    public void LoadNext()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
