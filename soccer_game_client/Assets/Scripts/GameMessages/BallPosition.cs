using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPosition
{
    public Vector3 m_position;
    public Vector3 m_velocity;

    public static BallPosition Create(Vector3 position, Vector3 velocity)
    {
        return new BallPosition()
        {
            m_position = position,
            m_velocity = velocity
        };
    }
}
