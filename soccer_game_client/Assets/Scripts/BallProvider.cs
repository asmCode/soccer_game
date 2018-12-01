using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProvider
{
    public static IBall GetBall()
    {
        var ballView = GameObject.Find("Ball").GetComponent<BallView>();
        return new Ball(ballView);
    }
}
