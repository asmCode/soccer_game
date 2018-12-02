using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match
{
    private Team[] m_teams = new Team[2];
    private IBall m_ball;
    private MessageInterpreter m_messageInterpreter;
    private ssg.Physics.IPhysics m_physics = new ssg.Physics.UnityPhysics();

    public Team[] Teams
    {
        get { return m_teams; }
    }

    public Match()
    {
        m_ball = BallProvider.GetBall();
        PlayerProps.Instance = new PlayerProps(0.5f);
        m_messageInterpreter = new MessageInterpreter();
    }

    public void Update(float dt)
    {
        var ballPlayer = m_ball.GetPlayer();
        if (ballPlayer != null)
        {
            var ballPos = ballPlayer.GetPosition() + ballPlayer.GetDirectionVector() * PlayerProps.Instance.BallDistance;
            m_ball.SetPosition(ballPos);
        }

        m_physics.Update(dt);
    }

    public void SetPlayers(List<IPlayer> players)
    {
        m_teams[0] = new Team();
        m_teams[1] = new Team();

        foreach (var data in players)
            m_teams[data.Team].Players.Add(data);
    }

    public void PlayerAction(byte team, float duration)
    {
        if (m_teams[team].ActivePlayer == null)
            return;

        m_ball.ClearPlayer();
        m_ball.EnablePhysics(true);
        m_ball.SetVelocity(ShootVelocity.GetVelocity(m_teams[team].ActivePlayer.GetDirection(), duration));
    }

    public Vector3 GetBallPosition()
    {
        return m_ball.GetPosition();
    }

    public Vector3 GetPlayerPosition(byte team, byte playerIndex)
    {
        return m_teams[team].Players[playerIndex].GetPosition();
    }

    public IPlayer GetPlayer(byte team, byte playerIndex)
    {
        return m_teams[team].Players[playerIndex];
    }

    public void SetPlayerPosition(byte team, byte playerIndex, Vector3 position, PlayerDirection direction)
    {
        var player = m_teams[team].Players[playerIndex];
        player.SetPosition(position);
        player.SetDirection(direction);
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
        m_ball.EnablePhysics(false);
        m_ball.SetPlayer(m_teams[teamIndex].Players[playerIndex]);
        m_teams[teamIndex].ActivePlayer = m_teams[teamIndex].Players[playerIndex];
    }

    public List<IPlayer> GetPlayers()
    {
        var players = new List<IPlayer>();

        players.AddRange(m_teams[0].Players);
        players.AddRange(m_teams[1].Players);

        return players;
    }
}
