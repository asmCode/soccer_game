using UnityEngine;
using Ssg.Core.Networking;

public abstract class GameServer
{
    private Connection m_con1;
    private Connection m_con2;
    private MessageSerializer m_msgSerializer;
    private MessageQueue m_msgQueue = new MessageQueue();

    public event System.Action<int> ClientConnected;

    public GameServer()
    {
        m_msgSerializer = MessageSerializerFactory.Create();
        m_msgQueue = new MessageQueue();
    }

    public abstract void StartServer();
    public abstract void StopServer();
    public abstract Connection CheckNewConnections();

    public virtual void Update()
    {
        if (GetClientCount() != 2)
        {
            var connection = CheckNewConnections();
            if (connection != null)
                NotifyNewConnection(connection);
        }

        GetMessagesFromClient(m_con1);
        GetMessagesFromClient(m_con2);
    }

    private void GetMessagesFromClient(Connection connection)
    {
        if (connection != null)
        {
            while (true)
            {
                var netMsg = connection.GetMessage();
                if (netMsg == null)
                    break;

                var msg = m_msgSerializer.Deserialize(netMsg.Data);
                m_msgQueue.AddMessage(msg);
            }
        }
    }

    public int GetClientCount()
    {
        int clients = 0;

        if (m_con1 != null)
            clients++;

        if (m_con2 != null)
            clients++;

        return clients;
    }

    public void SendToAll(Message message)
    {
        var networkMsg = CreateNetworkMessage(message);

        if (m_con1 != null)
            m_con1.Send(networkMsg);

        if (m_con2 != null)
            m_con2.Send(networkMsg);
    }

    public void SendToClient(int clientId, Message message)
    {
        var networkMsg = CreateNetworkMessage(message);
        if (clientId == 1)
            m_con1.Send(networkMsg);
        else if (clientId == 2)
            m_con2.Send(networkMsg);
    }

    public Message GetMessage()
    {
        if (m_msgQueue.Empty())
            return null;

        return m_msgQueue.Dequeue();
    }

    private Ssg.Core.Networking.Message CreateNetworkMessage(Message message)
    {
        var networkMsg = new Ssg.Core.Networking.Message();
        networkMsg.Data = m_msgSerializer.Serialize(message);
        return networkMsg;
    }

    protected void NotifyNewConnection(Connection connection)
    {
        if (m_con1 == null)
            m_con1 = connection;
        else if (m_con2 == null)
            m_con2 = connection;

        Debug.Log("new connection");

        if (ClientConnected != null)
            ClientConnected(0);
    }
}
