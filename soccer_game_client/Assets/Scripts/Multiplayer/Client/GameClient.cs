using UnityEngine;
using Ssg.Core.Networking;

public class GameClient
{
    // private ClientCommunication 

    private MessageSerializer m_msgSerializer;
    private MessageQueue m_msgQueue = new MessageQueue();

    public event System.Action Connected;

    public GameClient()
    {
        m_msgSerializer = MessageSerializerFactory.Create();
        m_msgQueue = new MessageQueue();
    }

    public bool IsConnected()
    {
        return m_connection != null;
    }

    // public abstract Connection Connect();
    public virtual void Disconnect()
    {
        if (m_connection == null)
            return;

        m_connection.Close();
        m_connection = null;
    }

    public virtual void Update()
    {
        //if (m_connection == null)
        //{
        //    m_connection = Connect();
        //    if (m_connection != null)
        //        NotifyNewConnection();
        //}

        //if (m_connection == null)
        //    return;

        //GetMessagesFromSever();
    }

    private void GetMessagesFromSever()
    {
        if (m_connection == null)
            return;

        while (true)
        {
            var netMsg = m_connection.GetMessage();
            if (netMsg == null)
                break;

            // var msg = m_msgSerializer.Deserialize(netMsg.Data);
            // m_msgQueue.AddMessage(msg);
        }
    }

    public void Send(Message message)
    {
        if (m_connection == null)
            return;

        var networkMsg = CreateNetworkMessage(message);
        m_connection.Send(networkMsg);
    }

    public Message GetMessage()
    {
        if (m_msgQueue.Empty())
            return null;

        return m_msgQueue.Dequeue();
    }

    private NetworkMessage CreateNetworkMessage(Message message)
    {
        var networkMsg = new NetworkMessage();
        networkMsg.Data = m_msgSerializer.Serialize(message);
        return networkMsg;
    }

    protected void NotifyNewConnection()
    {
        Log.Message("Connected to the server.");

        if (Connected != null)
            Connected();
    }
}
