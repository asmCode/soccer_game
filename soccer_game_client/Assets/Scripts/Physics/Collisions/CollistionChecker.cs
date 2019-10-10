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
            var tr1Pos = collider1.Trans.GetPosition();
            var tr2Pos = collider2.Trans.GetPosition();

            Vector2 pos1 = new Vector2(collider1.LocalCenter.x + tr1Pos.x, collider1.LocalCenter.z + tr1Pos.z);
            Vector2 pos2 = new Vector2(collider2.LocalCenter.x + tr2Pos.x, collider2.LocalCenter.z + tr2Pos.z);

            if (Vector2.Distance(pos1, pos2) < collider1.Radius + collider2.Radius)
                return new CollisionResult();

            return null;
        }
    }
}

/* TODO: i can keep info regarding position of the previous frame so i can calculate collisions contuously. */
