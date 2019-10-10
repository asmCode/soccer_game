using UnityEngine;

namespace ssg
{
    public abstract class Collider
    {
        public event CollisionDelegate Collision;
        public ITransformable Trans;

        public abstract ColliderId ColliderId { get; }

        public void NotifyCollision(Collider otherCollider)
        {
            Debug.Log("Collision2");

            if (Collision != null)
                Collision(otherCollider);
        }
    }
}
