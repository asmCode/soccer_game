using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchView : MonoBehaviour
{
    private Match m_match;
    public PlayerView m_playerView;

    public void Init(Match match)
    {
        m_match = match;
    }

    private void Update()
    {
        if (m_match == null)
            return;

        m_playerView.Position = m_match.GetPlayerPosition(0, 0);
    }
}
