using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : IPlayer
{
    private PlayerView m_playerView;
    private PlayerDirection m_direction;

    public byte Team { get; set; }
    public byte Index { get; set; }

    public float SlideTime { get; set; }
    public Vector3 SlideBasePos { get; set; }

    public PlayerState State { get; set; }

    public Player(PlayerView playerView, byte team, byte index, PlayerDirection playerDirection)
    {
        m_playerView = playerView;
        Team = team;
        Index = index;
        m_direction = playerDirection;

        SetIdle();
        SetDirection(playerDirection);
    }

    public void Update(float deltaTime)
    {
        State.Update(this, deltaTime);
    }

    public Vector3 GetPosition()
    {
        return m_playerView.transform.position;
    }

    public void SetPosition(Vector3 position)
    {
        m_playerView.transform.position = position;
    }

    public void OffsetPosition(Vector3 offset)
    {
        m_playerView.transform.position = m_playerView.transform.position + offset;
    }

    public void Run(PlayerDirection playerDirection, float deltaTime)
    {
        State.Run(this, playerDirection, deltaTime);
    }

    public void SetDirection(PlayerDirection direction)
    {
        m_direction = direction;
        m_playerView.transform.forward = GetDirectionVector();
    }

    public PlayerDirection GetDirection()
    {
        return m_direction;
    }

    public Vector3 GetDirectionVector()
    {
        return PlayerDirectionVector.GetVector(m_direction);
    }

    public void PlayAnimation(PlayerAnimationType playerAnimationType)
    {

    }

    public void SetIdle()
    {
        State = PlayerStateIdle.Get();
        State.Enter(this);

        Debug.Log("set idle");
    }

    public void Slide()
    {
        State = PlayerStateSlide.Get();
        State.Enter(this);
    }
}
