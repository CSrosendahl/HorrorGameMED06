using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine.UI;

public class TestLobby : MonoBehaviour
{
    [SerializeField] private Button createLobby;
    [SerializeField] private Button joinLobby;
    private Lobby hostLobby;
    private float heartbeatTimer;
    // Start is called before the first frame update

    private void Awake()
    {
        createLobby.onClick.AddListener(() => CreateLobby());
        joinLobby.onClick.AddListener(() => QuickJoinLobby());
    }

    private async void Start()
    {
       await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in" + AuthenticationService.Instance.PlayerId);
        };
       await AuthenticationService.Instance.SignInAnonymouslyAsync();
       // CreateLobby();
    }

    private async void CreateLobby()
    {
        try
        {
            string lobbyName = "TestLobby";
            int maxPlayers = 4;
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);
            hostLobby = lobby;
            Debug.Log("Lobby created");
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError("Failed to create lobby: " + e.Message);
        }

    }

    private void Update()
    {
        HeartBeatHandler();
    }

    private async void HeartBeatHandler()
    {
        if (hostLobby != null)
        {
            heartbeatTimer -= Time.deltaTime;
            if (heartbeatTimer < 0f)
            {
                float heartbeatTimerMax = 15f;
                heartbeatTimer = heartbeatTimerMax;
               await  LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
                
            }
        }

    }

    private void JoinLobby(string lobbyCode)
    {
        try
        {
            Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode);
            Debug.Log("Joined lobby withg code" + lobbyCode);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError("Failed to join lobby: " + e.Message);
        }
      
    }

    private async void QuickJoinLobby()
    {
        try
        {
            await LobbyService.Instance.QuickJoinLobbyAsync();
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError("Failed to join lobby: " + e.Message);
        }
    }

}
