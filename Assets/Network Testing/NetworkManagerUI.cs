using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkManagerUI : MonoBehaviour
{

    [SerializeField] private Button HostButton;
    [SerializeField] private Button ClientButton;
    [SerializeField] private Button ServButton ;

    private void Awake()
    {
        HostButton.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
        ServButton.onClick.AddListener(() => NetworkManager.Singleton.StartServer());
        ClientButton.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
