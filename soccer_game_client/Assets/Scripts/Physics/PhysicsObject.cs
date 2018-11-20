using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicsObject
{
    public abstract Vector3 GetPosition();
    public abstract void SetPosition(Vector3 position);

    public abstract Quaternion GetRotation();
    public abstract void SetRotation(Quaternion rotation);

    public abstract Vector3 GetVelocity();
    public abstract void SetVelocity(Vector3 velocity);

    public abstract float GetAngleVelocity();
    public abstract void SetAngleVelocity(float angleVelocity);
}
