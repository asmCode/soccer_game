using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public byte m_team;
    public float m_duration;

    public static Message Create(byte team, float duration)
    {
        var payload = new Action();
        payload.m_team = team;
        payload.m_duration = duration;

        var msg = new Message();
        msg.m_messageType = MessageType.PlayerAction;
        msg.m_message = payload;

        return msg;
    }
}
