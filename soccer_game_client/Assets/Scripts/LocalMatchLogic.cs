using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMatchLogic : IMatchLogic
{
    private Match m_match;

    public LocalMatchLogic(Match match)
    {
        m_match = match;
    }

    public void BallAndPlayerCollision(PlayerId playerId)
    {
        var ball = m_match.GetBall();
        ball.EnablePhysics(false);
        ball.SetPlayer(m_match.GetPlayer(playerId));
    }
}
