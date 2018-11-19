using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootVelocity
{
    public static Vector3 GetVelocity(PlayerDirection direction, float actionDuration)
    {
        actionDuration = Mathf.Clamp(actionDuration, 0, 1);

        var velocity = PlayerDirectionVector.GetVector(direction);
        velocity.y = 0.7f;
        velocity *= 15.0f * actionDuration;

        return velocity;
    }
}
