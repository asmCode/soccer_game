﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerAddressGetter : IServerAddressGetter
{
    public INetworkAddress GetServerAddress()
    {
        return new UdpNetworkAddress("127.0.0.1", GameSettings.ServerDefaultPort);
        // return new UdpNetworkAddress("192.168.0.110", GameSettings.ServerDefaultPort);
        //return new UdpNetworkAddress("73.93.128.73", GameSettings.ServerDefaultPort);
    }
}
