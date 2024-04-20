using System;

public class ClientData 
{

    public ulong clientId;
    public int characterId = -1;

    public ulong ClientId { get => clientId; set => clientId = value; }

    public ClientData(ulong clientId)
    {
        this.ClientId = clientId;
    }
}
