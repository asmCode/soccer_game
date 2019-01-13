using UnityEngine;
using Ssg.Core.Networking;

public class GameServer
{
    private NetworkMessageSerializer m_msgSerializer = new NetworkMessageSerializer(null, null);
    private MessageQueue m_gameMsgQueue = new MessageQueue();

    public event System.Action<int> ClientConnected;

    // private UdpGameServer m_serverCommunication = new UdpGameServer(GameSettings.ServerDefaultPort);

    public void StartServer()
    {
        Debug.Log("Staring server.");
        //m_serverCommunication.StartServer();
    }

    public void StopServer()
    {
        //m_serverCommunication.StopServer();
    }

    public virtual void Update()
    {
        //while (true)
        //{
        //    if (!m_serverCommunication.ReceiveData())
        //        break;

        //    var msg = m_msgSerializer.Deserialize(m_serverCommunication.ReceivedData);
        //    ProcessMessage(msg);
        //}
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
        //Debug.LogFormat("Network message received from client: {0}", netMsg.);
    }
}
