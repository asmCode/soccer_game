using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match
{
    private Team[] m_teams = new Team[2];
    private Ball m_ball = new Ball();
    private MessageInterpreter m_messageInterpreter;

    public Team[] Teams
    {
        get { return m_teams; }
    }

    public Match()
    {
        PlayerProps.Instance = new PlayerProps(0.5f);

        m_messageInterpreter = new MessageInterpreter();
    }

    public void SetPlayerInitialData(List<PlayerInitialData> players)
    {
        m_teams[0] = new Team();
        m_teams[1] = new Team();

        foreach (var data in players)
        {
            m_teams[data.Team].Players.Add(new Player(data.Team, data.Index, data.Position, data.Direction));
        }
    }

    public Vector3 GetBallPosition()
    {
        if (m_ball.Player == null)
            return Vector3.zero;

        return m_ball.Player.Position + m_ball.Player.GetDirectionVector() * PlayerProps.Instance.BallDistance;
    }

    public Vector3 GetPlayerPosition(byte team, byte playerIndex)
    {
        return m_teams[team].Players[playerIndex].Position;
    }

    public Player GetPlayer(byte team, byte playerIndex)
    {
        return m_teams[team].Players[playerIndex];
    }

    public void SetPlayerPosition(byte team, byte playerIndex, Vector3 position, PlayerDirection direction)
    {
        var player = m_teams[team].Players[playerIndex];
        player.Position = position;
        player.Direction = direction;
    }

    public void AttachBallToPlayer()
    {

    }

    public void ProcessMessage(Message message)
    {
        m_messageInterpreter.ProcessMessage(this, message);
    }

    public void NotifyPlayerBallCollision(byte teamIndex, byte playerIndex)
    {
        m_ball.SetPlayer(m_teams[teamIndex].Players[playerIndex]);
    }
}
