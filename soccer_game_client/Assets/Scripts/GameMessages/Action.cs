using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public byte m_team;
    public byte m_playerIndex;
    public float m_duration;

    public static MatchMessage Create(byte team, byte playerIndex, float duration)
    {
        var payload = new Action();
        payload.m_team = team;
        payload.m_playerIndex = playerIndex;
        payload.m_duration = duration;

        var msg = new MatchMessage();
        msg.m_messageType = MessageType.PlayerAction;
        msg.m_message = payload;

        return msg;
    }
}
