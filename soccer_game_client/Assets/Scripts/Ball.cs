using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : IBall, ITransformable
{
    private BallView m_ballView;
    private IPlayer m_player;

    public ssg.SphereCollider Collider { get; private set; }

    public Ball(BallView ballView)
    {
        m_ballView = ballView;

        Collider = new ssg.SphereCollider();
        Collider.Radius = 0.5f;
        Collider.Trans = this;
    }

    public IPlayer GetPlayer()
    {
        return m_player;
    }

    public void SetPlayer(IPlayer player)
    {
        m_player = player;
    }

    public void ClearPlayer()
    {
        m_player = null;
    }

    public void EnablePhysics(bool enable)
    {
        m_ballView.EnablePhysics(enable);
    }

    public bool IsPhysicsEnabled()
    {
        return m_ballView.IsPhysicsEnbaled(); 
    }

    public Vector3 GetPosition()
    {
        return m_ballView.transform.position;
    }

    public void SetPosition(Vector3 position)
    {
        m_ballView.SetPosition(position);
    }

    public Vector3 GetVelocity()
    {
        return m_ballView.GetVelocity();
    }

    public void SetVelocity(Vector3 velocity)
    {
        m_ballView.SetVelocity(velocity);
    }

    public void SetRotation(Quaternion rotation)
    {
        throw new System.NotImplementedException();
    }

    public Quaternion GetRotation()
    {
        throw new System.NotImplementedException();
    }

    public Transform GetTransform()
    {
        return m_ballView.transform;
    }
}

