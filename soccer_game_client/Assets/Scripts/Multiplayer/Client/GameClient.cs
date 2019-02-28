using UnityEngine;
using Ssg.Core.Networking;
using System;

public class GameClient
{
    private byte[] m_data;
    private INetworkCommunication m_com;

    private MessageSerializer m_msgSerializer;
    private NetworkMessageSerializer m_netMsgSerializer = new NetworkMessageSerializer(new BinaryDataWriter(), new BinaryDataReader());
    private MessageQueue m_msgQueue = new MessageQueue();
    private INetworkAddress m_serverAddress;

    public event System.Action Connected;
    public event System.Action OpponentFound;

    public GameClient(INetworkCommunication com)
    {
        m_data = new byte[256];

        m_com = com;
        m_com.Initialize();

        m_msgSerializer = MessageSerializerFactory.Create();
        m_msgQueue = new MessageQueue();
}

    public bool IsConnected()
    {
        return false;
        // return m_connection != null;
    }

    // public abstract Connection Connect();
    public virtual void Disconnect()
    {
        //if (m_connection == null)
        //    return;

        //m_connection.Close();
        //m_connection = null;
    }

    public void Update()
    {
        while (true)
        {
            int size;
            INetworkAddress address;
            if (!m_com.Receive(m_data, out size, out address))
                return;

            var msg = m_netMsgSerializer.Deserialize(m_data, size);
            if (msg == null)
                return;

            ProcessMessage(msg);
        }
    }

    private void GetMessagesFromSever()
    {
        //if (m_connection == null)
        //    return;

        //while (true)
        //{
        //    var netMsg = m_connection.GetMessage();
        //    if (netMsg == null)
        //        break;

        //    // var msg = m_msgSerializer.Deserialize(netMsg.Data);
        //    // m_msgQueue.AddMessage(msg);
        //}
    }

    public void SendReadyToStart()
    {
        var msg = new ReadyToStart();
        m_netMsgSerializer.Serialize(msg);
        m_com.Send(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize, m_serverAddress);
    }

    public void Send(MatchMessage message)
    {
        //if (m_connection == null)
        //    return;

        //var networkMsg = CreateNetworkMessage(message);
        //m_connection.Send(networkMsg);
    }

    public MatchMessage GetMatchMessage()
    {
        if (m_msgQueue.Empty())
            return null;

        return m_msgQueue.Dequeue();
    }

    public void Join()
    {
        var serverAddressGetter = new ServerAddressGetter();
        m_serverAddress = serverAddressGetter.GetServerAddress();

        var msg = new JoinRequest();
        msg.m_clientVersion = 0;
        msg.m_playerName = SystemInfo.deviceName;
        m_netMsgSerializer.Serialize(msg);

        m_com.Send(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize, m_serverAddress);
    }

    private NetworkMessage CreateNetworkMessage(MatchMessage message)
    {
        //var networkMsg = 
        //    new NetworkMessage();
        //networkMsg.Data = m_msgSerializer.Serialize(message);
        //return networkMsg;
        return null;
    }

    protected void NotifyNewConnection()
    {
        Log.Message("Connected to the server.");

        if (Connected != null)
            Connected();
    }

    private void ProcessMessage(NetworkMessage msg)
    {
        switch (msg.m_type)
        {
            case NetworkMessageType.JoinAccept:
                Debug.Log("Join Accept");
                break;

            case NetworkMessageType.OpponentFound:
                Debug.LogFormat("Opponent found: {0}", ((OpponentFound)msg.m_msg).m_playerName);
                if (OpponentFound != null)
                    OpponentFound();
                break;

            case NetworkMessageType.StartMatch:
                Debug.LogFormat("StartMatch");
                break;
        }
    }
}
