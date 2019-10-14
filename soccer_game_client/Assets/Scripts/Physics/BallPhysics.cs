using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics
{
    private const float Gravity = -9.8f;
    private const float NoBouncePositionTreshold = 0.01f;
    private const float NoBounceVelocityTreshold = 0.1f;
    private const float StopVelocityTreshold = 0.2f;
    private const float GrassFriction = 2.0f;
    private const float AirFriction = 0.02f;
    private const float BounceVelocityMultiplierY = 0.65f;
    private const float BounceVelocityMultiplierXZ = 0.9f;

    public void Update(Ball ball, float dt)
    {
        var velocity = ball.GetVelocity();
        var position = ball.GetPosition();

        float ballBottom = position.y - ball.Radius;

        bool grounded =
            ballBottom < NoBouncePositionTreshold &&
            Mathf.Abs(velocity.y) < NoBounceVelocityTreshold;

        if (grounded)
        {
            velocity.y = 0.0f;
            position.y = ball.Radius;

            velocity = Vector3.MoveTowards(velocity, Vector3.zero, GrassFriction * dt);
        }
        else
        {
            velocity.y += Gravity * dt;
            velocity *= 1.0f - (AirFriction * dt);
        }

        if (velocity.magnitude <= StopVelocityTreshold && grounded)
        {
            velocity = Vector3.zero;
        }

        position += velocity * dt;

        float newBallBottom = position.y - ball.Radius;

        if (newBallBottom < 0.0f)
        {
            position.y = ball.Radius;
            velocity.y = -velocity.y;
            velocity.y *= BounceVelocityMultiplierY;
            velocity.x *= BounceVelocityMultiplierXZ;
            velocity.z *= BounceVelocityMultiplierXZ;
        }

        ball.SetVelocity(velocity);
        ball.SetPosition(position);
    }
}
