using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : IPlayer
{
    private PlayerView m_playerView;
    private PlayerDirection m_direction;
    public ssg.CapsuleCollider BallTakeoverCollider { get; private set; }

    public byte Team { get; set; }
    public byte Index { get; set; }

    public float SlideTime { get; set; }
    public Vector3 SlideBasePos { get; set; }

    public PhysicsObject PhysicsObject { get; set; }

    public PlayerState State { get; set; }

    public Player(PlayerView playerView, byte team, byte index, PlayerDirection playerDirection, ssg.CapsuleCollider ballTakeoverCollider)
    {
        m_playerView = playerView;
        Team = team;
        Index = index;
        m_direction = playerDirection;
        BallTakeoverCollider = ballTakeoverCollider;
        BallTakeoverCollider.Trans = this;

        SetDirection(playerDirection);
        PhysicsObject = new PhysicsObject(this);
        PhysicsObject.Friction = 40.0f;

        BallTakeoverCollider.Collision += BallTakeoverCollider_Collision;

        SetIdle();
    }

    private void BallTakeoverCollider_Collision(ssg.Collider otherCollider)
    {
        Debug.Log("collision against: " + otherCollider.Trans.GetTransform().name);
    }

    public void Update(float deltaTime)
    {
        State.Update(this, deltaTime);
    }

    public void SetPosition(Vector3 position)
    {
        m_playerView.transform.position = position;
    }

    public Vector3 GetPosition()
    {
        return m_playerView.transform.position;
    }

    public Transform GetTransform()
    {
        return m_playerView.transform;
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
        State.Slide(this);
    }

    public void SetSlideState()
    {
        SetState(PlayerStateSlide.Get());
    }

    private void SetState(PlayerState newState)
    {
        if (State != null)
            State.Leave(this);

        State = newState;

        State.Enter(this);
    }

    public Quaternion GetRotation()
    {
        return m_playerView.transform.rotation;
    }

    public void SetRotation(Quaternion rotation)
    {
        m_playerView.transform.rotation = rotation;
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
