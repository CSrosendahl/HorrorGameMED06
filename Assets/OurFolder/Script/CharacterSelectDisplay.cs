using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using System;
using UnityEditor;

public class CharacterSelectDisplay : NetworkBehaviour
{
    private NetworkList<CharacterSelectState> players;
    // Start is called before the first frame update

   
    [SerializeField] private CharacterDatabase characterDatabase;
    [SerializeField] private Transform charactersHolder;
    [SerializeField] private CharacterSelectButton selectButtonPrefab;
    [SerializeField] private PlayerCard[] playerCards;
    [SerializeField] private GameObject characterInfoPanel;
    [SerializeField] private TMP_Text characterNameText;

    private void Awake()
    {
        players = new NetworkList<CharacterSelectState>();
    }
    public override void OnNetworkSpawn()
    {
        if (IsClient)
        {
            Character[] allcharacters = characterDatabase.GetAllCharacters();
            foreach (var character in allcharacters)
            {
              var selectbuttonInastance = Instantiate(selectButtonPrefab, charactersHolder);
                selectbuttonInastance.SetCharacter(this,character);
            }
            players.OnListChanged += HandlePlayersStateChanged;
        }
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnected;
           
            Debug.Log("Server spawned");
        }
        foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClientsList)
        {
            HandleClientConnected(client.ClientId);
        }
    }
    public override void OnNetworkDespawn()
    {
        if (IsClient)
        {
            players.OnListChanged -= HandlePlayersStateChanged;
        }
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnected;
        }
       if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnected;


        }
      
    }

 
    private void HandleClientDisconnected(ulong clientID)
    {
        players.Add(new CharacterSelectState(clientID));
    }

    private void HandleClientConnected(ulong clientID)
    {
       for (int i = 0; i < players.Count; i++)
        {
            if (players[i].ClientID == clientID)
            {
                players.RemoveAt(i);
                break;
            }
        }
    }

    public void Select(Character character)
    {
        characterNameText.text = character.DisplayName;
        characterInfoPanel.SetActive(true);
       
        SelectServerRpc(character.ID);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SelectServerRpc(int characterID, ServerRpcParams serverRpcParams = default)
    {
     for (int i = 0; i < players.Count; i++)
        {
            if (players[i].ClientID == serverRpcParams.Receive.SenderClientId)
            {
                players[i] = new CharacterSelectState(players[i].ClientID, characterID);
          
            }
        }
    }

    private void HandlePlayersStateChanged(NetworkListEvent<CharacterSelectState> changeEvent)
    {
       for (int i = 0; i < playerCards.Length; i++)
        {
           if (players.Count > i)
            {
                playerCards[i].UpdateDisplay(players[i]);
            }
            else
            {
                playerCards[i].DisableDisplay();
            }
        }
        for (int i = 0; i < players.Count; i++)
        {
            playerCards[i].UpdateDisplay(players[i]);
        }
    }

}
