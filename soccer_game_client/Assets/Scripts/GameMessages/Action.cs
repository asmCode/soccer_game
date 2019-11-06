using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public int m_messageNumber;
    public byte m_team;
    public byte m_playerIndex;
    public float m_duration;

    // team is ignored when sending from the client to the server.
    public static MatchMessage Create(int messageNumber, byte team, byte playerIndex, float duration)
    {
        var payload = new Action();
        payload.m_messageNumber = messageNumber;
        payload.m_team = team;
        payload.m_playerIndex = playerIndex;
        payload.m_duration = duration;

        var msg = new MatchMessage();
        msg.m_messageType = MessageType.PlayerAction;
        msg.m_message = payload;

        return msg;
    }
}
