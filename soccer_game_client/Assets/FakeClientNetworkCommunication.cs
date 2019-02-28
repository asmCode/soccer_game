using UnityEngine;

public class FakeClientNetworkCommunication : INetworkCommunication
{
    private FakeClient m_client1;
    private FakeClient m_client2;

    public FakeClientNetworkCommunication(FakeClient client1, FakeClient client2)
    {
        m_client1 = client1;
        m_client2 = client2;
    }

    public void Initialize()
    {
        Debug.Log("FakeClients: Initialize");
    }

    public void Send(byte[] data, int size, INetworkAddress address)
    {
    }

    public bool Receive(byte[] data, out int size, out INetworkAddress address)
    {
        size = 0;
        address = null;

        if (ReceiveFromClient(m_client1, data, ref size, ref address))
            return true;

        if (ReceiveFromClient(m_client2, data, ref size, ref address))
            return true;

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
        Debug.LogFormat("FakeClients: Close");
    }
}
