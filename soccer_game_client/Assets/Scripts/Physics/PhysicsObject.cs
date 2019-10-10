using UnityEngine;

public class PhysicsObject
{
    private ITransformable m_trans;

    public PhysicsObject(ITransformable trans)
    {
        m_trans = trans;
    }

    public Vector3 Position
    {
        get { return m_trans.GetPosition(); }
        set { m_trans.SetPosition(value); }
    }

    public Quaternion Rotation
    {
        get { return m_trans.GetRotation(); }
        set { m_trans.SetRotation(value); }
    }

    public Vector3 Velocity;
    public float Friction;
    public float AngularVelocity;
}
