using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProvider
{
    public static Ball GetBall()
    {
        return GameObject.Find("Ball").GetComponent<Ball>();
    }
}
