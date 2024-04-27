using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }
    public TextMeshProUGUI chestsopenedUI;

    public GameObject toolTips_GO;

    public void ToggleToolTips()
    {
        chestsopenedUI.text = "Chests opened: " + ChestManager.instance.chestsOpened.ToString() + " / " + ChestManager.instance.chests.Length.ToString();
        toolTips_GO.SetActive(!toolTips_GO.activeSelf);
       
        
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(4);
        Debug.Log("Try Again");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
