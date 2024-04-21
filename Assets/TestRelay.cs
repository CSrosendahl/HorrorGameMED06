using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine.UI;
using Unity.Netcode;
using System;
using TMPro;
using Unity.Netcode.Transports.UTP;

public class TestRelay : MonoBehaviour
{
    public TMP_Text codeText;

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn +=() => { Debug.Log("Signed in" + AuthenticationService.Instance.PlayerId); };
       await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

  

    public async void CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
            Debug.Log("Relay created");

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log(joinCode);




            // Start the host
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                allocation.RelayServer.IpV4,
               (ushort)allocation.RelayServer.Port,
               allocation.AllocationIdBytes,
               allocation.Key,
               allocation.ConnectionData);

            NetworkManager.Singleton.StartHost();

            codeText.text = joinCode;
        }
        catch (RelayServiceException e)
        {
            Debug.LogError("Failed to create relay: " + e.Message);
           
        }
    }

    public async void JoinRelay(string joinCode)
    {
        try
        {
          JoinAllocation joinAllocation =  await RelayService.Instance.JoinAllocationAsync(joinCode);
            Debug.Log("Relay joined");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                joinAllocation.RelayServer.IpV4,
               (ushort)joinAllocation.RelayServer.Port,
               joinAllocation.AllocationIdBytes,
               joinAllocation.Key,
               joinAllocation.ConnectionData,
               joinAllocation.HostConnectionData);

            NetworkManager.Singleton.StartClient();
          //  NetworkManager.Singleton.NetworkConfig.PlayerPrefab = Resources.Load("Monster") as GameObject;
          

        }
        catch (RelayServiceException e)
        {
            Debug.LogError("Failed to join relay: " + e.Message);
          
        }
    }
    
}
