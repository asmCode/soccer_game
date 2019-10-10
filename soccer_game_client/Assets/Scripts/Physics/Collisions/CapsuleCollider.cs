using UnityEngine;
namespace ssg
{

    public class CapsuleCollider : Collider
    {
        public override ColliderId ColliderId
        {
            get { return ColliderId.Capsule; }
        }

        public Vector3 LocalCenter { get; set; }
        public float Radius { get; set; }
    }
}
