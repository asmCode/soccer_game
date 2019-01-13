using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerAddressGetter
{
    public INetworkAddress GetServerAddress()
    {
        return new UdpNetworkAddress("192.168.0.110", GameSettings.ServerDefaultPort);
    }
}
