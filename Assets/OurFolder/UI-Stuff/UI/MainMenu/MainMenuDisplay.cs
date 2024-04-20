using System;
using TMPro;
using UnityEngine;

public class MainMenuDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private TMP_Text loadingText;

   
    private ClientGameManager gameManager;

    private void Start()
    {
   
    }


    public async void StartHost()
    {
        loadingText.text = "Loading...";
        await HostSingleton.Instance.StartHostAsync();
       
    }

    public async void StartClient()
    {
        loadingText.text = "Loading...";
        await ClientSingleton.Instance.Manager.BeginConnection(joinCodeInputField.text);
      
    }
}
