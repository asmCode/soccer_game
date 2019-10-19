using System.Collections.Generic;
using UnityEngine;

public class FakeServerNetworkCommunication : INetworkCommunication
{
    private FakeAddress m_serverAddress;
    private NetworkMessageSerializer m_netMsgSerializer = new NetworkMessageSerializer(new BinaryDataWriter(), new BinaryDataReader());

    public Queue<RawData> OutMessages
    {
        get;
        private set;
    }

    public FakeServerNetworkCommunication()
    {
        OutMessages = new Queue<RawData>();
        m_serverAddress = new FakeAddress("server-address");
    }

    public void Initialize()
    {
        Debug.Log("FakeServerNetworkCommunication: Initialize");
    }

    public void Send(byte[] data, int size, INetworkAddress address)
    {
        var netMsg = m_netMsgSerializer.Deserialize(data, size);
        if (netMsg == null)
            return;

        Debug.LogFormat("FakeServerNetworkCommunication: Send, size = {0}, type = {1}", size, netMsg.m_type);

        switch (netMsg.m_type)
        {
            case NetworkMessageType.JoinRequest:
                var msg = new JoinAccept();
                msg.m_team = 0;
                m_netMsgSerializer.Serialize(msg);
                OutMessages.Enqueue(new RawData(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize));
                // TODO: add this message to OutMessages
                break;
        }
    }

    public bool Receive(byte[] data, out int size, out INetworkAddress address)
    {
        size = 0;
        address = null;

        if (OutMessages.Count == 0)
            return false;

        address = m_serverAddress;
        var rawData = OutMessages.Dequeue();
        rawData.CopyTo(data, out size);
        return true;
    }

    public void Close()
    {
        Debug.LogFormat("FakeServerNetworkCommunication: Close");
    }
}
