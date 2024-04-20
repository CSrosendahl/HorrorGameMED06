using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterSpawner : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterDatabase characterDatabase; //Get allll the characters
    [SerializeField] private Transform HumanSpawnPoint; //Human spawn point. Assign in inspector
    [SerializeField] private Transform MonsterSpawnPoint; //Monster spawn point. Assign in inspector
    // Add more spawn points as needed

    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }

        foreach (var client in MatchplayNetworkServer.Instance.ClientData)
        {
            var character = characterDatabase.GetCharacterById(client.Value.characterId);
            if (character != null) //Yay for bools 
            {
                // Assign spawn point based on character's isHuman flag
                if (character.IsHuman)
                {
                    var characterInstance = Instantiate(character.GameplayPrefab, HumanSpawnPoint.position, Quaternion.identity);
                    characterInstance.SpawnAsPlayerObject(client.Value.clientId);
                }
                else if (character.IsMonster) //it's a monster
                {
                    var characterInstance = Instantiate(character.GameplayPrefab, MonsterSpawnPoint.position, Quaternion.identity);
                    characterInstance.SpawnAsPlayerObject(client.Value.clientId);
                }

            }
        }
    }
}
