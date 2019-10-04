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

    public PhysicsObject PhysicsObject { get; set; }

    public PlayerState State { get; set; }

    public Player(PlayerView playerView, byte team, byte index, PlayerDirection playerDirection)
    {
        m_playerView = playerView;
        Team = team;
        Index = index;
        m_direction = playerDirection;

        SetDirection(playerDirection);
        PhysicsObject = new PhysicsObject();
        PhysicsObject.Position = m_playerView.transform.position;
        PhysicsObject.Rotation = m_playerView.transform.rotation;
        PhysicsObject.Friction = 20.0f;

        SetIdle();
    }

    public void Update(float deltaTime)
    {
        ApplyPhysics();

        State.Update(this, deltaTime);
    }

    private void ApplyPhysics()
    {
        m_playerView.transform.position = PhysicsObject.Position;
        m_playerView.transform.rotation = PhysicsObject.Rotation;
    }

    public Vector3 GetPosition()
    {
        return m_playerView.transform.position;
    }

    public void SetPosition(Vector3 position)
    {
        PhysicsObject.Position = position;
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
    }

    public void StopRunning()
    {
        State.StopRunning(this);
    }

    public void Slide()
    {
        State = PlayerStateSlide.Get();
        State.Enter(this);
    }

    public Quaternion GetRotation()
    {
        throw new System.NotImplementedException();
    }

    public void SetRotation(Quaternion rotation)
    {
        throw new System.NotImplementedException();
    }

    public Vector3 GetVelocity()
    {
        throw new System.NotImplementedException();
    }

    public void SetVelocity(Vector3 velocity)
    {
        throw new System.NotImplementedException();
    }

    public float GetAngleVelocity()
    {
        throw new System.NotImplementedException();
    }

    public void SetAngleVelocity(float angleVelocity)
    {
        throw new System.NotImplementedException();
    }
}
