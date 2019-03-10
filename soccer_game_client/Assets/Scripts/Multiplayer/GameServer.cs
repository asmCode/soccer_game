using UnityEngine;

public class GameServer
{
    private byte[] m_data;
    // TODO: poor design
    public NetworkMessageSerializer m_netMsgSerializer = new NetworkMessageSerializer(new BinaryDataWriter(), new BinaryDataReader());
    private MessageQueue m_gameMsgQueue = new MessageQueue();

    public event System.Action PlayersConnected;

    private IGameServerState m_state;
    private INetworkCommunication m_com;
    private INetworkAddress m_serverAddress;

    public ClientInfo[] ClientInfos
    {
        get;
        private set;
    }
    
    public GameServer(INetworkCommunication networkCommunication)
    {
        m_data = new byte[256];
        ClientInfos = new ClientInfo[2];
        m_com = networkCommunication;
    }

    public void StartServer()
    {
        Debug.Log("Staring server.");

        m_com.Initialize();

        m_state = WaitingForPlayers.Get();
    }

    public void StopServer()
    {
        m_com.Close();
    }

    public void AddClient(ClientInfo clientInfo)
    {
        if (ClientInfos[0] == null)
        {
            ClientInfos[0] = clientInfo;
        }
        else if (ClientInfos[1] == null)
        {
            ClientInfos[1] = clientInfo;
        }

        Debug.LogFormat("Added client, team={0}, addr={1}", clientInfo.Team, clientInfo.Address.ToString());
    }

    public bool IsClientConnected(INetworkAddress address)
    {
        return
            (ClientInfos[0] != null && ClientInfos[0].Address.Equals(address)) ||
            (ClientInfos[1] != null && ClientInfos[1].Address.Equals(address));
    }

    public ClientInfo GetClientInfoByAddress(INetworkAddress address)
    {
        if (ClientInfos[0] != null && ClientInfos[0].Address.Equals(address))
            return ClientInfos[0];

        if (ClientInfos[1] != null && ClientInfos[1].Address.Equals(address))
            return ClientInfos[1];

        return null;
    }

    public int GetClientCount()
    {
        if (ClientInfos[1] != null)
            return 2;

        if (ClientInfos[0] != null)
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
        joinAccept.m_team = (byte)(GetClientCount() - 1);
        m_netMsgSerializer.Serialize(joinAccept);
        m_com.Send(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize, address);
    }

    public void SendStartMatch()
    {
        Debug.Assert(ClientInfos[0] != null && ClientInfos[1] != null);

        var msg = new StartMatch();
        m_netMsgSerializer.Serialize(msg);

        m_com.Send(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize, ClientInfos[0].Address);
        m_com.Send(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize, ClientInfos[1].Address);
    }

    public void SendOpponentFound()
    {
        Debug.Assert(ClientInfos[0] != null && ClientInfos[1] != null);

        var msg = new OpponentFound();
        msg.m_playerName = ClientInfos[1].Name;
        m_netMsgSerializer.Serialize(msg);
        m_com.Send(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize, ClientInfos[0].Address);

        msg = new OpponentFound();
        msg.m_playerName = ClientInfos[0].Name;
        m_netMsgSerializer.Serialize(msg);
        m_com.Send(m_netMsgSerializer.Data, m_netMsgSerializer.DataSize, ClientInfos[1].Address);
    }

    public void SendToAll(byte[] data, int size)
    {
        m_com.Send(data, size, ClientInfos[0].Address);
        m_com.Send(data, size, ClientInfos[1].Address);
    }

    public void Send(byte[] data, int size, INetworkAddress address)
    {
        m_com.Send(data, size, address);
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
            (ClientInfos[0] != null && ClientInfos[0].IsReadyToStart &&
            (ClientInfos[1] != null && ClientInfos[1].IsReadyToStart));
    }

    public void NotifyPlayersConnected()
    {
        if (PlayersConnected != null)
            PlayersConnected();
    }

    public void AddMatchMessage(MatchMessage msg)
    {
        m_gameMsgQueue.AddMessage(msg);
    }

    public MatchMessage GetMatchMessage()
    {
        if (m_gameMsgQueue.Empty())
            return null;

        return m_gameMsgQueue.Dequeue();
    }
}
