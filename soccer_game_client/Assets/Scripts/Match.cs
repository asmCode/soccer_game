using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match
{
    private Team[] m_teams = new Team[2];
    private Ball m_ball = new Ball();
    private MessageInterpreter m_messageInterpreter;
    private BallView m_ballView;
    
    public Team[] Teams
    {
        get { return m_teams; }
    }

    public Match(BallView ballView)
    {
        m_ballView = ballView;
        PlayerProps.Instance = new PlayerProps(0.5f);
        m_messageInterpreter = new MessageInterpreter();
    }

    public void SetPlayers(List<PlayerInitialData> players)
    {
        m_teams[0] = new Team();
        m_teams[1] = new Team();

        foreach (var data in players)
        {
            m_teams[data.Team].Players.Add(new Player(data.Team, data.Index, data.Position, data.Direction));
        }
    }

    public void PlayerAction(byte team, float duration)
    {
        if (m_teams[team].ActivePlayer == null)
            return;

        m_ball.ClearPlayer();
        m_ballView.SetVelocity(ShootVelocity.GetVelocity(m_teams[team].ActivePlayer.Direction, duration));
    }

    public Vector3 GetBallPosition()
    {
        if (m_ball.Player == null)
            return m_ballView.transform.position;

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
        m_teams[teamIndex].ActivePlayer = m_teams[teamIndex].Players[playerIndex];
    }
}
