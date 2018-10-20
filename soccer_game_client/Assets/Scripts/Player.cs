using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Player(byte team, byte index, Vector3 position)
    {
        Team = team;
        Index = index;
        Position = position;
    }

    public byte Team { get; set; }
    public byte Index { get; set; }
    public Vector3 Position { get; set; }
    public PlayerDirection Direction { get; set; }
}
