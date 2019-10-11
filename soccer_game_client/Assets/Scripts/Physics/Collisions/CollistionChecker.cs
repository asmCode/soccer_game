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
            var tr1 = collider1.Trans.GetTransform();
            var tr2 = collider2.Trans.GetTransform();

            Vector3 worldCenter1 = tr1.TransformPoint(collider1.LocalCenter);
            Vector3 worldCenter2 = tr2.TransformPoint(collider2.LocalCenter);

            Vector2 pos1 = new Vector2(worldCenter1.x, worldCenter1.z);
            Vector2 pos2 = new Vector2(worldCenter2.x, worldCenter2.z);

            if (Vector2.Distance(pos1, pos2) < collider1.Radius + collider2.Radius)
                return new CollisionResult();

            return null;
        }
    }
}

/* TODO: i can keep info regarding position of the previous frame so i can calculate collisions contuously. */
