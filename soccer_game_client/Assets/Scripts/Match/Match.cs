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

    public Team[] Teams
    {
        get { return m_teams; }
    }

    public Match()
    {
        m_ball = BallProvider.GetBall();
        m_messageInterpreter = new MessageInterpreter();

        m_physics = new ssg.Physics.SoccerPhysics(m_ball);

        var playersProvider = new PlayersProvider();
        var players = playersProvider.GetPlayers();
        SetPlayers(players);
    }

    public void SetLogic(IMatchLogic logic)
    {
        m_logic = logic;
    }

    public void Update(float dt)
    {
        var ballPlayer = m_ball.GetPlayer();
        if (ballPlayer != null)
        {
            var ballPos = ballPlayer.GetPosition() + ballPlayer.GetDirectionVector() * PlayerProps.Instance.BallDistance;
            m_ball.SetPosition(ballPos);
        }

        m_physics.Update(m_physiscObjects, dt);

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
    public void Idle (byte team, byte playerIndex)
    {
        var player = m_teams[team].Players[playerIndex];
        if (!(player.State is PlayerStateIdle))
            player.SetIdle();
    }

    public void SetPlayers(List<Player> players)
    {
        m_teams[0] = new Team();
        m_teams[1] = new Team();

        foreach (var player in players)
        {
            m_teams[player.Team].Players.Add(player);
            m_physiscObjects.Add(player.PhysicsObject);
        }
    }

    public void PlayerAction(byte team, float duration)
    {
        var activePlayer = m_ball.GetPlayer();

        if (activePlayer == null)
        {
            activePlayer = Teams[team].Players[0];
        }

        if (activePlayer.State != PlayerStateSlide.Get())
            activePlayer.Slide();
        return;

        m_ball.ClearPlayer();
        m_ball.EnablePhysics(true);
        m_ball.SetVelocity(ShootVelocity.GetVelocity(activePlayer.GetDirection(), duration));
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

    // Do I need this?
    // public void PlayerSlide(byte team, byte playerIndex, Vector3 position, PlayerDirection direction) {}

    public void AttachBallToPlayer(PlayerId playerId)
    {
        var player = GetPlayer(playerId);
        m_ball.SetPlayer(player);
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
