using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPosition
{
    public int m_clientMsgNum;
    public Vector3 m_position;
    public Vector3 m_velocity;

    public static BallPosition Create(int clientMsgNum, Vector3 position, Vector3 velocity)
    {
        return new BallPosition()
        {
            m_clientMsgNum = clientMsgNum,
            m_position = position,
            m_velocity = velocity
        };
    }
}
