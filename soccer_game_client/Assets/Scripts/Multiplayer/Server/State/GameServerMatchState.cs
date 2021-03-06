﻿using UnityEngine;

public class GameServerMatchState : IGameServerState
{
    private static GameServerMatchState m_instance;

    public static GameServerMatchState Get()
    {
        if (m_instance == null)
            m_instance = new GameServerMatchState();

        return m_instance;
    }

    public void ProcessMessage(GameServer gameServer, NetworkMessage netMsg, INetworkAddress address)
    {
        if (!gameServer.IsClientConnected(address))
        {
            Debug.LogFormat("Received the message {0} from not connected client {1}", netMsg.m_type, address.ToString());
            return;
        }

        var clientInfo = gameServer.GetClientInfoByAddress(address);

        switch (netMsg.m_type)
        {
            case NetworkMessageType.PlayerMove:
                {
                    var playerMove = netMsg.m_msg as PlayerMove;
                    playerMove.m_team = clientInfo.Team;
                    MatchMessage matchMsg = new MatchMessage();
                    matchMsg.m_message = playerMove;
                    matchMsg.m_messageType = MessageType.PlayerMove;
                    gameServer.AddMatchMessage(matchMsg);

                    // Not sure it it supposed to be here.
                    clientInfo.LastMsgNum = Mathf.Max(clientInfo.LastMsgNum, playerMove.m_messageNumber);
                    break;
                }

            case NetworkMessageType.PlayerAction:
                {
                    var msg = netMsg.m_msg as Action;
                    msg.m_team = clientInfo.Team;
                    MatchMessage matchMsg = new MatchMessage();
                    matchMsg.m_message = msg;
                    matchMsg.m_messageType = MessageType.PlayerAction;
                    gameServer.AddMatchMessage(matchMsg);

                    // Not sure it it supposed to be here.
                    clientInfo.LastMsgNum = Mathf.Max(clientInfo.LastMsgNum, msg.m_messageNumber);
                    break;
                }
        }
    }

    public void Update(GameServer gameServer)
    {
    }
}
