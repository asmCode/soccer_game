using UnityEngine;

namespace ssg
{
    public class CollisionChecker
    {
        public static CollisionResult CheckCollision(Collider collider1, Collider collider2)
        {
            if (collider1.ColliderId == ColliderId.Capsule &&
                collider2.ColliderId == ColliderId.Capsule)
            {
                return CheckCapsuleCapsuleCollision(collider1 as CapsuleCollider, collider2 as CapsuleCollider);
            }

            return null;
        }

        public static CollisionResult CheckCapsuleCapsuleCollision(CapsuleCollider collider1, CapsuleCollider collider2)
        {
            Vector2 pos1 = new Vector2(collider1.Point1WorldPosition.x, collider1.Point1WorldPosition.z);
            Vector2 pos2 = new Vector2(collider2.Point1WorldPosition.x, collider2.Point1WorldPosition.z);

            if (Vector2.Distance(pos1, pos1) < collider1.radius + collider2.radius)
                return new CollisionResult();

            return null;
        }
    }
}

/* TODO: i can keep info regarding position of the previous frame so i can calculate collisions contuously. */
