using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide
{
    public int m_clientMsgNum;
    public byte m_team;
    public byte m_index;
    public Vector2 m_position;
    public PlayerDirection m_direction;

    public static MatchMessage Create(
        int clientMsgNumber, byte team, byte index, Vector2 position, PlayerDirection direction)
    {
        var payload = new Slide()
        {
            m_clientMsgNum = clientMsgNumber,
            m_team = team,
            m_index = index,
            m_position = position,
            m_direction = direction,
        };

        var msg = new MatchMessage();
        msg.m_messageType = MessageType.Slide;
        msg.m_message = payload;

        return msg;
    }
}
