using UnityEngine;
using Ssg.Core.Networking;

public abstract class GameServer
{
    private NetworkMessageSerializer m_msgSerializer = new NetworkMessageSerializerBinary();
    private MessageQueue m_gameMsgQueue = new MessageQueue();

    public event System.Action<int> ClientConnected;

    public abstract void StartServer();
    public abstract void StopServer();
    public abstract byte[] RecvMessage();

    public virtual void Update()
    {
        while (true)
        {
            var msgData = RecvMessage();
            if (msgData == null)
                break;

            var msg = m_msgSerializer.Deserialize(msgData);
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
        Debug.LogFormat("Network message received from client: {0}", netMsg.Type);
    }
}
