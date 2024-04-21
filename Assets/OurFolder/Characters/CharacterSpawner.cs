using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterSpawner : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterDatabase characterDatabase;
    [SerializeField] private Transform HumanSpawnPoint; // Human spawn point. Assign in inspector
    [SerializeField] private Transform MonsterSpawnPoint; // Monster spawn point. Assign in inspector

    private void Start()
    {
        if (!IsServer) { return; }

        foreach (var client in MatchplayNetworkServer.Instance.ClientData)
        {
            var character = characterDatabase.GetCharacterById(client.Value.characterId);
            if (character != null)
            {
                // Assign spawn point based on character's isHuman flag
                if (character.IsHuman) //are we human?
                {
                    var characterInstance = Instantiate(character.GameplayPrefab, HumanSpawnPoint.position, Quaternion.identity);
                    characterInstance.SpawnAsPlayerObject(client.Value.clientId);
                }
                else if (character.IsMonster) //or are we Dancer?
                {
                    var characterInstance = Instantiate(character.GameplayPrefab, MonsterSpawnPoint.position, Quaternion.identity);
                    characterInstance.SpawnAsPlayerObject(client.Value.clientId);
                }
                // Add more conditions for different character types if needed
            }
        }
    }


  
}
