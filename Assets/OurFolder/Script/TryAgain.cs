using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{
   
    public void tryAgain()
    {
       SceneManager.LoadScene(2);
        Debug.Log("Try Again");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
