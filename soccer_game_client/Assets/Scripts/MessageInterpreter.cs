using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageInterpreter
{
    public void ProcessMessage(Match match, MatchMessage message)
    {
        switch (message.m_messageType)
        {
            case MessageType.PlayerMove:
                var playerMoveMsg = message.m_message as PlayerMove;
                var moveVector = PlayerDirectionVector.GetVector(playerMoveMsg.m_playerDirection) * GameSettings.PlayerSpeed;
                match.SetPlayerPosition(0, 0, match.GetPlayerPosition(0, 0) + moveVector * playerMoveMsg.m_dt, playerMoveMsg.m_playerDirection);
                break;

            case MessageType.PlayerAction:
                var playerActionMsg = message.m_message as Action;
                match.PlayerAction(playerActionMsg.m_team, playerActionMsg.m_duration);
                break;

            case MessageType.PlayerPosition:
                var playerPositionMsg = message.m_message as PlayerPosition;
                match.SetPlayerPosition(playerPositionMsg.m_team, playerPositionMsg.m_index, playerPositionMsg.m_position, playerPositionMsg.m_direction);
                break;

            case MessageType.BallPosition:
                var ballPositionMsg = message.m_message as BallPosition;
                match.SetBallPosition(ballPositionMsg.m_position);
                break;
        }
    }
}
