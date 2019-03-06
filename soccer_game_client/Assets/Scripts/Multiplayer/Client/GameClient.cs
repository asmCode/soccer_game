using UnityEngine;

public class GameClient
{
    private byte[] m_data;
    private INetworkCommunication m_com;

    private MessageSerializer m_msgSerializer;
    // TODO: poor design
    public NetworkMessageSerializer m_netMsgSerializer = new NetworkMessageSerializer(new BinaryDataWriter(), new BinaryDataReader());
    private MessageQueue m_msgQueue = new MessageQueue();
    private INetworkAddress m_serverAddress;

    public event System.Action Connected;
    public event System.Action OpponentFound;
    public event System.Action MatchStarted;

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

    public void Send(byte[] data, int size)
    {
        m_com.Send(data, size, m_serverAddress);
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
                if (MatchStarted != null)
                    MatchStarted();
                break;

            case NetworkMessageType.PlayerPosition:
                {
                    var matchMsg = new MatchMessage();
                    matchMsg.m_messageType = MessageType.PlayerPosition;
                    matchMsg.m_message = msg.m_msg;
                    m_msgQueue.AddMessage(matchMsg);
                    break;
                }

            case NetworkMessageType.BallPosition:
                {
                    var matchMsg = new MatchMessage();
                    matchMsg.m_messageType = MessageType.BallPosition;
                    matchMsg.m_message = msg.m_msg;
                    m_msgQueue.AddMessage(matchMsg);
                    break;
                }
        }
    }
}
