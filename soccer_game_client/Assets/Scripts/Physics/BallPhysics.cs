using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics
{
    private const float Gravity = -9.8f;
    private const float NoBounceTreshold = 0.2f;
    private const float StopVelocityTreshold = 1.0f;
    
    public void Update(Ball ball, float dt)
    {
        var velocity = ball.GetVelocity();
        var position = ball.GetPosition();

        if (velocity.y <= NoBounceTreshold)
        {
            velocity.y = 0.0f;
        }
        else
        {
            velocity.y += Gravity * dt;
        }

        if (velocity.magnitude <= StopVelocityTreshold)
        {
            velocity = Vector3.zero;
        }

        position += velocity * dt;

        if (position.y - ball.Radius < 0.0f)
        {
            position.y = ball.Radius;
            velocity.y = -velocity.y;
            velocity *= 0.8f;
        }

        ball.SetVelocity(velocity);
        ball.SetPosition(position);
    }
}
