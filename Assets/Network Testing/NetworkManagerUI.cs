using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button HostButton;
    [SerializeField] private Button ClientButton;
    [SerializeField] private TMP_InputField JoinCodeInput;
    [SerializeField] GameObject JoinScreen;
    [SerializeField] TMP_Text codeText;

    private TestRelay testRelay;

    public AudioSource audioSource;
    

    private void Awake()
    {
        // Ensure testRelay reference is set
        testRelay = FindObjectOfType<TestRelay>();
        if (testRelay == null)
        {
            Debug.LogError("TestRelay script not found in the scene.");
            return;
        }

        HostButton.onClick.AddListener(StartingRelay);
        ClientButton.onClick.AddListener(JoinRelayWithCode);
       
        audioSource.Play();

    }

    private void Start()
    {
     
    }

    private void JoinRelayWithCode()
    {
        // Get the join code from the input field
        string joinCode = JoinCodeInput.text;

        // Check if join code is not empty
        if (!string.IsNullOrEmpty(joinCode))
        {
            // Call JoinRelay with the join code
            testRelay.JoinRelay(joinCode);
            JoinScreen.SetActive(false);
            codeText.text = joinCode;
            audioSource.Stop();
        }
        else
        {
            // Handle the case when join code is empty
            Debug.LogWarning("Join code is empty. Please enter a valid join code.");
        }
    }


  private void StartingRelay()
    {
        testRelay.CreateRelay();
        JoinScreen.SetActive(false);
        audioSource.Stop();
    }
}
