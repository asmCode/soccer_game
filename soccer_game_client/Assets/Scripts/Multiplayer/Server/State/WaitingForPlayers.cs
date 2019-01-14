using UnityEngine;

public class WaitingForPlayers : IGameServerState
{
    private static WaitingForPlayers m_instance;

    public static WaitingForPlayers Get()
    {
        if (m_instance == null)
            m_instance = new WaitingForPlayers();

        return m_instance;
    }

    public void ProcessMessage(GameServer gameServer, NetworkMessage netMsg, INetworkAddress address)
    {
        switch (netMsg.m_type)
        {
            case NetworkMessageType.JoinRequest:
                ProcessJoinRequest(gameServer, netMsg.m_msg as JoinRequest, address);
                break;
        }
    }

    public void Update(GameServer gameServer)
    {
    }

    private void ProcessJoinRequest(GameServer gameServer, JoinRequest msg, INetworkAddress address)
    {
        Debug.LogFormat("Join request from player {0}. Address {1}", msg.m_playerName, address.ToString());

        if (gameServer.GetClientCount() == 2)
        {
            Debug.Log("Server is full.");
            return;
        }

        if (gameServer.IsClientConnected(address))
        {
            Debug.Log("Client is already connected.");
            return;
        }

        gameServer.AddClient(new ClientInfo(msg.m_playerName, address));
        gameServer.AcceptClient(address);
        gameServer.SendOpponentFound();

        // 
        if (gameServer.GetClientCount() == 2)
            gameServer.NotifyPlayersConnected();
    }
}
