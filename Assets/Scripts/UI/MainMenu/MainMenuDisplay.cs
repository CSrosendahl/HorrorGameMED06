using System;
using TMPro;
using UnityEngine;

public class MainMenuDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private TMP_Text loadingText;

    public async void StartHost()
    {
        loadingText.text = "Loading Character Selection...";
        await HostSingleton.Instance.StartHostAsync();
       
    }

    public async void StartClient()
    {
        loadingText.text = "Loading Loading Character Selection...";
        await ClientSingleton.Instance.Manager.BeginConnection(joinCodeInputField.text);
      
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
