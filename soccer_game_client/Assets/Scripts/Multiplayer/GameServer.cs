using UnityEngine;
using Ssg.Core.Networking;

public class GameServer
{
    private byte[] m_data;
    private NetworkMessageSerializer m_netMsgSerializer = new NetworkMessageSerializer(new BinaryDataWriter(), new BinaryDataReader());
    private MessageQueue m_gameMsgQueue = new MessageQueue();

    public event System.Action<int> ClientConnected;

    private INetworkCommunication m_com;

    public GameServer()
    {
        m_data = new byte[256];
    }

    public void StartServer()
    {
        Debug.Log("Staring server.");

        m_com = new UdpCommunication(GameSettings.ServerDefaultPort);
        m_com.Initialize();
    }

    public void StopServer()
    {
        m_com.Close();
    }

    public virtual void Update()
    {
        while (true)
        {
            int size;
            INetworkAddress address;
            if (!m_com.Receive(m_data, out size, out address))
                return;

            var msg = m_netMsgSerializer.Deserialize(m_data, size);
            ProcessMessage(msg);
        }
    }

    public int GetClientCount()
    {
        return 0;
    }

    public void SendToAll(Message message)
    {
    }

    public void SendToClient(int clientId, Message message)
    {
    }

    public Message GetMessage()
    {
        return null;
    }

    protected void NotifyNewConnection(int connectionId)
    {
        Debug.Log("new connection");

        if (ClientConnected != null)
            ClientConnected(0);
    }

    public bool IsWaitingForPlayers()
    {
        return GetClientCount() != 2;
    }

    private void ProcessMessage(NetworkMessage netMsg)
    {
        Debug.LogFormat("Network message received from client: {0}", netMsg.m_type);
    }
}
