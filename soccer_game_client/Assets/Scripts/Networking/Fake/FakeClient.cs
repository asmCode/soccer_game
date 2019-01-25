using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawData
{
    public byte[] Data
    {
        get;
        private set;
    }

    public RawData(byte[] data, int size)
    {
        Data = new byte[size];
        System.Array.Copy(data, Data, size);
    }

    public void CopyTo(byte[] data, out int size)
    {
        size = Data.Length;
        System.Array.Copy(Data, data, Data.Length);
    }
}

// This is fake client to be used on the true Server scene. This class pretends real client.
public class FakeClient
{
    private string m_playerName;
    private NetworkMessageSerializer m_netMsgSerializer = new NetworkMessageSerializer(new BinaryDataWriter(), new BinaryDataReader());

    public Queue<RawData> OutMessages
    {
        get;
        private set;
    }

    public INetworkAddress NetworkAddress
    {
        get;
        private set;
    }

    public FakeClient(string playerName, string netAddress)
    {
        OutMessages = new Queue<RawData>();
        m_playerName = playerName;

        NetworkAddress = new FakeAddress(netAddress);
    }

    public void SendJoinRequest()
    {
        var msg = new JoinRequest();
        msg.m_playerName = m_playerName;
        msg.m_clientVersion = 1;
        m_netMsgSerializer.Serialize(msg);

        OutMessages.Enqueue(new RawData(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize));
    }

    public void SendReadyToStart()
    {
        var msg = new ReadyToStart();
        m_netMsgSerializer.Serialize(msg);

        OutMessages.Enqueue(new RawData(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize));
    }

    public void Update(float deltaTime)
    {

    }
}
