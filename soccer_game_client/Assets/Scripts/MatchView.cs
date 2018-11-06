using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchView : MonoBehaviour
{
    private Match m_match;
    public List<PlayerView>[] m_teams = new List<PlayerView>[2];
    public Transform m_teamsContainter;
    public BallView m_ballView;

    public void Init(Match match)
    {
        m_match = match;

        InitTeams();
    }

    private void Update()
    {
        if (m_match == null)
            return;

        m_teams[0][0].Position = m_match.GetPlayerPosition(0, 0);


        m_ballView.transform.position = m_match.GetBallPosition();
    }

    private void InitTeams()
    {
        m_teams[0] = new List<PlayerView>();
        GetPlayers(m_teams[0], m_teamsContainter.Find("Team1"));
        InitPlayers(0, m_teams[0]);

        m_teams[1] = new List<PlayerView>();
        GetPlayers(m_teams[1], m_teamsContainter.Find("Team2"));
        InitPlayers(1, m_teams[1]);
    }

    private void GetPlayers(List<PlayerView> team, Transform teamContainer)
    {
        for (int i = 0; i < teamContainer.childCount; i++)
        {
            var child = teamContainer.GetChild(i);
            var playerView = child.GetComponent<PlayerView>();
            team.Add(playerView);
        }
    }

    private void InitPlayers(byte teamIndex, List<PlayerView> players)
    {
        for (int i = 0; i < players.Count; i++)
            players[i].Init(m_match, teamIndex, (byte)i);
    }
}
