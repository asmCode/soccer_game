using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Match m_match;
    private PlayerId m_playerId;

    public PlayerId PlayerId
    {
        get { return m_playerId; }
    }

    public void Init(Match match, byte teamIndex, byte playerIndex)
    {
        m_match = match;
        m_playerId.Team = teamIndex;
        m_playerId.Index = playerIndex;
    }

    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public Vector3 Direction
    {
        set { transform.forward = value; }
    }

    public void TriggerBallCollision()
    {
        m_match.NotifyPlayerBallCollision(PlayerId);
    }
}
