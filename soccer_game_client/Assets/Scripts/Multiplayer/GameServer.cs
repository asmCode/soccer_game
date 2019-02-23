using UnityEngine;
using Ssg.Core.Networking;
using System;

public class GameServer
{
    private byte[] m_data;
    private NetworkMessageSerializer m_netMsgSerializer = new NetworkMessageSerializer(new BinaryDataWriter(), new BinaryDataReader());
    private MessageQueue m_gameMsgQueue = new MessageQueue();

    public event System.Action PlayersConnected;

    private IGameServerState m_state;
    private INetworkCommunication m_com;
    private INetworkAddress m_serverAddress;

    private ClientInfo[] m_clientInfos;

    public GameServer(INetworkCommunication networkCommunication)
    {
        m_data = new byte[256];
        m_clientInfos = new ClientInfo[2];
        m_com = networkCommunication;
    }

    public void StartServer()
    {
        Debug.Log("Staring server.");

        m_com.Initialize();

        m_state = WaitingForPlayers.Get();
    }

    // Implement message queue and use it in the ServerScene.
    // Make it possible to have one fake and one real client.

    public void StopServer()
    {
        m_com.Close();
    }

    public void AddClient(ClientInfo clientInfo)
    {
        if (m_clientInfos[0] == null)
        {
            m_clientInfos[0] = clientInfo;
            return;
        }

        if (m_clientInfos[1] == null)
        {
            m_clientInfos[1] = clientInfo;
            return;
        }
    }

    public bool IsClientConnected(INetworkAddress address)
    {
        return
            (m_clientInfos[0] != null && m_clientInfos[0].Address.Equals(address)) ||
            (m_clientInfos[1] != null && m_clientInfos[1].Address.Equals(address));

    }

    public ClientInfo GetClientInfoByAddress(INetworkAddress address)
    {
        if (m_clientInfos[0] != null && m_clientInfos[0].Address.Equals(address))
            return m_clientInfos[0];

        if (m_clientInfos[1] != null && m_clientInfos[1].Address.Equals(address))
            return m_clientInfos[1];

        return null;
    }

    public int GetClientCount()
    {
        if (m_clientInfos[1] != null)
            return 2;

        if (m_clientInfos[0] != null)
            return 1;

        return 0;
    }

    public virtual void Update()
    {
        while (true)
        {
            int size;
            INetworkAddress address;
            if (!m_com.Receive(m_data, out size, out address))
                break;

            var msg = m_netMsgSerializer.Deserialize(m_data, size);

            m_state.ProcessMessage(this, msg, address);
        }

        m_state.Update(this);
    }

    public void AcceptClient(INetworkAddress address)
    {
        var joinAccept = new JoinAccept();
        m_netMsgSerializer.Serialize(joinAccept);
        m_com.Send(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize, address);
    }

    public void SendStartMatch()
    {
        Debug.Assert(m_clientInfos[0] != null && m_clientInfos[1] != null);

        var msg = new StartMatch();
        m_netMsgSerializer.Serialize(msg);

        m_com.Send(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize, m_clientInfos[0].Address);
        m_com.Send(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize, m_clientInfos[1].Address);
    }

    public void SendOpponentFound()
    {
        Debug.Assert(m_clientInfos[0] != null && m_clientInfos[1] != null);

        var msg = new OpponentFound();
        msg.m_playerName = m_clientInfos[1].Name;
        m_netMsgSerializer.Serialize(msg);
        m_com.Send(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize, m_clientInfos[0].Address);

        msg = new OpponentFound();
        msg.m_playerName = m_clientInfos[0].Name;
        m_netMsgSerializer.Serialize(msg);
        m_com.Send(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize, m_clientInfos[1].Address);
    }

    public void SendMatchMessage(MatchMessage message)
    {
    }

    public void SetReadyToStart(INetworkAddress address)
    {
        var clientInfo = GetClientInfoByAddress(address);
        if (clientInfo == null)
            return;

        clientInfo.IsReadyToStart = true;

        if (ClientsReady())
            m_state = GameServerMatchState.Get();
    }

    public bool ClientsReady()
    {
        return
            (m_clientInfos[0] != null && m_clientInfos[0].IsReadyToStart &&
            (m_clientInfos[1] != null && m_clientInfos[1].IsReadyToStart));
    }

    public void NotifyPlayersConnected()
    {
        if (PlayersConnected != null)
            PlayersConnected();
    }
}
