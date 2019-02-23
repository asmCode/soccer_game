using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove
{
    public float m_dt;
    public byte m_team;
    public byte m_playerIndex;
    public PlayerDirection m_playerDirection;

    public static MatchMessage Create(float dt, byte team, byte playerIndex, PlayerDirection direction)
    {
        var payload = new PlayerMove();
        payload.m_dt = dt;
        payload.m_team = team;
        payload.m_playerIndex = playerIndex;
        payload.m_playerDirection = direction;

        var msg = new MatchMessage();
        msg.m_messageType = MessageType.PlayerMove;
        msg.m_message = payload;

        return msg;
    }
}
