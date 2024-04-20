using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterSpawner : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterDatabase characterDatabase;
    [SerializeField] private Transform HumanSpawnPoint;
    [SerializeField] private Transform MonsterSpawnPoint;
    // Add more spawn points as needed

    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }

        foreach (var client in MatchplayNetworkServer.Instance.ClientData)
        {
            var character = characterDatabase.GetCharacterById(client.Value.characterId);
            if (character != null)
            {
                // Assign spawn point based on character ID
                if (client.Value.characterId == 1)
                {
                    var characterInstance = Instantiate(character.GameplayPrefab, HumanSpawnPoint.position, Quaternion.identity);
                    characterInstance.SpawnAsPlayerObject(client.Value.clientId);
                }
                else if (client.Value.characterId == 3)
                {
                    var characterInstance = Instantiate(character.GameplayPrefab, MonsterSpawnPoint.position, Quaternion.identity);
                    characterInstance.SpawnAsPlayerObject(client.Value.clientId);
                }
                
            }
        }
    }
}
