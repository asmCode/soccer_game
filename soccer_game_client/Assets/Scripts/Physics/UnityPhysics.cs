﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ssg.Physics
{
    public class UnityPhysics : IPhysics
    {
        private float m_timer;

        public void AddCollider(Collider collider)
        {
            throw new System.NotImplementedException();
        }

        public void Update(List<PhysicsObject> objects, float dt)
        {
            if (UnityEngine.Physics.autoSimulation)
                return;

            m_timer += dt;

            while (m_timer >= Time.fixedDeltaTime)
            {
                m_timer -= Time.fixedDeltaTime;
                UnityEngine.Physics.Simulate(Time.fixedDeltaTime);
            }
        }
    }
}
