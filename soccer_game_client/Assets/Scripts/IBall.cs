﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBall
{
    void EnablePhysics(bool enable);
    bool IsPhysicsEnabled();
    Vector3 GetPosition();
    void SetPosition(Vector3 position);

    Vector3 GetVelocity();
    void SetVelocity(Vector3 velocity);
}
