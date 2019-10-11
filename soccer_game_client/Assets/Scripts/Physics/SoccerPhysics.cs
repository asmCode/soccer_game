using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ssg.Physics
{
    public class SoccerPhysics : IPhysics
    {
        private float m_timer;
        private IBall m_ball;
        private const float FixedDeltaTime = 0.02f;

        private List<Collider> m_colliders = new List<Collider>();

        public SoccerPhysics(IBall ball)
        {
            m_ball = ball;
        }

        public void AddCollider(Collider collider)
        {
            m_colliders.Add(collider);
        }

        public void Update(List<PhysicsObject> objects, float dt)
        {
            //if (!m_ball.IsPhysicsEnabled())
            //    return;

            if (Input.GetKeyDown(KeyCode.F))
            {
                m_ball.SetVelocity(new Vector3(1, 1, 0).normalized * 10.0f);
            }

            m_timer += dt;

            while (m_timer >= FixedDeltaTime)
            {
                m_timer -= FixedDeltaTime;
             
                FixedUpdate(objects, FixedDeltaTime);
            }

            if (m_timer > 0.0)
            {
                FixedUpdate(objects, m_timer);
                m_timer = 0.0f;
            }
        }

        private void CalculateCollisions()
        {
            for (int i = 0; i < m_colliders.Count; i++)
            {
                for (int j = i; j < m_colliders.Count; j++)
                {
                    if (i == j)
                        continue;

                    var collisionResult = ssg.CollisionChecker.CheckCollision(m_colliders[i], m_colliders[j]);
                    if (collisionResult != null)
                    {
                        m_colliders[i].NotifyCollision(m_colliders[j]);
                        m_colliders[j].NotifyCollision(m_colliders[i]);
                    }
                }
            }
        }

        public void FixedUpdate(List<PhysicsObject> objects, float fixedDeltaTime)
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

            foreach (var po in objects)
            {
                po.Position += po.Velocity * fixedDeltaTime;
                po.Velocity = Vector3.MoveTowards(po.Velocity, Vector3.zero, po.Friction * fixedDeltaTime);
            }

            CalculateCollisions();
        }
    }
}
