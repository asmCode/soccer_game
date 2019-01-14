using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientInfo
{
    public ClientInfo(string name, INetworkAddress address)
    {
        Name = name;
        Address = address;
    }

    public string Name { get; set; }
    public INetworkAddress Address { get; set; }
    public bool IsReadyToStart { get; set; }
}
