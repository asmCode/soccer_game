﻿using System.Collections;
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
                match.SetPlayerPosition(
                    playerMoveMsg.m_team,
                    0,
                    match.GetPlayerPosition(playerMoveMsg.m_team, 0) + moveVector * playerMoveMsg.m_dt,
                    playerMoveMsg.m_playerDirection);
                break;

            case MessageType.PlayerAction:
                var playerActionMsg = message.m_message as Action;
                match.PlayerAction(playerActionMsg.m_team, playerActionMsg.m_duration);
                break;

            case MessageType.Slide:
                var slide = message.m_message as Slide;
                match.SetPlayerPosition(slide.m_team, slide.m_index, new Vector3(slide.m_position.x, 0.0f, slide.m_position.y), slide.m_direction);
                match.Slide(slide.m_team, slide.m_index);
                break;

            case MessageType.PlayerPosition:
                var playerPositionMsg = message.m_message as PlayerPosition;
                match.SetPlayerPosition(playerPositionMsg.m_team, playerPositionMsg.m_index, playerPositionMsg.m_position, playerPositionMsg.m_direction);
                break;

            case MessageType.BallPosition:
                var ballPositionMsg = message.m_message as BallPosition;
                match.SetBallPosition(ballPositionMsg.m_position, ballPositionMsg.m_velocity);
                break;
        }
    }
}
