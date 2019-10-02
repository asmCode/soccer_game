using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ssg.Physics
{
    public interface IPhysics
    {
        void Update(List<PhysicsObject> objects, float dt);
    }
}
