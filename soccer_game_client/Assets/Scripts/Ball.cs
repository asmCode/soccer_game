using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : IBall, ITransformable
{
    private BallView m_ballView;

    public ssg.SphereCollider Collider { get; private set; }

    public Ball(BallView ballView)
    {
        m_ballView = ballView;

        Collider = new ssg.SphereCollider();
        Collider.Radius = Radius;
        Collider.Trans = this;
        Collider.Tag = (int)ObjectId.Ball;
    }

    public float Radius { get { return 0.11f; } }

    private bool m_isPhysicsEnabled = true;

    public void EnablePhysics(bool enable)
    {
        // m_ballView.EnablePhysics(enable);

        m_isPhysicsEnabled = enable;
    }

    public bool IsPhysicsEnabled()
    {
        // return m_ballView.IsPhysicsEnbaled();

        return m_isPhysicsEnabled;
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

