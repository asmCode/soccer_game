﻿using UnityEngine;

public class FakeServerNetworkCommunication : INetworkCommunication
{
    public FakeServerNetworkCommunication()
    {
    }

    public void Initialize()
    {
        Debug.Log("FakeServer: Initialize");
    }

    public void Send(byte[] data, int size, INetworkAddress address)
    {
        Debug.LogFormat("FakeServer: Send, size = {0}", size);
    }

    public bool Receive(byte[] data, out int size, out INetworkAddress address)
    {
        size = 0;
        address = null;

        //if (ReceiveFromClient(m_client1, data, ref size, ref address))
        //    return true;

        //if (ReceiveFromClient(m_client2, data, ref size, ref address))
        //    return true;

        return false;
    }

    private static bool ReceiveFromClient(FakeClient client, byte[] data, ref int size, ref INetworkAddress address)
    {
        if (client.OutMessages.Count == 0)
            return false;

        address = client.NetworkAddress;
        var rawData = client.OutMessages.Dequeue();
        rawData.CopyTo(data, out size);
        return true;
    }

    public void Close()
    {
        Debug.LogFormat("FakeServer: Close");
    }
}
