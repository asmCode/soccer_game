using UnityEngine;
namespace ssg
{

    public class CapsuleCollider : Collider
    {
        public override ColliderId ColliderId
        {
            get { return ColliderId.Capsule; }
        }

        public Vector3 Point1WorldPosition { get; set; }
        public Vector3 Point2WorldPosition { get; set; }
        public float radius { get; set; }
    }
}
