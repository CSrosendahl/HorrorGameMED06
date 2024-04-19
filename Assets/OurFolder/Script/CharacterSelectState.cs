using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public struct CharacterSelectState : INetworkSerializable, IEquatable<CharacterSelectState>
 {
    public ulong ClientID;
    public int CharacterID;

    public CharacterSelectState(ulong clientID, int characterID =-1)
    {
        ClientID = clientID;
        CharacterID = characterID;
    }


    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ClientID);
        serializer.SerializeValue(ref CharacterID);
    }

    public bool Equals(CharacterSelectState other)
    
    { return ClientID == other.ClientID && 
            CharacterID == other.CharacterID;
    }
}
