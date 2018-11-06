using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Match m_match;

    public byte TeamIndex { get; private set; }
    public byte PlayerIndex { get; private set; }


    public void Init(Match match, byte teamIndex, byte playerIndex)
    {
        m_match = match;
        TeamIndex = teamIndex;
        PlayerIndex = playerIndex;
    }

    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public void TriggerBallCollision()
    {
        m_match.NotifyPlayerBallCollision(TeamIndex, PlayerIndex);
    }
}
