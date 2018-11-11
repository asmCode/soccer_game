using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProps
{
    public static PlayerProps Instance;

    public float BallDistance
    {
        get;
        private set;
    }

    public PlayerProps(float ballDistance)
    {
        BallDistance = ballDistance;
    }
}
