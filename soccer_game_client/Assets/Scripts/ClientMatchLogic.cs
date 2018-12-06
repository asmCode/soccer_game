using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMatchLogic : IMatchLogic
{
    private Match m_match;

    public ClientMatchLogic(Match match)
    {
        m_match = match;
    }

    public void BallAndPlayerCollision(PlayerId playerId)
    {
        // Do nothing for now. Implement it when it comes to predictions.
    }
}
