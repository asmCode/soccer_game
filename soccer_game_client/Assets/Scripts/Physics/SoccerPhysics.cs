using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ssg.Physics
{
    public class SoccerPhysics : IPhysics
    {
        private float m_timer;
        private IBall m_ball;
        private const float FixedDeltaTime = 0.01f;

        public SoccerPhysics(IBall ball)
        {
            m_ball = ball;
        }

        public void Update(float dt)
        {
            if (!m_ball.IsPhysicsEnabled())
                return;

            if (Input.GetKeyDown(KeyCode.F))
            {
                m_ball.SetVelocity(new Vector3(1, 1, 0).normalized * 10.0f);
            }

            m_timer += dt;

            while (m_timer >= FixedDeltaTime)
            {
                m_timer -= FixedDeltaTime;

                FixedUpdate(FixedDeltaTime);
            }

            //FixedUpdate(FixedDeltaTime - m_timer);
            //m_timer = 0.0f;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            var velocity = m_ball.GetVelocity();
            velocity += new Vector3(0, -9.8f, 0.0f) * fixedDeltaTime;

            var ballPos = m_ball.GetPosition();
            ballPos += velocity * fixedDeltaTime;

            if (ballPos.y <= 0.0f)
            {
                ballPos.y = 0.0f;
                velocity.y = -velocity.y;
            }

            m_ball.SetVelocity(velocity);
            m_ball.SetPosition(ballPos);
        }
    }
}
