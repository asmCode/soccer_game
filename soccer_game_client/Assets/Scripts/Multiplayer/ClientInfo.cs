using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientInfo
{
    public ClientInfo(string name, byte team, INetworkAddress address)
    {
        Name = name;
        Team = team;
        Address = address;
        LastMsgNum = -1;
    }

    public string Name { get; set; }
    public INetworkAddress Address { get; set; }
    public bool IsReadyToStart { get; set; }
    public byte Team { get; set; }

    public int LastMsgNum { get; set; }
}
