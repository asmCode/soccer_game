using UnityEngine;

namespace ssg
{
    public class SphereCollider : Collider
    {
        public override ColliderId ColliderId
        {
            get { return ColliderId.Sphere; }
        }

        public Vector3 LocalCenter { get; set; }
        public float Radius { get; set; }
    }
}
