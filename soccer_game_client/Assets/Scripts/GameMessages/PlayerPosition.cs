using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition
{
    public int m_clientMsgNum;
    public byte m_team;
    public byte m_index;
    public Vector3 m_position;
    public PlayerDirection m_direction;

    public static PlayerPosition Create(int clientMsgNum, byte team, byte index, Vector3 position, PlayerDirection direction)
    {
        var payload = new PlayerPosition();
        payload.m_clientMsgNum = clientMsgNum;
        payload.m_team = team;
        payload.m_index = index;
        payload.m_position = position;
        payload.m_direction = direction;
        return payload;
    }
}
