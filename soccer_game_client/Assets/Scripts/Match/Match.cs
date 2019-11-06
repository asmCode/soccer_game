using System.Collections.Generic;
using UnityEngine;

public class Match
{
    private Team[] m_teams = new Team[2];
    private IBall m_ball;
    private MessageInterpreter m_messageInterpreter;
    private ssg.Physics.IPhysics m_physics;
    private IMatchLogic m_logic;
    private List<PhysicsObject> m_physiscObjects = new List<PhysicsObject>();
    private PlayerId m_playerIdWithBall = PlayerId.None;

    public Team[] Teams
    {
        get { return m_teams; }
    }

    public Match()
    {
        m_ball = BallProvider.GetBall();
        m_messageInterpreter = new MessageInterpreter();

        m_physics = new ssg.Physics.SoccerPhysics(m_ball);
        m_physics.AddCollider(((Ball)m_ball).Collider);

        var playersProvider = new PlayersProvider();
        var players = playersProvider.GetPlayers(this);
        SetPlayers(players);
    }

    public void SetLogic(IMatchLogic logic)
    {
        m_logic = logic;
    }

    public void Update(float dt)
    {
        m_physics.Update(m_physiscObjects, dt);

        if (m_playerIdWithBall != PlayerId.None)
        {
            var ballPlayer = GetPlayer(m_playerIdWithBall);
            var ballPos = ballPlayer.GetPosition() + ballPlayer.GetDirectionVector() * PlayerProps.Instance.BallDistance;
            ballPos.y += ((Ball)m_ball).Radius;
            m_ball.SetPosition(ballPos);
        }

        foreach (var team in m_teams)
        {
            foreach (var player in team.Players)
            {
                player.Update(dt);
            }
        }
    }

    public void Run(byte team, byte playerIndex, PlayerDirection direction, float deltaTime)
    {
        m_teams[team].Players[playerIndex].Run(direction, deltaTime);
    }
    public void StopRunning(byte team, byte playerIndex)
    {
        (m_teams[team].Players[playerIndex] as Player).StopRunning();
    }

    public void Idle(byte team, byte playerIndex)
    {
        var player = m_teams[team].Players[playerIndex];
        if (!(player.State is PlayerStateIdle))
            player.SetIdle();
    }

    public void SetBallOwner(PlayerId playerId)
    {
        m_playerIdWithBall = playerId;
    }

    public void SetPlayers(List<Player> players)
    {
        m_teams[0] = new Team();
        m_teams[1] = new Team();

        foreach (var player in players)
        {
            m_teams[player.Team].Players.Add(player);
            m_physiscObjects.Add(player.PhysicsObject);
            m_physics.AddCollider(player.BallTakeoverCollider);
        }
    }

    public Player GetActivePlayer(byte team)
    {
        return (Player)m_teams[team].Players[m_teams[team].ActivePlayerIndex];
    }

    public void Slide(byte team, byte playerIndex)
    {
        var player = GetPlayer(new PlayerId(team, playerIndex));
        player.Slide();
    }

    public void PlayerAction(byte team, float duration)
    {
        var player = GetActivePlayer(team);
        if (player == null)
            return;

        if (player.PlayerId == m_playerIdWithBall)
        {
            var velocity = player.GetDirectionVector();
            velocity.y = 0.5f;
            velocity *= 20.0f;
            KickBall(velocity);
        }
        else
            player.Slide();
    }

    private void KickBall(Vector3 velocity)
    {
        m_playerIdWithBall = PlayerId.None;
        m_ball.EnablePhysics(true);
        m_ball.SetVelocity(velocity);
    }

    public void SetBallPosition(Vector3 position, Vector3 velocity)
    {
        m_ball.SetPosition(position);
        m_ball.SetVelocity(velocity);
    }

    public Vector3 GetBallPosition()
    {
        return m_ball.GetPosition();
    }

    public Vector3 GetPlayerPosition(byte team, byte playerIndex)
    {
        return m_teams[team].Players[playerIndex].GetPosition();
    }

    public IPlayer GetPlayer(PlayerId playerId)
    {
        return m_teams[playerId.Team].Players[playerId.Index];
    }

    public void SetPlayerPosition(byte team, byte playerIndex, Vector3 position, PlayerDirection direction)
    {
        var player = m_teams[team].Players[playerIndex];
        player.SetPosition(position);
        player.SetDirection(direction);
    }

    public void OffsetPlayerPosition(byte team, byte playerIndex, Vector3 offset, PlayerDirection direction)
    {
        var player = m_teams[team].Players[playerIndex];
        var position = player.GetPosition();
        player.SetPosition(position + offset);
        player.SetDirection(direction);
    }

    public void ProcessMessage(MatchMessage message)
    {
        m_messageInterpreter.ProcessMessage(this, message);
    }

    public void NotifyPlayerBallCollision(PlayerId playerId)
    {
        m_logic.BallAndPlayerCollision(playerId);
    }

    public IBall GetBall()
    {
        return m_ball;
    }

    // TODO: don't create list every time
    public List<IPlayer> GetPlayers()
    {
        var players = new List<IPlayer>();

        players.AddRange(m_teams[0].Players);
        players.AddRange(m_teams[1].Players);

        return players;
    }
}
