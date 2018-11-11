using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageInterpreter
{
    public void ProcessMessage(Match match, Message message)
    {
        switch (message.m_messageType)
        {
            case MessageType.PlayerMove:
                var playerMoveMsg = message.m_message as MovePlayer;
                var moveVector = PlayerDirectionVector.GetVector(playerMoveMsg.m_playerDirection) * GameSettings.PlayerSpeed;
                match.SetPlayerPosition(0, 0, match.GetPlayerPosition(0, 0) + moveVector * playerMoveMsg.m_dt, playerMoveMsg.m_playerDirection);
                break;
        }
    }
}
