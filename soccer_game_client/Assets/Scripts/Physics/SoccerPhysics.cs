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
        private BallPhysics m_ballPhysics = new BallPhysics();

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
                m_ball.SetVelocity(new Vector3(0, 1.0f, 1).normalized * 15.0f);
                m_ball.EnablePhysics(true);
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
                // First check if the existing collisions still occure.
                var currentColliders = new HashSet<Collider>(m_colliders[i].CurrentColliders);
                foreach (var collider in currentColliders)
                {
                    var collisionResult = ssg.CollisionChecker.CheckCollision(m_colliders[i], collider);
                    if (collisionResult == null)
                    {
                        m_colliders[i].NotifyCollisionLeave(collider);
                    }
                }

                for (int j = i; j < m_colliders.Count; j++)
                {
                    if (i == j)
                        continue;

                    if (m_colliders[i].CurrentColliders.Contains(m_colliders[j]))
                        continue;

                    var collisionResult = ssg.CollisionChecker.CheckCollision(m_colliders[i], m_colliders[j]);
                    if (collisionResult != null)
                    {
                        m_colliders[i].NotifyCollisionEnter(m_colliders[j]);
                        m_colliders[j].NotifyCollisionEnter(m_colliders[i]);
                    }
                }
            }
        }

        public void FixedUpdate(List<PhysicsObject> objects, float fixedDeltaTime)
        {
            if (m_ball.IsPhysicsEnabled())
                m_ballPhysics.Update((Ball)m_ball, fixedDeltaTime);

            foreach (var po in objects)
            {
                po.Position += po.Velocity * fixedDeltaTime;
                po.Velocity = Vector3.MoveTowards(po.Velocity, Vector3.zero, po.Friction * fixedDeltaTime);
            }

            CalculateCollisions();
        }
    }
}
