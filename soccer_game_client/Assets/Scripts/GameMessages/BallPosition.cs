using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPosition
{
    public Vector3 m_position;
    public Vector3 m_velocity;

    public static MatchMessage Create(Vector3 position)
    {
        var payload = new BallPosition();        
        payload.m_position = position;

        var msg = new MatchMessage();
        msg.m_messageType = MessageType.BallPosition;
        msg.m_message = payload;

        return msg;
    }
}
