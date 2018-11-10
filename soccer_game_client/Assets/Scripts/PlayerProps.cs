using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProps
{
    public static PlayerProps Instance;

    public Vector3 BallOffset
    {
        get;
        private set;
    }

    public PlayerProps(Vector3 ballOffset)
    {
        BallOffset = ballOffset;
    }
}
