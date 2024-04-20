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
            {   // Assign spawn point based on character's isHuman flag
                if (character.IsHuman) //Ýaay for bools!
                {
                    var characterInstance = Instantiate(character.GameplayPrefab, HumanSpawnPoint.position, Quaternion.identity);
                    characterInstance.SpawnAsPlayerObject(client.Value.clientId);
                }
                else if (character.IsMonster) //Its a monster!
                {
                    var characterInstance = Instantiate(character.GameplayPrefab, MonsterSpawnPoint.position, Quaternion.identity);
                    characterInstance.SpawnAsPlayerObject(client.Value.clientId);
                }

            }
        }
    }
}
