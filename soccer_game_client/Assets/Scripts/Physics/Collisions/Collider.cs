using System.Collections.Generic;
using UnityEngine;

namespace ssg
{
    public abstract class Collider
    {
        public event CollisionDelegate CollisionEnter;
        public event CollisionDelegate CollisionLeave;
        public ITransformable Trans;
        public HashSet<Collider> CurrentColliders = new HashSet<Collider>();

        public abstract ColliderId ColliderId { get; }

        public void NotifyCollisionEnter(Collider otherCollider)
        {
            CurrentColliders.Add(otherCollider);

            if (CollisionEnter != null)
                CollisionEnter(otherCollider);
        }

        public void NotifyCollisionLeave(Collider otherCollider)
        {
            CurrentColliders.Remove(otherCollider);

            if (CollisionLeave != null)
                CollisionLeave(otherCollider);
        }
    }
}
