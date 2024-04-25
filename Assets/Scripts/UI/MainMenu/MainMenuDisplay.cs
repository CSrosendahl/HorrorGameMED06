using System;
using TMPro;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
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
        string joinCode = joinCodeInputField.text;

        // Check if the join code is correct
        try
        {
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            if (allocation != null)
            {
                loadingText.text = "Loading Character Selection...";
                await ClientSingleton.Instance.Manager.BeginConnection(joinCode);
            }
           
        }
        catch (Exception e)
        {
            Debug.Log("Wrong or No join code " + e.Message);
            loadingText.text = "Wrong or No join code"; // Update text to indicate an error
            loadingText.color = Color.red;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
