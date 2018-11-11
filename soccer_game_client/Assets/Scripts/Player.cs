using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Player(byte team, byte index, Vector3 position, PlayerDirection playerDirection)
    {
        Team = team;
        Index = index;
        Position = position;
        Direction = playerDirection;
    }

    public byte Team { get; set; }
    public byte Index { get; set; }
    public Vector3 Position { get; set; }
    public PlayerDirection Direction { get; set; }

    public Vector3 GetDirectionVector()
    {
        return PlayerDirectionVector.GetVector(Direction);
    }
}
