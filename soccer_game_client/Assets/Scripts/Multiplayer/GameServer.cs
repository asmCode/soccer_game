using Ssg.Core.Networking;

public abstract class GameServer
{
    private Connection m_con1;
    private Connection m_con2;
    private MessageSerializer m_msgSerializer;
    private MessageQueue m_msgQueue = new MessageQueue();

    event System.Action<int> ClientConnected;

    public GameServer()
    {
        m_msgSerializer = MessageSerializerFactory.Create();
        m_msgQueue = new MessageQueue();
    }

    public abstract void StartServer();
    public abstract void StopServer();

    public void Update()
    {

    }

    public void SendToAll(Message message)
    {
        var networkMsg = CreateNetworkMessage(message);

        m_con1.Send(networkMsg);
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
}
